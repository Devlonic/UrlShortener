namespace UrlShortener.Application.Models.Lookups {
    public record class UserLookup {
        public int? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }

        public IList<string>? Roles { get; set; }
    }
}
