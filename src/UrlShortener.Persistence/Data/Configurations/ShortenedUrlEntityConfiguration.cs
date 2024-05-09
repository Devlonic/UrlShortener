using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Persistence.Data.Configurations {
    public class ShortenedUrlEntityConfiguration : IEntityTypeConfiguration<ShortenedUrlEntity> {
        public void Configure(EntityTypeBuilder<ShortenedUrlEntity> builder) {
            #region Properties

            builder.HasKey(e => e.ShortenedUrl);

            builder.HasOne(e => e.Creator)
                .WithMany()
                .HasForeignKey(e => e.CreatorId);

            #endregion
        }
    }

    public class AboutEntityConfiguration : IEntityTypeConfiguration<AboutEntity> {
        public void Configure(EntityTypeBuilder<AboutEntity> builder) {
            #region Properties

            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Editor)
                .WithMany()
                .HasForeignKey(e => e.EditorId);

            #endregion
        }
    }
}
