using Oostel.Application.Modules.UserAuthentication.Services;
using Oostel.Application.Modules.UserProfiles.Features.Queries;
using Oostel.Application.Modules.UserProfiles.Services;
using Oostel.Common.Constants;
using Oostel.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserAuthentication.Features.Queries
{
    public class GetCurrentUserRequest: IRequest<APIResponse>
    {

        public sealed class GetCurrentUserRequestCommand : IRequestHandler<GetCurrentUserRequest, APIResponse>
        {
            private readonly IUserAuthenticationService _userAuthenticationService;
            public GetCurrentUserRequestCommand(IUserAuthenticationService userAuthenticationService) =>
                _userAuthenticationService = userAuthenticationService;
            public async Task<APIResponse> Handle(GetCurrentUserRequest request, CancellationToken cancellationToken)
            {
                var currentUser = await _userAuthenticationService.GetCurrentUser();
                if (currentUser is null) return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.NotFound);

                return APIResponse.GetSuccessMessage(HttpStatusCode.OK, data: currentUser, ResponseMessages.FetchedSuccess);
            }
        }
    }
}
