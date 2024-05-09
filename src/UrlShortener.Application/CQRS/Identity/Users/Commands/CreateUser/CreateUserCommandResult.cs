namespace UrlShortener.Application.CQRS.Identity.Users.Commands.CreateUser {
    public class CreateUserCommandResult {
        public string? Token { get; set; }
        public int? UserId { get; set; }
        public string? Role { get; set; }
    }
}
