using MediatR;
using UrlShortener.Application.Models.Lookups;

namespace UrlShortener.Application.CQRS.ShorteningUrls.Queries {
    public class ResolveShortUrlQuery : IRequest<ShortenedUrlLookup> {
        public string ShortHash { get; set; }
    }
}
