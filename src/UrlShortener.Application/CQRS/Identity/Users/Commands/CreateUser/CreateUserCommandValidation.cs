using FluentValidation;

namespace UrlShortener.Application.CQRS.Identity.Users.Commands.CreateUser {
    public class CreateUserCommandValidation : AbstractValidator<CreateUserCommand> {
        public CreateUserCommandValidation() {
            RuleFor(c => c.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(64);

            RuleFor(c => c.Username)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(128);

            RuleFor(c => c.Email)
                .NotEmpty()
                .Matches("[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?");
        }
    }
}
