using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Common.DbContexts {
    public interface IApplicationDbContext {
        DbSet<ShortenedUrlEntity> ShortenedUrls { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
