using Oostel.Application.Modules.UserProfiles.Services;
using Oostel.Common.Constants;
using Oostel.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserProfiles.Features.Queries
{
    public class GetAgentByIdRequest : IRequest<APIResponse>
    {
        public string AgentId { get; set; }
        public sealed class GetAgentByIdRequestCommand : IRequestHandler<GetAgentByIdRequest, APIResponse>
        {
            private readonly IUserRolesProfilesService _userProfilesService;
            public GetAgentByIdRequestCommand(IUserRolesProfilesService userProfilesService) =>
                _userProfilesService = userProfilesService;
            public async Task<APIResponse> Handle(GetAgentByIdRequest request, CancellationToken cancellationToken)
            {
                var agentProfile = await _userProfilesService.GetAgentById(request.AgentId);
                if (agentProfile is null) return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.NotFound);

                return APIResponse.GetSuccessMessage(HttpStatusCode.OK, data: agentProfile, ResponseMessages.FetchedSuccess);
            }
        }
    }
}
