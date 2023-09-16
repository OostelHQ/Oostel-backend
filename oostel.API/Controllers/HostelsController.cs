using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Oostel.API.APIRoutes;
using Oostel.API.ViewModels.HostelsVM;
using Oostel.API.ViewModels.UserProfilesVM;
using Oostel.Application.Modules.Hostel.Features.Commands;
using Oostel.Application.Modules.Hostel.Features.Queries;
using Oostel.Application.Modules.UserProfiles.Features.Commands;
using Oostel.Application.Modules.UserProfiles.Features.Queries;
using Oostel.Common.Types;

namespace Oostel.API.Controllers
{
    public class HostelsController : BaseController
    {
        private readonly IMapper _mapper;
        public HostelsController(IMapper mapper) => _mapper = mapper;

        [HttpPost]
        [Route(HostelRoute.CreateHostel)]
        public async Task<ActionResult<APIResponse>> CreateHostel(HostelRequest request)
        {
            var hostelRequest = _mapper.Map<CreateHostelCommand>(request);
            return HandleResult(await Mediator.Send(hostelRequest));
        }

        [HttpGet]
        [Route(HostelRoute.GetAllHostels)]
        public async Task<ActionResult<APIResponse>> GetAllHostels()
        {
            return HandleResult(await Mediator.Send(new GetAllHostelsRequest()));
        }

        [HttpGet]
        [Route(HostelRoute.GetHostelById)]
        public async Task<ActionResult<APIResponse>> GetHostelById(string hostelId)
        {
            return HandleResult(await Mediator.Send(new GetHostelByIdRequest { HostelId = hostelId}));
        }
    }
}
