using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using UrlShortener.Application.Common.Exceptions;
using UrlShortener.Application.Common.Interfaces;
using UrlShortener.Application.Models.Lookups;
using UrlShortener.Domain.Constants;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Persistence.Services {
    public class IdentityService : IIdentityService {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager) {
            _userManager = userManager;
            this.roleManager = roleManager;
        }

        private async Task AddToRoleAsync(ApplicationUser user, string roleName) {
            var result = await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task AddToRoleAsync(int userId, string roleName) {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if ( user == null )
                throw new NotFoundException(userId.ToString(), nameof(ApplicationUser));

            await AddToRoleAsync(user, roleName);
        }
        public async Task CreateRoleAsync(string roleName) {
            if ( await roleManager.FindByNameAsync(roleName) != null ) {
                throw new AlreadyExistsException(roleName, nameof(ApplicationRole));
            }

            var result = await roleManager.CreateAsync
                (
                    new ApplicationRole() {
                        Name = roleName,
                        NormalizedName = roleName.ToUpper()
                    }
                );
        }
        public async Task<UserLookup> CreateUserAsync(string password, string email, string username) {

            if ( await _userManager.FindByEmailAsync(email) != null ) {
                throw new AlreadyExistsException(email, "User is already exist");
            }

            var user = new ApplicationUser {
                UserName = username,
                Email = email
            };

            var result = await _userManager.CreateAsync(user);

            await AddToRoleAsync(user, Roles.User);

            await _userManager.AddPasswordAsync(user, password);

            return new UserLookup() {
                Email = email,
                UserName = username,
                UserId = user.Id,
                Roles = ( await GetUserRolesAsync(user.Id) )
            };
        }

        public async Task<IList<string>> GetUserRolesAsync(int userId) {
            ApplicationUser? user = await _userManager.FindByIdAsync(userId.ToString());
            if ( user == null )
                throw new NotFoundException(userId.ToString(), nameof(ApplicationUser));

            var roles = await _userManager.GetRolesAsync(user);

            return roles;
        }
        public async Task<UserLookup> GetUserLookupAsync(int userId) {
            ApplicationUser? user = await _userManager.FindByIdAsync(userId.ToString());
            if ( user == null )
                throw new NotFoundException(userId.ToString(), nameof(ApplicationUser));
            return new UserLookup() {
                Email = user.Email,
                UserName = user.UserName,
                UserId = user.Id,
                Roles = ( await GetUserRolesAsync(user.Id) )
            };
        }
    }
}
