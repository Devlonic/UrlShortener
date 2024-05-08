using MediatR;

namespace UrlShortener.Application.CQRS.Identity.Users.Commands.CreateUser {
    public class CreateUserCommand : IRequest<CreateUserCommandResult> {
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
    }
}
