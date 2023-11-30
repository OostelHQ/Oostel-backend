using MediatR;
using Microsoft.AspNetCore.Mvc;
using Oostel.API.Extensions;
using Oostel.Common.Helpers;
using Oostel.Common.Types;
using Oostel.Common.Types.RequestFeatures;

namespace Oostel.API.Controllers
{
    [Route("api/")]
    [ApiController]
    //[ResponseCache(CacheProfileName = "120SecondsDuration")]
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

        protected ActionResult HandlePagedResult<T>(ResultResponse<PagedList<T>> result)
        {
            if (result == null) return NotFound();
            if (result.IsSuccess && result.Data != null)
            {
                Response.AddPaginationHeader(result.Data.MetaData.CurrentPage, result.Data.MetaData.PageSize,
                    result.Data.MetaData.TotalCount, result.Data.MetaData.TotalPages);

                return Ok(result.Data);
            }
            if (result.IsSuccess && result.Data == null)
                return NotFound();
            return BadRequest(result.Error);
        }

    }
}
