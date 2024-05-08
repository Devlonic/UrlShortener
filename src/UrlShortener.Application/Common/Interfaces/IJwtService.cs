using UrlShortener.Application.Models.Lookups;

namespace UrlShortener.Application.Common.Interfaces {
    public interface IJwtService {
        string GenerateToken(int userId, UserLookup user);
    }
}
