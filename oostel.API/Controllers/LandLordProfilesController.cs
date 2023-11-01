using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Oostel.API.APIRoutes;
using Oostel.API.ViewModels.UserProfilesVM;
using Oostel.Application.Modules.UserProfiles.Features.Commands;
using Oostel.Application.Modules.UserProfiles.Features.Queries;
using Oostel.Application.Modules.UserRolesProfiles.Features.Commands;
using Oostel.Common.Types;

namespace Oostel.API.Controllers
{
    public class LandLordProfilesController : BaseController
    {
        private readonly IMapper _mapper;
        public LandLordProfilesController(IMapper mapper) => _mapper = mapper;

        [HttpPost]
        [Route(UserProfileRoute.CreateLandlordProfile)]
        public async Task<ActionResult<APIResponse>> CreateLandlordProfile(CreateLandlordRequest request)
        {
            var landlordProfileRequest = _mapper.Map<CreateLandlordProfileCommand>(request);
            return HandleResult(await Mediator.Send(landlordProfileRequest));
        }

        [HttpPut]
        [Route(UserProfileRoute.UpdateLandlordProfile)]
        public async Task<ActionResult<APIResponse>> UpdateLandlordProfile(LandlordProfileRequest request)
        {
            var landlordProfileRequest = _mapper.Map<UpdateLandLordProfileCommand>(request);
            return HandleResult(await Mediator.Send(landlordProfileRequest));
        }

        [HttpGet]
        [Route(UserProfileRoute.GetAllLandlords)]
        public async Task<ActionResult<APIResponse>> GetAllLandlords()
        {
            return HandleResult(await Mediator.Send(new GetAllLandLordsRequest()));
        }

        [HttpGet]
        [Route(UserProfileRoute.GetLandlordById)]
        public async Task<ActionResult<APIResponse>> GetLandlordById(string landlordId)
        {
            return HandleResult(await Mediator.Send(new GetLandlordByIdRequest { LandlordId = landlordId }));
        }

        [HttpPost]
        [Route(UserProfileRoute.SendAgentInvitationCode)]
        public async Task<ActionResult<APIResponse>> SendAgentInvitationCode(SendInvitationRequest request)
        {
            var invitationRequest = _mapper.Map<InviteAgentCommand>(request);
            return HandleResult(await Mediator.Send(invitationRequest));
        }
    }
}
