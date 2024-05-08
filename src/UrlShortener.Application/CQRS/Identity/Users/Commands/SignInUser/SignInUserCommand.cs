using MediatR;

namespace UrlShortener.Application.CQRS.Identity.Users.Commands.SignInUser {
    public class SignInUserCommand : IRequest<SignInUserCommandResult> {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
