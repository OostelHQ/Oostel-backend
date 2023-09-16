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
    public class GetUserProfileByIdRequest : IRequest<APIResponse>
    {
        public string UserId { get; set; }
        public sealed class GetUserProfileByIdRequestCommand : IRequestHandler<GetUserProfileByIdRequest, APIResponse>
        {
            private readonly IUserProfilesService _userProfilesService;
            public GetUserProfileByIdRequestCommand(IUserProfilesService userProfilesService) =>
                _userProfilesService = userProfilesService;
            public async Task<APIResponse> Handle(GetUserProfileByIdRequest request, CancellationToken cancellationToken)
            {
                var userProfile = await _userProfilesService.GetUserProfileById(request.UserId);
                if (userProfile is null) return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.NotFound);

                return APIResponse.GetSuccessMessage(HttpStatusCode.OK, data: userProfile, ResponseMessages.FetchedSuccess);
            }
        }
    }
}
