using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Application.Common.DbContexts;
using UrlShortener.Application.Common.Exceptions;
using UrlShortener.Application.Common.Interfaces;
using UrlShortener.Domain.Constants;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.CQRS.ShorteningUrls.Commands.RemoveLink {
    public class RemoveLinkCommandHandler : IRequestHandler<RemoveLinkCommand> {
        private readonly IApplicationDbContext applicationDbContext;
        private readonly IIdentityService identityService;

        public RemoveLinkCommandHandler(IApplicationDbContext dbContext, IIdentityService identityService) {
            this.applicationDbContext = dbContext;
            this.identityService = identityService;
        }


        public async Task Handle(RemoveLinkCommand request, CancellationToken cancellationToken) {
            var e = await applicationDbContext.ShortenedUrls
                .Include(r => r.Creator)
                .Where(e => e.ShortenedUrl == request.LinkId)
                .FirstOrDefaultAsync();

            if ( e is null )
                throw new NotFoundException(request.LinkId!, nameof(ShortenedUrlEntity));

            var isAdmin = ( await identityService.GetUserRolesAsync(request.RequestedId ?? -1) )
                .Any((r) => r == Roles.Administrator);

            // if requester is not admin and
            // requires to delete not it`s own ref = forbid
            if ( e.CreatorId != request.RequestedId && !isAdmin )
                throw new ForbiddenAccessException();

            this.applicationDbContext.ShortenedUrls.Remove(e);

            await applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
