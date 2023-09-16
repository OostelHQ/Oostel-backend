using MapsterMapper;
using Oostel.Application.Modules.UserProfiles.DTOs;
using Oostel.Application.Modules.UserProfiles.Features.Commands;
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
    public class GetAllUserProfileRequest : IRequest<APIResponse>
    {
        public sealed class GetAllUserProfileRequestCommand : IRequestHandler<GetAllUserProfileRequest, APIResponse>
        {
            private readonly IUserProfilesService _userProfilesService;
            public GetAllUserProfileRequestCommand(IUserProfilesService userProfilesService) => 
                _userProfilesService = userProfilesService;
            public async Task<APIResponse> Handle(GetAllUserProfileRequest request, CancellationToken cancellationToken)
            {
                var userProfile = await _userProfilesService.GetAllUserProfile();
                if (userProfile is null) return APIResponse.GetFailureMessage(HttpStatusCode.OK, null, ResponseMessages.NotFound);

                return APIResponse.GetSuccessMessage(HttpStatusCode.OK, data: userProfile, ResponseMessages.FetchedSuccess);
            }
        }
    }
}
