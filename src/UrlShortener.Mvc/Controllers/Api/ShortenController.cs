﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Application.CQRS.ShorteningUrls.Commands;
using UrlShortener.Application.CQRS.ShorteningUrls.Commands.RemoveLink;
using UrlShortener.Application.CQRS.ShorteningUrls.Queries;
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
        [HttpDelete("{hash_id}")]
        [Authorize(Roles = $"{Roles.User},{Roles.Administrator}", AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ShortenedUrlLookup>> DeleteShortenUrl([FromRoute] string hash_id) {
            var command = new RemoveLinkCommand() {
                LinkId = hash_id,
                RequestedId = UserId
            };

            await Mediator.Send(command);
            return Ok(hash_id);
        }

        [HttpGet]
        public async Task<ActionResult<IList<ShortenedUrlLookup>>> GetAllUrls() {
            var command = new GetShortUrlsListQuery();

            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
