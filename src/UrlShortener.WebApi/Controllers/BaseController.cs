using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.WebApi.Common.Exceptions;

namespace UrlShortener.WebApi.Controllers {
    [ApiController]
    public abstract class BaseController : ControllerBase {
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
    }
}
