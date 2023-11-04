using MapsterMapper;
using Oostel.Application.Modules.UserProfiles.DTOs;
using Oostel.Application.Modules.UserProfiles.Services;
using Oostel.Application.Modules.UserRolesProfiles.DTOs;
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
    public class CreateAgentProfileCommand : IRequest<APIResponse>
    {
        public string UserId { get; set; }
        public string StateOfOrigin { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Religion { get; set; }
        public int Age { get; set; }

        public sealed class CreateAgentProfileCommandHandler : IRequestHandler<CreateAgentProfileCommand, APIResponse>
        {
            private readonly IUserRolesProfilesService _userProfilesService;
            private readonly IMapper _mapper;
            public CreateAgentProfileCommandHandler(IUserRolesProfilesService userProfilesService, IMapper mapper)
            {
                _userProfilesService = userProfilesService;
                _mapper = mapper;
            }
            public async Task<APIResponse> Handle(CreateAgentProfileCommand request, CancellationToken cancellationToken)
            {
                var mapData = _mapper.Map<CreateAgentProfileDTO>(request);
                var agentProfile = await _userProfilesService.CreateAgentProfile(mapData);
                if (!agentProfile) return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.FailedCreation);

                return APIResponse.GetSuccessMessage(HttpStatusCode.Created, data: null, ResponseMessages.SuccessfulCreation);
            }
        }
    }
}
