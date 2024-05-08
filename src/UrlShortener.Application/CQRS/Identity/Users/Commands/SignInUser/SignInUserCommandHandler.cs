using MediatR;
using Microsoft.AspNetCore.Identity;
using UrlShortener.Application.Common.Interfaces;
using UrlShortener.Application.Models.Lookups;
using UrlShortener.Domain.Entities;
using System.Security.Authentication;

namespace UrlShortener.Application.CQRS.Identity.Users.Commands.SignInUser {
    public class SignInUserCommandHandler : IRequestHandler<SignInUserCommand, SignInUserCommandResult> {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;

        public SignInUserCommandHandler(UserManager<ApplicationUser> userManager, IJwtService jwtService) {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<SignInUserCommandResult> Handle(SignInUserCommand request, CancellationToken cancellationToken) {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if ( user == null )
                throw new InvalidCredentialException("Wrong login or password");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            // hiding correct email, but wrong password.
            // making the behaviour as it is anyway wrong
            if ( !isPasswordValid ) {
                await _userManager.AccessFailedAsync(user);
                throw new InvalidCredentialException("Wrong login or password");
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var userLookup = new UserLookup() {
                Email = user.Email,
                UserId = user.Id,
                UserName = user.UserName,
                Roles = userRoles
            };

            return new SignInUserCommandResult() {
                Token = _jwtService.GenerateToken(user.Id, userLookup),
                User = userLookup
            };
        }
    }
}
