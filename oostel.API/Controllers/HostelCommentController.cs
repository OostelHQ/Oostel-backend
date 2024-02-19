using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oostel.API.APIRoutes;
using Oostel.API.ViewModels.HostelsVM;
using Oostel.Application.Modules.Hostel.Features.Commands;
using Oostel.Application.Modules.Hostel.Features.Queries;
using Oostel.Common.Types;

namespace Oostel.API.Controllers
{
    public class HostelCommentController : BaseController
    {
        public readonly IMapper _mapper;
        public HostelCommentController(IMapper mapper)
        {
            _mapper = mapper;
        }


        [HttpPost]
        [Route(HostelRoute.CreateHostelComment)]
        public async Task<ActionResult<APIResponse>> CreateHostelComment(HostelCommentRequest request)
        {
            var mapData = _mapper.Map<CreateCommentCommand>(request);
            return HandleResult(await Mediator.Send(mapData));
        }

        [HttpPut]
        [Route(HostelRoute.UpdateHostelComment)]
        public async Task<ActionResult<APIResponse>> UpdateHostelComment(UpdateCommentRequest request)
        {
            var mapData = _mapper.Map<UpdateHostelCommentCommand>(request);
            return HandleResult(await Mediator.Send(mapData));
        }

        [HttpDelete]
        [Route(HostelRoute.DeleteHostelComment)]
        public async Task<ActionResult<APIResponse>> DeleteHostelComment(string commentId)
        {
            return HandleResult(await Mediator.Send(new DeleteHostelCommentCommand { CommentId = commentId}));
        }

        [HttpGet]
        [Route(HostelRoute.GetAllHostelComments)]
        public async Task<ActionResult<APIResponse>> GetAllHostelComments(string hostelId)
        {
            return HandleResult(await Mediator.Send(new GetCommentsRequest { HostelId = hostelId }));
        }
    }
}
