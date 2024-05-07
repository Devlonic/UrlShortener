using UrlShortener.Application.Common.Interfaces;

namespace UrlShortener.Persistence.Services;

public class DateTimeService : IDateTimeService {
    public DateTime Now => DateTime.UtcNow;
}
