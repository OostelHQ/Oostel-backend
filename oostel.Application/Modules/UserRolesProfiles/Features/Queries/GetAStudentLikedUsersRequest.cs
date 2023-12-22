using Oostel.Application.Modules.Hostel.Features.Queries;
using Oostel.Application.Modules.Hostel.Services;
using Oostel.Application.Modules.UserProfiles.Services;
using Oostel.Common.Constants;
using Oostel.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserRolesProfiles.Features.Queries
{
    public class GetAStudentLikedUsersRequest : IRequest<APIResponse>
    {
        public string StudentId { get; set; }

        public sealed class GetAStudentLikedUsersRequestCommand : IRequestHandler<GetAStudentLikedUsersRequest, APIResponse>
        {
            private readonly IUserRolesProfilesService _userRolesProfilesService;
            public GetAStudentLikedUsersRequestCommand(IUserRolesProfilesService userRolesProfilesService) => _userRolesProfilesService = userRolesProfilesService;

            public async Task<APIResponse> Handle(GetAStudentLikedUsersRequest request, CancellationToken cancellationToken)
            {
                var LikedUsers = await _userRolesProfilesService.GetAStudentLikedUsers(request.StudentId);

                if (LikedUsers is null)
                    return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.NotFound);

                return APIResponse.GetSuccessMessage(HttpStatusCode.OK, data: LikedUsers, ResponseMessages.FetchedSuccess);
            }
        }
    }
}
