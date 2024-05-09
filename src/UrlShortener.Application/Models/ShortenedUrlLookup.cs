using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Models.Lookups {
    public record class ShortenedUrlLookup {
        public string? ShortenedUrl { get; set; } = null;
        public string? FullUrl { get; set; } = null;
        public int? CreatedById { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
