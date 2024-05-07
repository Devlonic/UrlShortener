using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Persistence.Data.Configurations.Identity {
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser> {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder) {
            #region Properties

            #endregion
        }
    }
}
