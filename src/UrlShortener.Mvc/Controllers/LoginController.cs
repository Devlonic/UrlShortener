using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.Security.Authentication;
using UrlShortener.Application.Common.Exceptions;
using UrlShortener.Application.CQRS.Identity.Users.Commands.CreateUser;
using UrlShortener.Application.CQRS.Identity.Users.Commands.SignInUser;
using UrlShortener.Mvc.Common.Exceptions;
using UrlShortener.Mvc.Dto.Auth.User;
using UrlShortener.Mvc.Models;

namespace UrlShortener.Mvc.Controllers {
    public class LoginController : Controller {
        private readonly IMapper mapper;
        private IMediator mediator = null!;

        // if this.mediator is null - get and set it from context.
        protected IMediator Mediator => mediator ??=
            HttpContext.RequestServices.GetService<IMediator>() ??
                throw new ServiceNotRegisteredException(nameof(IMediator));

        public LoginController(IMapper mapper) {
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public IActionResult SignIn() {
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInUserDto dto) {

            try {
                var command = mapper.Map<SignInUserCommand>(dto);
                var result = await Mediator.Send(command);
                Response.Cookies.Append("Authentication", result.Token!);
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""));
            }
            catch ( InvalidCredentialException e ) {
                ViewBag.LoginError = e.Message;
                return View(nameof(Index));
            }
            catch ( Exception e ) {
                ViewBag.LoginError = "Unknown server error";
                return View(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult SignUp() {
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto dto) {

            try {
                var command = mapper.Map<CreateUserCommand>(dto);
                var result = await Mediator.Send(command);
                Response.Cookies.Append("Authentication", result.Token!);
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""));
            }
            catch ( ValidationException ve ) {
                ViewBag.RegistrationError = "Validation error";
                ViewBag.ValidationErrors = ve.Errors;
                return View(nameof(Index));
            }
            catch ( AlreadyExistsException e ) {
                ViewBag.RegistrationError = "User already exists";
                return View(nameof(Index));
            }
            catch ( Exception e ) {
                ViewBag.RegistrationError = "Unknown server error: " + e.ToString();
                return View(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult SignOut() {
            Response.Cookies.Delete("Authentication"); // expire cookie
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""));
        }
    }
}
