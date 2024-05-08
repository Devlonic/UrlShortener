using MediatR;
using UrlShortener.Application.Common.Interfaces;

namespace UrlShortener.Application.CQRS.Identity.Users.Commands.CreateUser {
    public class CreateUserCommnadHandler : IRequestHandler<CreateUserCommand, CreateUserCommandResult> {
        private readonly IIdentityService _identityService;
        private readonly IJwtService _jwtService;
        private readonly IMediator _mediator;
        public CreateUserCommnadHandler(IIdentityService identityService, IJwtService jwtService, IMediator mediator) {
            _identityService = identityService;
            _jwtService = jwtService;
            _mediator = mediator;
        }
        public async Task<CreateUserCommandResult> Handle(CreateUserCommand request, CancellationToken cancellationToken) {
            var result = await _identityService.CreateUserAsync(
                 request.Password, request.Email, request.Username);

            return new CreateUserCommandResult() {
                UserId = result.UserId,
                Token = _jwtService.GenerateToken(result.UserId ?? -1, result),
            };
        }
    }
}
