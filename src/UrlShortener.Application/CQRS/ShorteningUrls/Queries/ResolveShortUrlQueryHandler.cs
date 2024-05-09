using Ardalis.GuardClauses;
using MediatR;
using UrlShortener.Application.Common.DbContexts;
using UrlShortener.Application.Common.Interfaces;
using UrlShortener.Application.Models.Lookups;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.CQRS.ShorteningUrls.Queries {
    public class ResolveShortUrlQueryHandler : IRequestHandler<ResolveShortUrlQuery, ShortenedUrlLookup> {
        private readonly IApplicationDbContext applicationDbContext;

        public ResolveShortUrlQueryHandler(IApplicationDbContext dbContext) {
            this.applicationDbContext = dbContext;
        }

        public async Task<ShortenedUrlLookup> Handle(ResolveShortUrlQuery request, CancellationToken cancellationToken) {
            var result = await applicationDbContext
                .ShortenedUrls
                .FindAsync(request.ShortHash);

            if ( result is null )
                throw new NotFoundException(request.ShortHash, nameof(ShortenedUrlEntity));

            return new ShortenedUrlLookup() {
                FullUrl = result.ForwardToUrl
            };
        }
    }
}
