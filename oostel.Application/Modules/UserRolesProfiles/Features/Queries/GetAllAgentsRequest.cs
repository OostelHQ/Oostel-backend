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
    public class GetAllAgentsRequest : IRequest<APIResponse>
    {
        public sealed class GetAllAgentsRequestCommand : IRequestHandler<GetAllAgentsRequest, APIResponse>
        {
            private readonly IUserRolesProfilesService _userProfilesService;
            public GetAllAgentsRequestCommand(IUserRolesProfilesService userProfilesService) =>
                _userProfilesService = userProfilesService;
            public async Task<APIResponse> Handle(GetAllAgentsRequest request, CancellationToken cancellationToken)
            {
                var agentsProfile = await _userProfilesService.GetAllAgents();
                if (agentsProfile is null) return APIResponse.GetFailureMessage(HttpStatusCode.OK, null, ResponseMessages.NotFound);

                return APIResponse.GetSuccessMessage(HttpStatusCode.OK, data: agentsProfile, ResponseMessages.FetchedSuccess);
            }
        }
    }
}
