using MapsterMapper;
using Oostel.Application.Modules.UserProfiles.DTOs;
using Oostel.Application.Modules.UserProfiles.Services;
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
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? StateOfOrigin { get; set; }
        public string? Gender { get; set; }
        public string? SchoolLevel { get; set; }
        public string? Religion { get; set; }
        public string? Denomination { get; set; }
        public string? Age { get; set; }
        public string? Hobby { get; set; }

        public sealed class UpdateUserProfileCommandHandler : IRequestHandler<UpdateStudentProfileCommand, APIResponse>
        {
            private readonly IUserProfilesService _userProfilesService;
            private readonly IMapper _mapper;
            public UpdateUserProfileCommandHandler(IUserProfilesService userProfilesService, IMapper mapper)
            {
                _userProfilesService = userProfilesService;
                _mapper = mapper;
            }
            public async Task<APIResponse> Handle(UpdateStudentProfileCommand request, CancellationToken cancellationToken)
            {
                var mapData = _mapper.Map<UpdateStudentProfileDTO>(request);
                var studentProfile = await _userProfilesService.UpdateStudentProfile(mapData);
                if (!studentProfile) return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.FailToUpdateError);

                return APIResponse.GetSuccessMessage(HttpStatusCode.Created, data: null, ResponseMessages.SuccessfulCreation);
            }
        }
    }
}
