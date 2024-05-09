namespace UrlShortener.Application.Models.Lookups {
    public record class AboutLookup {
        public string? Text { get; set; } = null!;
        public UserLookup? Editor { get; set; }
        public DateTime? EditedAt { get; set; }
    }
}
