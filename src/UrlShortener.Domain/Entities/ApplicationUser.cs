using Microsoft.AspNetCore.Identity;
using UrlShortener.Domain.Entities.Abstract;

namespace UrlShortener.Domain.Entities {
    public class ApplicationUser : IdentityUser<int>, IEntity {

    }
}
