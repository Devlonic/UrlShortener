using UrlShortener.Domain.Entities.Abstract;

namespace UrlShortener.Domain.Entities {
    public class ShortenedUrlEntity : IEntity {
        public string? ShortenedUrl { get; set; }
        public string? ForwardToUrl { get; set; }

        public ApplicationUser? Creator { get; set; }
        public int? CreatorId { get; set; }
    }
}
