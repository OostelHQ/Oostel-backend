using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Oostel.API.APIRoutes;
using Oostel.API.ViewModels.UserProfilesVM;
using Oostel.Application.Modules.UserProfiles.Features.Commands;
using Oostel.Application.Modules.UserProfiles.Features.Queries;
using Oostel.Common.Types;

namespace Oostel.API.Controllers
{
    public class UserProfilesController : BaseController
    {

        private readonly IMapper _mapper;
        public UserProfilesController(IMapper mapper) => _mapper = mapper;

        [HttpPost]
        [Route(UserProfileRoute.CreateUserProfile)]
        public async Task<ActionResult<APIResponse>> CreateUserProfile(UserProfileRequest request)
        {
            var userProfileRequest = _mapper.Map<CreateUserProfileCommand>(request);
            return HandleResult(await Mediator.Send(userProfileRequest));
        }

        [HttpPut]
        [Route(UserProfileRoute.UpdateUserProfile)]
        public async Task<ActionResult<APIResponse>> UpdateUserProfile(UserProfileRequest request)
        {
            var userProfileRequest = _mapper.Map<UpdateUserProfileCommand>(request);
            return HandleResult(await Mediator.Send(userProfileRequest));
        }

        [HttpGet]
        [Route(UserProfileRoute.GetAllUserProfiles)]
        public async Task<ActionResult<APIResponse>> GetAllUserProfiles()
        {
            return HandleResult(await Mediator.Send(new GetAllUserProfileRequest()));
        }

        [HttpGet]
        [Route(UserProfileRoute.GetUserProfileById)]
        public async Task<ActionResult<APIResponse>> GetUserProfileById(string userId)
        {
            return HandleResult(await Mediator.Send(new GetUserProfileByIdRequest { UserId = userId }));
        }

        [HttpPut]
        [Route(UserProfileRoute.UploadUserProfilePicture)]
        public async Task<ActionResult<APIResponse>> UploadUserProfilePicture([FromForm] UploadUserprofilePictureRequest request)
        {
            return HandleResult(await Mediator.Send(new UploadUserProfilePictureCommand { File = request.File, UserId = request.UserId }));
        }

    }
}
