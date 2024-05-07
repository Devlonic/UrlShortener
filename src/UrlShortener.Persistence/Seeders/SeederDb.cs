using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Application.Common.Exceptions;
using UrlShortener.Application.Common.Interfaces;
using UrlShortener.Domain.Constants;
using UrlShortener.Persistence.Data.Contexts;

namespace UrlShortener.Persistence.Data.Seeders {
    public static class SeederDB {
        public static void SeedData(this IApplicationBuilder app) {
            using ( var scope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>().CreateScope() ) {

                scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();

                // add roles
                var identityService = scope.ServiceProvider.GetRequiredService<IIdentityService>();
                try {
                    identityService.CreateRoleAsync(Roles.User).Wait();
                }
                catch ( AlreadyExistsException ) {

                }
                catch ( AggregateException ) {

                }

                try {
                    identityService.CreateRoleAsync(Roles.Administrator).Wait();
                }
                catch ( AlreadyExistsException ) {

                }
                catch ( AggregateException ) {

                }
            }
        }
    }
}
