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
    public class GetLandlordByIdRequest : IRequest<APIResponse>
    {
        public string LandlordId { get; set; }
        public sealed class GetLandlordByIdRequestCommand : IRequestHandler<GetLandlordByIdRequest, APIResponse>
        {
            private readonly IUserProfilesService _userProfilesService;
            public GetLandlordByIdRequestCommand(IUserProfilesService userProfilesService) =>
                _userProfilesService = userProfilesService;
            public async Task<APIResponse> Handle(GetLandlordByIdRequest request, CancellationToken cancellationToken)
            {
                var landlordProfile = await _userProfilesService.GetLandlordsById(request.LandlordId);
                if (landlordProfile is null) return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.NotFound);

                return APIResponse.GetSuccessMessage(HttpStatusCode.OK, data: landlordProfile, ResponseMessages.FetchedSuccess);
            }
        }
    }
}
