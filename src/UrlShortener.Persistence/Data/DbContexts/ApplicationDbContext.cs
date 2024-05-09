using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Application.Common.DbContexts;
using UrlShortener.Domain.Entities;
using UrlShortener.Persistence.Data.Configurations;
using UrlShortener.Persistence.Data.Configurations.Identity;

namespace UrlShortener.Persistence.Data.Contexts {
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>, IApplicationDbContext {
        public DbSet<ShortenedUrlEntity> ShortenedUrls { get; set; }
        public DbSet<AboutEntity> Abouts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder) {
            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new ApplicationRoleConfiguration());
            builder.ApplyConfiguration(new ShortenedUrlEntityConfiguration());
            builder.ApplyConfiguration(new AboutEntityConfiguration());

            base.OnModelCreating(builder);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
            // default behaviour
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
