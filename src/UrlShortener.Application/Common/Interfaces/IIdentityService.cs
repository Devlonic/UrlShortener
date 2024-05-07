using UrlShortener.Application.Models.Lookups;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Common.Interfaces {
    public interface IIdentityService {
        Task<UserLookup> CreateUserAsync(string password, string email, string username);
        Task CreateRoleAsync(string roleName);
        Task AddToRoleAsync(Guid userId, string roleName);
        Task<IList<string>> GetUserRolesAsync(Guid userId);
        Task<UserLookup> GetUserLookupAsync(Guid userId);
    }
}
