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
    public class DeleteUserAccountCommand : IRequest<APIResponse>
    {
        public string UserId { get; set; }

        public sealed class DeleteUserAccountCommandHandler : IRequestHandler<DeleteUserAccountCommand, APIResponse>
        {
            private readonly IUserRolesProfilesService _userRolesProfilesService;
            public DeleteUserAccountCommandHandler(IUserRolesProfilesService userRolesProfilesService)
            {
                _userRolesProfilesService = userRolesProfilesService;
            }
            public async Task<APIResponse> Handle(DeleteUserAccountCommand request, CancellationToken cancellationToken)
            {
                var result = await _userRolesProfilesService.DeleteUserAccountAsync(request.UserId);
                if (!result)
                {
                    return APIResponse.GetFailureMessage(HttpStatusCode.NotFound, null, ResponseMessages.NotFound);
                }

                return APIResponse.GetSuccessMessage(HttpStatusCode.OK, data: null, ResponseMessages.DeleteUserAccountErrorMessage);
            }
        }
    }
}
