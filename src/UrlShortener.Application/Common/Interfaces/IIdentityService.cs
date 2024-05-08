using UrlShortener.Application.Models.Lookups;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Common.Interfaces {
    public interface IIdentityService {
        Task<UserLookup> CreateUserAsync(string password, string email, string username);
        Task CreateRoleAsync(string roleName);
        Task AddToRoleAsync(int userId, string roleName);
        Task<IList<string>> GetUserRolesAsync(int userId);
        Task<UserLookup> GetUserLookupAsync(int userId);
    }
}
