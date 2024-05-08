using FluentValidation;

namespace UrlShortener.Application.CQRS.Identity.Users.Commands.SignInUser {
    public class SignInUserCommandValidation : AbstractValidator<SignInUserCommand> {
        public SignInUserCommandValidation() {
            RuleFor(c => c.Password)
                .NotEmpty();

            RuleFor(c => c.Email)
                .NotEmpty()
                .Matches("[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?");
        }
    }
}
