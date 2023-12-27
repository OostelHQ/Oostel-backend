using MapsterMapper;
using Oostel.Application.Modules.UserProfiles.DTOs;
using Oostel.Application.Modules.UserProfiles.Services;
using Oostel.Application.Modules.UserRolesProfiles.DTOs;
using Oostel.Common.Constants;
using Oostel.Common.Types;
using System.Net;

namespace Oostel.Application.Modules.UserProfiles.Features.Commands
{
    public class UpdateStudentProfileCommand : IRequest<APIResponse>
    {
        public string UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? StateOfOrigin { get; set; }
        public string? Gender { get; set; }
        public string? SchoolLevel { get; set; }
        public string? Religion { get; set; }
        public string? Denomination { get; set; }
        public string? Age { get; set; }
        public string? Hobby { get; set; }
        public string? GuardianPhoneNumber { get; set; }
        public sealed class UpdateUserProfileCommandHandler : IRequestHandler<UpdateStudentProfileCommand, APIResponse>
        {
            private readonly IUserRolesProfilesService _userProfilesService;
            private readonly IMapper _mapper;
            public UpdateUserProfileCommandHandler(IUserRolesProfilesService userProfilesService, IMapper mapper)
            {
                _userProfilesService = userProfilesService;
                _mapper = mapper;
            }
            public async Task<APIResponse> Handle(UpdateStudentProfileCommand request, CancellationToken cancellationToken)
            {
                var mapData = _mapper.Map<UpdateStudentDTO>(request);
                var studentProfile = await _userProfilesService.UpdateStudentProfile(mapData);
                if (studentProfile is null) return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.FailToUpdateError);

                return APIResponse.GetSuccessMessage(HttpStatusCode.Created, data: studentProfile, ResponseMessages.UpdateMessage);
            }
        }
    }
}
