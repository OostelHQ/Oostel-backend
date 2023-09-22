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
    public class GetAllLandLordsRequest : IRequest<APIResponse>
    {
        public sealed class GetAllLandLordProfileRequestCommand : IRequestHandler<GetAllLandLordsRequest, APIResponse>
        {
            private readonly IUserRolesProfilesService _userProfilesService;
            public GetAllLandLordProfileRequestCommand(IUserRolesProfilesService userProfilesService) =>
                _userProfilesService = userProfilesService;
            public async Task<APIResponse> Handle(GetAllLandLordsRequest request, CancellationToken cancellationToken)
            {
                var landlordsProfile = await _userProfilesService.GetAllLandlords();
                if (landlordsProfile is null) return APIResponse.GetFailureMessage(HttpStatusCode.OK, null, ResponseMessages.NotFound);

                return APIResponse.GetSuccessMessage(HttpStatusCode.OK, data: landlordsProfile, ResponseMessages.FetchedSuccess);
            }
        }
    }
}
