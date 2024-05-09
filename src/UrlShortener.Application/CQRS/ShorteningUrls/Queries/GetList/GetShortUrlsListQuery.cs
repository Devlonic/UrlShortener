using MediatR;
using UrlShortener.Application.Models.Lookups;

namespace UrlShortener.Application.CQRS.ShorteningUrls.Queries {
    public class GetShortUrlsListQuery : IRequest<IList<ShortenedUrlLookup>> {

    }
}
