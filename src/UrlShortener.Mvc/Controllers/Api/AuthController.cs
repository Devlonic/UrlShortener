using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Application.CQRS.Identity.Users.Commands.CreateUser;
using UrlShortener.Application.CQRS.Identity.Users.Commands.SignInUser;
using UrlShortener.Mvc.Dto.Auth.User;

namespace UrlShortener.Mvc.Controllers.Api {
    public class AuthController : BaseController {
        private readonly IMapper mapper;

        public AuthController(IMapper mapper) {
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<int>> SignUp([FromForm] SignUpDto dto) {
            var command = mapper.Map<CreateUserCommand>(dto);
            var result = await Mediator.Send(command);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> SignIn([FromBody] SignInUserDto dto) {
            var command = mapper.Map<SignInUserCommand>(dto);
            var result = await Mediator.Send(command);

            return Ok(result);
        }
    }
}
