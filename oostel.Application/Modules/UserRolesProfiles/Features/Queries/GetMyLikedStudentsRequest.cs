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
    public class GetMyLikedStudentsRequest: IRequest<APIResponse>
    {
        public string UserId { get; set; }

        public sealed class GetMyLikedStudentsRequestCommand : IRequestHandler<GetMyLikedStudentsRequest, APIResponse>
        {
            private readonly IUserRolesProfilesService _userRolesProfilesService;
            public GetMyLikedStudentsRequestCommand(IUserRolesProfilesService userRolesProfilesService) => _userRolesProfilesService = userRolesProfilesService;

            public async Task<APIResponse> Handle(GetMyLikedStudentsRequest request, CancellationToken cancellationToken)
            {
                var LikedStudents = await _userRolesProfilesService.GetMyLikedStudents(request.UserId);

                if (LikedStudents is null)
                    return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.NotFound);

                return APIResponse.GetSuccessMessage(HttpStatusCode.OK, data: LikedStudents, ResponseMessages.FetchedSuccess);
            }

        }
        
    }
}
