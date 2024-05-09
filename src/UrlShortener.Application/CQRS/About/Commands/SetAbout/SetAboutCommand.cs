using MediatR;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Application.Common.DbContexts;
using UrlShortener.Application.Common.Interfaces;

namespace UrlShortener.Application.CQRS.About.Commands.SetAbout {
    public class SetAboutCommand : IRequest {
        public string? NewText { get; set; }
        public int? EditorId { get; set; }
    }
    public class SetAboutCommandHandler : IRequestHandler<SetAboutCommand> {
        private readonly IApplicationDbContext applicationDbContext;
        private readonly IDateTimeService dateTimeService;

        public SetAboutCommandHandler(IApplicationDbContext dbContext, IHashGenerator hashGenerator, IDateTimeService dateTimeService) {
            this.applicationDbContext = dbContext;
            this.dateTimeService = dateTimeService;
        }

        public async Task Handle(SetAboutCommand request, CancellationToken cancellationToken) {
            var existing = await applicationDbContext.Abouts.FirstOrDefaultAsync();
            if ( existing is null ) {
                var about = new Domain.Entities.AboutEntity() {
                    EditedAt = dateTimeService.Now,
                    EditorId = request.EditorId,
                    Text = request.NewText
                };
                applicationDbContext.Abouts.Add(about);
            }
            else {
                existing.Text = request.NewText;
                existing.EditedAt = dateTimeService.Now;
                existing.EditorId = request.EditorId;
            }
            await applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
