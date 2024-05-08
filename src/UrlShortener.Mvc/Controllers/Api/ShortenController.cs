using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Application.CQRS.ShorteningUrls.Commands;
using UrlShortener.Application.Models.Lookups;
using UrlShortener.Domain.Constants;
using UrlShortener.Mvc.Dto;

namespace UrlShortener.Mvc.Controllers.Api {
    public class ShortenController : BaseController {
        private readonly IMapper mapper;

        public ShortenController(IMapper mapper) {
            this.mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = Roles.User, AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ShortenedUrlLookup>> ShortenUrl([FromBody] ShortenUrlDto dto) {
            var command = mapper.Map<ShortenLinkCommand>(dto);
            command.RequesterId = UserId;

            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
