using MediatR;

namespace UrlShortener.Application.CQRS.ShorteningUrls.Commands.RemoveLink {
    public class RemoveLinkCommand : IRequest {
        public string? LinkId { get; set; }
        public int? RequestedId { get; set; }
    }
}
