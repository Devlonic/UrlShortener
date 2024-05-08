using UrlShortener.Application.Models.Lookups;

namespace UrlShortener.Application.CQRS.Identity.Users.Commands.SignInUser {
    public class SignInUserCommandResult {
        public string Token { get; set; } = null!;
        public UserLookup User { get; set; } = null!;
    }
}
