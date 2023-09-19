using MapsterMapper;
using Oostel.Application.Modules.UserProfiles.DTOs;
using Oostel.Application.Modules.UserProfiles.Services;
using Oostel.Common.Constants;
using Oostel.Common.Types;
using System.Net;

namespace Oostel.Application.Modules.UserProfiles.Features.Commands
{
    public class UpdateUserProfileCommand : IRequest<APIResponse>
    {
        public string UserId { get; set; }
        public string? StateOfOrigin { get; set; }
        public string? Gender { get; set; }
        public string? SchoolLevel { get; set; }
        public string? Religion { get; set; }
        public int Age { get; set; }
        public string? Hobby { get; set; }

        public sealed class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, APIResponse>
        {
            private readonly IUserProfilesService _userProfilesService;
            private readonly IMapper _mapper;
            public UpdateUserProfileCommandHandler(IUserProfilesService userProfilesService, IMapper mapper)
            {
                _userProfilesService = userProfilesService;
                _mapper = mapper;
            }
            public async Task<APIResponse> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
            {
                var mapData = _mapper.Map<UserProfileDTO>(request);
                var userProfile = await _userProfilesService.UpdateUserProfile(mapData);
                if (!userProfile) return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.FailToUpdateError);

                return APIResponse.GetSuccessMessage(HttpStatusCode.Created, data: null, ResponseMessages.SuccessfulCreation);
            }
        }
    }
}
