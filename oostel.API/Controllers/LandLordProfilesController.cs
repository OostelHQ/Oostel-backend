using MapsterMapper;
using Marvin.Cache.Headers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oostel.API.APIRoutes;
using Oostel.API.ViewModels.UserProfilesVM;
using Oostel.Application.Modules.UserProfiles.Features.Commands;
using Oostel.Application.Modules.UserProfiles.Features.Queries;
using Oostel.Application.Modules.UserRolesProfiles.Features.Commands;
using Oostel.Common.Types;

namespace Oostel.API.Controllers
{
    [Authorize]
    public class LandLordProfilesController : BaseController
    {
        private readonly IMapper _mapper;
        public LandLordProfilesController(IMapper mapper) => _mapper = mapper;

        [HttpPost]
        [Route(UserProfileRoute.CreateLandlordProfile)]
        [Authorize(Roles = "LandLord")]
        public async Task<ActionResult<APIResponse>> CreateLandlordProfile(CreateLandlordRequest request)
        {
            var landlordProfileRequest = _mapper.Map<CreateLandlordProfileCommand>(request);
            return HandleResult(await Mediator.Send(landlordProfileRequest));
        }

        [HttpPut]
        [Route(UserProfileRoute.UpdateLandlordProfile)]
        [Authorize(Roles = "LandLord")]
        public async Task<ActionResult<APIResponse>> UpdateLandlordProfile(LandlordProfileRequest request)
        {
            var landlordProfileRequest = _mapper.Map<UpdateLandLordProfileCommand>(request);
            return HandleResult(await Mediator.Send(landlordProfileRequest));
        }

        [HttpGet]
        [Route(UserProfileRoute.GetAllLandlords)]
        [ResponseCache(Duration = 60)]
        public async Task<ActionResult<APIResponse>> GetAllLandlords()
        {
            return HandleResult(await Mediator.Send(new GetAllLandLordsRequest()));
        }

        [HttpGet]
        [Route(UserProfileRoute.GetLandlordById)]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<ActionResult<APIResponse>> GetLandlordById(string landlordId)
        {
            return HandleResult(await Mediator.Send(new GetLandlordByIdRequest { LandlordId = landlordId }));
        }

        [HttpPost]
        [Route(UserProfileRoute.SendAgentInvitationCode)]
        [Authorize(Roles = "LandLord")]
        public async Task<ActionResult<APIResponse>> SendAgentInvitationCode(SendInvitationRequest request)
        {
            var invitationRequest = _mapper.Map<InviteAgentCommand>(request);
            return HandleResult(await Mediator.Send(invitationRequest));
        }


        [HttpPost]
        [Route(UserProfileRoute.CreateAgentProfile)]
        [Authorize(Roles = "Agent")]
        public async Task<ActionResult<APIResponse>> CreateAgentProfile(CreateAgentRequest request)
        {
            var agentProfileRequest = _mapper.Map<CreateAgentProfileCommand>(request);
            return HandleResult(await Mediator.Send(agentProfileRequest));
        }

        [HttpPut]
        [Route(UserProfileRoute.UpdateAgentProfile)]
        [Authorize(Roles = "Agent")]
        public async Task<ActionResult<APIResponse>> UpdateAgentProfile(AgentProfileRequest request)
        {
            var agentProfileRequest = _mapper.Map<UpdateAgentProfileCommand>(request);
            return HandleResult(await Mediator.Send(agentProfileRequest));
        }

        [HttpGet]
        [Route(UserProfileRoute.GetAllAgents)]
        [ResponseCache(Duration = 60)]
        public async Task<ActionResult<APIResponse>> GetAllAgents()
        {
            return HandleResult(await Mediator.Send(new GetAllAgentsRequest()));
        }

        [HttpGet]
        [Route(UserProfileRoute.GetAgentById)]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<ActionResult<APIResponse>> GetAgentById(string agentId)
        {
            return HandleResult(await Mediator.Send(new GetAgentByIdRequest { AgentId = agentId }));
        }
    }
}
