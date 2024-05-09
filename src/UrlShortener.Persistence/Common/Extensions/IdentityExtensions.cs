using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using UrlShortener.Domain.Entities;
using UrlShortener.Application.Common.Interfaces;
using UrlShortener.Persistence.Services;

namespace UrlShortener.Persistence.Common.Extensions {
    public static class IdentityExtensions {
        /// <summary>
        /// Adds and configures the identity system for the specified User and Role types.
        /// </summary>
        /// <param name="services">The services available in the application.</param>
        /// <returns>An <see cref="IdentityBuilder"/> for creating and configuring the identity system.</returns>
        public static IdentityBuilder AddIdentityExtensions(
            this IServiceCollection services, IConfiguration configuration) {

            // Services used by identity
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            });
            services.AddAuthorization();

            // setup JWT bearer authentication
            string? issuer = configuration.GetValue<string>("Jwt:Issuer");
            string? audience = configuration.GetValue<string>("Jwt:Audience");
            string? symmetricKey = configuration.GetValue<string>("Jwt:Key");

            Guard.Against.Null(issuer, message: "value 'Jwt:Issuer' not found.");
            Guard.Against.Null(audience, message: "value 'Jwt:Audience' not found.");
            Guard.Against.Null(symmetricKey, message: "value 'Jwt:Key' not found.");

            services.AddAuthentication(o => {
                o.DefaultAuthenticateScheme = "Bearer";
                o.DefaultChallengeScheme = "Bearer";
            })
            .AddJwtBearer(options => {
                options.Events = new JwtBearerEvents {
                    OnMessageReceived = context => {
                        // ignore "Bearer" preffix
                        context.Token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");


                        // if token already taken from headers
                        if ( !string.IsNullOrEmpty(context.Token) )
                            return Task.CompletedTask;

                        // extract token from cookie
                        var x = context.Request.Cookies["Authentication"];
                        if ( x is not null )
                            context.Token = x.Replace("Bearer ", "");

                        return Task.CompletedTask;
                    }
                };

                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(symmetricKey))
                };
            });

            // Hosting doesn't add IHttpContextAccessor by default
            services.AddHttpContextAccessor();
            // Identity services
            services.TryAddScoped<IPasswordHasher<ApplicationUser>, PasswordHasher<ApplicationUser>>();
            services.TryAddScoped<ILookupNormalizer, UpperInvariantLookupNormalizer>();
            services.TryAddScoped<IRoleValidator<ApplicationRole>, RoleValidator<ApplicationRole>>();
            // No interface for the error describer so we can add errors without rev'ing the interface
            services.TryAddScoped<IdentityErrorDescriber>();
            services.TryAddScoped<ISecurityStampValidator, SecurityStampValidator<ApplicationUser>>();
            services.TryAddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>>();
            services.TryAddScoped<IUserConfirmation<ApplicationUser>, DefaultUserConfirmation<ApplicationUser>>();

            services.TryAddScoped<SignInManager<ApplicationUser>>();
            services.TryAddScoped<UserManager<ApplicationUser>>();
            services.TryAddScoped<RoleManager<ApplicationRole>>();

            // register custom services
            services.TryAddScoped<IDateTimeService, DateTimeService>();
            services.TryAddScoped<IJwtService, JwtService>();
            services.TryAddScoped<IIdentityService, IdentityService>();

            return new IdentityBuilder(typeof(ApplicationUser), typeof(ApplicationRole), services);
        }
    }
}
