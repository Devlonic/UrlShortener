using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Application.Common.DbContexts;
using UrlShortener.Application.Models.Lookups;

namespace UrlShortener.Application.CQRS.ShorteningUrls.Queries {
    public class GetShortUrlsListQueryHandler : IRequestHandler<GetShortUrlsListQuery, IList<ShortenedUrlLookup>> {
        private readonly IApplicationDbContext applicationDbContext;

        public GetShortUrlsListQueryHandler(IApplicationDbContext dbContext) {
            this.applicationDbContext = dbContext;
        }

        public async Task<IList<ShortenedUrlLookup>> Handle(GetShortUrlsListQuery request, CancellationToken cancellationToken) {
            var result = await applicationDbContext
                .ShortenedUrls
                .Select(u => new ShortenedUrlLookup() {
                    FullUrl = u.ForwardToUrl,
                    ShortenedUrl = u.ShortenedUrl,
                    CreatedById = u.CreatorId
                })
                .ToListAsync();

            return result;
        }
    }
}
