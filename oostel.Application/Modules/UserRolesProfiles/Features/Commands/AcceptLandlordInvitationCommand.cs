using MapsterMapper;
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

namespace Oostel.Application.Modules.UserRolesProfiles.Features.Commands
{
    public class AcceptLandlordInvitationCommand : IRequest<APIResponse>
    {
        public string LandlordId { get; set; }
        public string AgentId { get; set; }

        public sealed class AcceptLandlordInvitationCommandHandler : IRequestHandler<AcceptLandlordInvitationCommand, APIResponse>
        {
            private readonly IUserRolesProfilesService _userRolesProfilesService;
            public AcceptLandlordInvitationCommandHandler(IUserRolesProfilesService userRolesProfilesService, IMapper mapper) =>
                _userRolesProfilesService = userRolesProfilesService;

            public async Task<APIResponse> Handle(AcceptLandlordInvitationCommand request, CancellationToken cancellationToken)
            {
                var acceptInvite = await _userRolesProfilesService.AcceptLandlordInvitation(request.AgentId, request.LandlordId);

                if (!acceptInvite) return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.FailedCreation);

                return APIResponse.GetSuccessMessage(HttpStatusCode.Created, data: null, ResponseMessages.SuccessfulCreation);
            }
        }
    }
}
