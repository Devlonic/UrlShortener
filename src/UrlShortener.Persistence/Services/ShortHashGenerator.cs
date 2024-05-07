using System.Text;
using UrlShortener.Application.Common.Interfaces;

namespace UrlShortener.Persistence.Services {
    public class ShortHashGenerator : IHashGenerator {
        public async Task<string> GenerateHashAsync(CancellationToken cancellationToken) {
            return await Task.Run(() => {
                StringBuilder sb = new StringBuilder();
                for ( var i = 0; i < 5; i++ ) {
                    char symb;
                    switch ( Random.Shared.Next(0, 3) ) {
                        case 0:
                        symb = (char)Random.Shared.Next('a', 'z');
                        break;
                        case 1:
                        symb = (char)Random.Shared.Next('A', 'Z');
                        break;
                        case 2:
                        symb = (char)Random.Shared.Next('0', '9');
                        break;
                        default:
                        symb = '0';
                        break;
                    }
                    sb.Append(symb);
                }
                return sb.ToString();
            }, cancellationToken);
        }
    }
}
