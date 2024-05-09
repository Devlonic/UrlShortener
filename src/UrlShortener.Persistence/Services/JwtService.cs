using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UrlShortener.Application.Common.Interfaces;
using UrlShortener.Application.Models.Lookups;

namespace UrlShortener.Persistence.Services {
    public class JwtService : IJwtService {
        private readonly IConfiguration _configuration;
        private readonly IDateTimeService _dateTimeService;

        public JwtService(IConfiguration configuration, IDateTimeService dateTimeService) {
            _configuration = configuration;
            _dateTimeService = dateTimeService;
        }

        public string GenerateToken(int userId, UserLookup user) {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Key") ?? throw new Exception("Jwt:Key not found")));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new() {
                // idk how to avoid it
                new Claim("roles", "")
            };

            if ( user.Roles is not null ) {
                foreach ( var role in user.Roles ) {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                    claims.Add(new Claim("roles", role));
                }
            }

            claims.Add(new Claim("userId", userId.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, user.Email ?? ""));
            claims.Add(new Claim("email", user.Email ?? ""));
            claims.Add(new Claim("username", user.UserName ?? ""));

            var token = new JwtSecurityToken(
                _configuration.GetValue<string>("Jwt:Issuer"),
                _configuration.GetValue<string>("Jwt:Audience"),
                claims,
                expires: _dateTimeService.Now.AddHours(_configuration.GetValue<int>("Jwt:ExpiresAfterHours")),
                signingCredentials: credentials
                );

            return $"{_configuration.GetValue<string>("Jwt:Scheme")} {new JwtSecurityTokenHandler().WriteToken(token)}";
        }
    }
}
