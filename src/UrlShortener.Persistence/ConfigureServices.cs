using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ardalis.GuardClauses;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.SignalR;
using UrlShortener.Application.Common.DbContexts;
using UrlShortener.Persistence.Data.Contexts;
using UrlShortener.Persistence.Common.Extensions;
using Newtonsoft.Json;

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

        return services;
    }
}
