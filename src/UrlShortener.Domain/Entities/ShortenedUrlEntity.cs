using UrlShortener.Domain.Entities.Abstract;

namespace UrlShortener.Domain.Entities {
    public class ShortenedUrlEntity : IEntity {
        public string? ForwardToUrl { get; set; }
        public string? ShortenedUrl { get; set; }

        public ApplicationUser? Creator { get; set; }
        public Guid? CreatorId { get; set; }
    }
}
