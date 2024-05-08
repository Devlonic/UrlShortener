using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ardalis.GuardClauses;
using Microsoft.Extensions.DependencyInjection.Extensions;
using UrlShortener.Application.Common.DbContexts;
using UrlShortener.Persistence.Data.Contexts;
using UrlShortener.Persistence.Common.Extensions;
using Newtonsoft.Json;
using UrlShortener.Application.Common.Interfaces;
using UrlShortener.Persistence.Services;
using Microsoft.AspNetCore.SignalR;
using UrlShortener.Persistence.Data.Providers;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices {
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) {
        var connectionString = configuration.GetConnectionString("default_url_shortener");

        // ensure that connection string exists, else throw startup exception
        Guard.Against.Null(connectionString, message: "Connection string 'default_url_shortener' not found.");

        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>((sp, options) => {
            options.UseNpgsql(connectionString);
        });

        // setup Identity services
        services.AddIdentityExtensions(configuration)
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.TryAddSingleton((provider) => JsonSerializer.CreateDefault());
        services.TryAddSingleton<IUserIdProvider, ApplicationUserIdProvider>();

        services.TryAddScoped<IJwtService, JwtService>();
        services.TryAddScoped<IDateTimeService, DateTimeService>();
        services.TryAddScoped<IHashGenerator, ShortHashGenerator>();

        return services;
    }
}
