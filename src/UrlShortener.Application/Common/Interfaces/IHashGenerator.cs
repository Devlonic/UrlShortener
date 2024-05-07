namespace UrlShortener.Application.Common.Interfaces {
    public interface IHashGenerator {
        Task<string> GenerateHashAsync(CancellationToken cancellationToken);
    }
}
