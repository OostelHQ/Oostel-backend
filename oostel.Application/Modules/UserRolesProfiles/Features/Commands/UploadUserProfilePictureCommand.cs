using Microsoft.AspNetCore.Http;
using Oostel.Application.Modules.UserProfiles.Services;
using Oostel.Common.Constants;
using Oostel.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserProfiles.Features.Commands
{
    public class UploadUserProfilePictureCommand : IRequest<APIResponse>
    {
        public IFormFile File { get; set; }
        public string UserId { get; set; }

        public sealed class UploadUserProfilePictureCommandHandler : IRequestHandler<UploadUserProfilePictureCommand, APIResponse>
        {
            private readonly IUserRolesProfilesService _userProfilesService;
            public UploadUserProfilePictureCommandHandler(IUserRolesProfilesService userProfileService)
            {
                _userProfilesService = userProfileService;
            }
            public async Task<APIResponse> Handle(UploadUserProfilePictureCommand request, CancellationToken cancellationToken)
            {
                var isPictureUploaded = await _userProfilesService.UploadDisplayPictureAsync(request.File, request.UserId);

                if (!isPictureUploaded)
                {
                    return APIResponse.GetFailureMessage(HttpStatusCode.NotFound, null, ResponseMessages.NotFound);
                }

                return APIResponse.GetSuccessMessage(HttpStatusCode.OK, data: ResponseMessages.SuccessfulCreation, ResponseMessages.SuccessfulCreation);
            }
        }
    }

}
