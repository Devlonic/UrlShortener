using UrlShortener.Application.Models.Lookups;

namespace UrlShortener.Application.Common.Interfaces {
    public interface IJwtService {
        string GenerateToken(Guid userId, UserLookup user);
        public string GenerateTokenWithSecretPhrase(string secretPhrase);
        public bool VerifyTokenWithSecretPhrase(string token, string secretPhrase);
    }
}
