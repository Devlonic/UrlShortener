using MediatR;
using UrlShortener.Application.Common.DbContexts;
using UrlShortener.Application.Common.Interfaces;
using UrlShortener.Application.Models.Lookups;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.CQRS.ShorteningUrls.Commands {
    public class ShortenLinkCommandHandler : IRequestHandler<ShortenLinkCommand, ShortenedUrlLookup> {
        private readonly IApplicationDbContext applicationDbContext;
        private readonly IHashGenerator hashGenerator;
        private readonly IDateTimeService dateTimeService;

        public ShortenLinkCommandHandler(IApplicationDbContext dbContext, IHashGenerator hashGenerator, IDateTimeService dateTimeService) {
            this.applicationDbContext = dbContext;
            this.hashGenerator = hashGenerator;
            this.dateTimeService = dateTimeService;
        }

        public async Task<ShortenedUrlLookup> Handle(ShortenLinkCommand request, CancellationToken cancellationToken) {
            var newLink = new ShortenedUrlEntity() {
                CreatorId = request.RequesterId,
                ForwardToUrl = request.FullLink!.ToString(),
                ShortenedUrl = await hashGenerator.GenerateHashAsync(cancellationToken),
                CreatedAt = dateTimeService.Now
            };

            applicationDbContext.ShortenedUrls.Add(newLink);

            await applicationDbContext.SaveChangesAsync(cancellationToken);

            return new ShortenedUrlLookup() {
                ShortenedUrl = newLink.ShortenedUrl,
                FullUrl = newLink.ForwardToUrl,
                CreatedById = newLink.CreatorId,
                CreatedAt = newLink.CreatedAt
            };
        }
    }
}
