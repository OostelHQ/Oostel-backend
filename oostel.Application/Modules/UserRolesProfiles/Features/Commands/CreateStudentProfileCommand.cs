using Mailjet.Client.Resources;
using MapsterMapper;
using Oostel.Application.Modules.UserProfiles.DTOs;
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
    public class CreateStudentProfileCommand : IRequest<APIResponse>
    {
        public string UserId { get; set; }
        public string StateOfOrigin { get; set; }
        public string Gender { get; set; }
        public string SchoolLevel { get; set; }
        public string Religion { get; set; }
        public string Age { get; set; }
        public string Hobby { get; set; }

        public sealed class CreateUserProfileCommandHandler : IRequestHandler<CreateStudentProfileCommand, APIResponse>
        {
            private readonly IUserRolesProfilesService _userProfilesService;
            private readonly IMapper _mapper;
            public CreateUserProfileCommandHandler(IUserRolesProfilesService userProfilesService, IMapper mapper)
            {
                _userProfilesService = userProfilesService;
                _mapper = mapper;
            }
            public async Task<APIResponse> Handle(CreateStudentProfileCommand request, CancellationToken cancellationToken)
            {
                var mapData = _mapper.Map<StudentProfileDTO>(request);
                var userProfile = await _userProfilesService.CreateStudentProfile(mapData);
                if(!userProfile) return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.FailedCreation);

                return APIResponse.GetSuccessMessage(HttpStatusCode.Created, data: null, ResponseMessages.SuccessfulCreation);
            }
        }
        
    }
}
