using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UrlShortener.Application.CQRS.ShorteningUrls.Queries;
using UrlShortener.Mvc.Common.Exceptions;
using UrlShortener.Mvc.Models;

namespace UrlShortener.Mvc.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private IMediator mediator = null!;

        // if this.mediator is null - get and set it from context.
        protected IMediator Mediator => mediator ??=
            HttpContext.RequestServices.GetService<IMediator>() ??
                throw new ServiceNotRegisteredException(nameof(IMediator));

        // get user id from claims (token).
        // if User or Identity is null - set UserId to empty
        internal int UserId =>
            !User?.Identity?.IsAuthenticated ?? false
            ? -1
          : int.Parse(User?.FindFirst("userId")?.Value ?? "");

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }

        public IActionResult Index() {
            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        public async Task<IActionResult> Short([FromRoute] string hash) {
            try {
                var query = new ResolveShortUrlQuery() {
                    ShortHash = hash
                };
                var result = await Mediator.Send(query);
                return Redirect(result.FullUrl!);
            }
            catch ( NotFoundException ) {
                ViewBag.LinkBroken = true;
                ViewBag.Link = hash;
                return View(nameof(Index));
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
