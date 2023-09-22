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
    public class GetStudentByIdRequest : IRequest<APIResponse>
    {
        public string StudentId { get; set; }
        public sealed class GetStudentByIdRequestCommand : IRequestHandler<GetStudentByIdRequest, APIResponse>
        {
            private readonly IUserRolesProfilesService _userProfilesService;
            public GetStudentByIdRequestCommand(IUserRolesProfilesService userProfilesService) =>
                _userProfilesService = userProfilesService;
            public async Task<APIResponse> Handle(GetStudentByIdRequest request, CancellationToken cancellationToken)
            {
                var userProfile = await _userProfilesService.GetStudentById(request.StudentId);
                if (userProfile is null) return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.NotFound);

                return APIResponse.GetSuccessMessage(HttpStatusCode.OK, data: userProfile, ResponseMessages.FetchedSuccess);
            }
        }
    }
}
