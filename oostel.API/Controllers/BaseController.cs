using MediatR;
using Microsoft.AspNetCore.Mvc;
using Oostel.Common.Types;

namespace Oostel.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class BaseController : ControllerBase
    {

        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices
            .GetService<IMediator>();

        protected ActionResult HandleResult(APIResponse result)
        {
            if (result == null)
            {
                return NotFound(new APIResponse
                {
                    IsSuccessful = result.IsSuccessful,
                    Message = result.Message,
                    Data = result.Data,
                    StatusCode = result.StatusCode
                });
            }

            return result.IsSuccessful
                ? (result.IsSuccessful
                ? Ok(new APIResponse
                {
                    IsSuccessful = result.IsSuccessful,
                    Message = result.Message,
                    Data = result.Data,
                    StatusCode = result.StatusCode
                })
                : NotFound(new APIResponse
                {
                    IsSuccessful = result.IsSuccessful,
                    Message = result.Message,
                    Data = result.Data,
                    StatusCode = result.StatusCode
                }))
                : BadRequest(new APIResponse
                {
                    IsSuccessful = result.IsSuccessful,
                    Message = result.Message,
                    Data = result.Data,
                    StatusCode = result.StatusCode
                });
        }

    }
}
