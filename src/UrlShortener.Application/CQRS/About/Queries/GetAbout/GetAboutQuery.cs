using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Application.Common.DbContexts;
using UrlShortener.Application.Common.Interfaces;
using UrlShortener.Application.Models.Lookups;

namespace UrlShortener.Application.CQRS.About.Queries.GetAbout {
    public class GetAboutQuery : IRequest<AboutLookup> {
        public bool IsAdmin { get; set; }
    }
    public class GetAboutQueryHandler : IRequestHandler<GetAboutQuery, AboutLookup> {
        private readonly IApplicationDbContext applicationDbContext;

        public GetAboutQueryHandler(IApplicationDbContext dbContext, IHashGenerator hashGenerator, IDateTimeService dateTimeService) {
            this.applicationDbContext = dbContext;
        }
        public async Task<AboutLookup> Handle(GetAboutQuery request, CancellationToken cancellationToken) {
            var query = applicationDbContext.Abouts.Where((x) => true);
            if ( request.IsAdmin ) {
                var x = await query
                    .Include(a => a.Editor)
                    .Select(a => new AboutLookup() {
                        Text = a.Text,
                        EditedAt = a.EditedAt,
                        Editor = new UserLookup() {
                            UserId = a.Editor.Id,
                            //Email = a.Editor.Email,
                            UserName = a.Editor.UserName
                        }
                    }).SingleOrDefaultAsync(cancellationToken);
                return x;
            }

            return ( await query.Select(a => new AboutLookup() {
                Text = a.Text
            }).SingleOrDefaultAsync(cancellationToken) ) ?? new AboutLookup() {
                Text = "Not Set"
            };
        }
    }
}
