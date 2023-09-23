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

namespace Oostel.Application.Modules.UserRolesProfiles.Features.Commands
{
    public class ProfileViewsCountCommand : IRequest<APIResponse>
    {
        public string UserId { get; set; }
        public sealed class ProfileViewsCountCommandHandler : IRequestHandler<ProfileViewsCountCommand, APIResponse>
        {
            private readonly IUserRolesProfilesService _userProfilesService;
            public ProfileViewsCountCommandHandler(IUserRolesProfilesService userProfilesService) =>
                _userProfilesService = userProfilesService;

            public async Task<APIResponse> Handle(ProfileViewsCountCommand request, CancellationToken cancellationToken)
            {
                var profileViewsCount = await _userProfilesService.ProfileViewsCount(request.UserId);
                if (!profileViewsCount) return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.FailToUpdateError);

                return APIResponse.GetSuccessMessage(HttpStatusCode.Created, data: null, ResponseMessages.SuccessfulCreation);
            }
        }
    }
}
