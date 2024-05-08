using MediatR;
using UrlShortener.Application.Models.Lookups;

namespace UrlShortener.Application.CQRS.ShorteningUrls.Commands {
    public class ShortenLinkCommand : IRequest<ShortenedUrlLookup> {
        public int? RequesterId { get; set; }
        public Uri? FullLink { get; set; }
    }
}
