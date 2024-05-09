using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Authentication;
using UrlShortener.Application.CQRS.About.Commands.SetAbout;
using UrlShortener.Application.CQRS.Identity.Users.Commands.SignInUser;
using UrlShortener.Application.CQRS.ShorteningUrls.Queries;
using UrlShortener.Domain.Constants;
using UrlShortener.Mvc.Common.Exceptions;
using UrlShortener.Mvc.Dto;
using UrlShortener.Mvc.Models;

namespace UrlShortener.Mvc.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper mapper;
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
        public HomeController(ILogger<HomeController> logger, IMapper mapper) {
            _logger = logger;
            this.mapper = mapper;
        }

        public IActionResult Index() {
            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        [AllowAnonymous]
        [Authorize(Roles = $"{Roles.User},{Roles.Administrator}", AuthenticationSchemes = "Bearer")]
        public IActionResult About() {
            return View(new { IsAdmin = User.IsInRole(Roles.Administrator) });
        }

        [HttpPost]
        [Authorize(Roles = Roles.Administrator, AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AboutPost([FromForm] SetAboutDto dto) {
            try {
                var command = mapper.Map<SetAboutCommand>(dto);
                command.EditorId = UserId;
                await Mediator.Send(command);
                return RedirectToAction(nameof(About), nameof(HomeController).Replace("Controller", ""));
            }
            catch ( InvalidCredentialException e ) {
                ViewBag.LoginError = e.Message;
                return View(nameof(About));
            }
            catch ( Exception e ) {
                ViewBag.LoginError = "Unknown server error";
                return View(nameof(About));
            }
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
