using Oostel.Application.Modules.UserProfiles.Services;
using Oostel.Common.Constants;
using Oostel.Common.Types;
using System.Net;

namespace Oostel.Application.Modules.UserRolesProfiles.Features.Queries
{
    public class GetAnyUserDetailsCommand : IRequest<APIResponse>
    {
        public string UserId { get; set; }
        public sealed class GetAnyUserDetailsCommandHandler : IRequestHandler<GetAnyUserDetailsCommand, APIResponse>
        {
            private readonly IUserRolesProfilesService _userProfilesService;
            public GetAnyUserDetailsCommandHandler(IUserRolesProfilesService userProfilesService) =>
                _userProfilesService = userProfilesService;
            public async Task<APIResponse> Handle(GetAnyUserDetailsCommand request, CancellationToken cancellationToken)
            {
                var agentsProfile = await _userProfilesService.GetAnyUserProfile(request.UserId);
                if (agentsProfile is null) return APIResponse.GetFailureMessage(HttpStatusCode.OK, null, ResponseMessages.NotFound);

                return APIResponse.GetSuccessMessage(HttpStatusCode.OK, data: agentsProfile, ResponseMessages.FetchedSuccess);
            }
        }
    }
}
