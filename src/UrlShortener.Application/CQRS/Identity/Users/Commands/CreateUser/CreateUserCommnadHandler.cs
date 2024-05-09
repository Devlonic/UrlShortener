using MediatR;
using UrlShortener.Application.Common.Interfaces;
using UrlShortener.Domain.Constants;

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

            if ( request.DoesSetAdmin == true )
                await _identityService.AddToRoleAsync(result.UserId ?? -1, Roles.Administrator);
            result.Roles.Add(Roles.Administrator);

            return new CreateUserCommandResult() {
                UserId = result.UserId,
                Token = _jwtService.GenerateToken(result.UserId ?? -1, result),
            };
        }
    }
}
