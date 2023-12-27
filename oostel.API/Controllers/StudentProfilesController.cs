using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oostel.API.APIRoutes;
using Oostel.API.ViewModels.UserProfilesVM;
using Oostel.Application.Modules.UserProfiles.Features.Commands;
using Oostel.Application.Modules.UserProfiles.Features.Queries;
using Oostel.Application.Modules.UserRolesProfiles.Features.Commands;
using Oostel.Application.Modules.UserRolesProfiles.Features.Queries;
using Oostel.Common.Types;
using Oostel.Common.Types.RequestFeatures;

namespace Oostel.API.Controllers
{
    [Authorize]
    public class StudentProfilesController : BaseController
    {

        private readonly IMapper _mapper;
        public StudentProfilesController(IMapper mapper) => _mapper = mapper;

        [HttpPost]
        [Route(UserProfileRoute.CreateUserProfile)]
        public async Task<ActionResult<APIResponse>> CreateUserProfile(StudentProfileRequest request)
        {
            var userProfileRequest = _mapper.Map<CreateStudentProfileCommand>(request);
            return HandleResult(await Mediator.Send(userProfileRequest));
        }

        [HttpPut]
        [Route(UserProfileRoute.UpdateStudentProfile)]
        public async Task<ActionResult<APIResponse>> UpdateUserProfile(UpdateStudentProfileRequest request)
        {
            var studentProfileRequest = _mapper.Map<UpdateStudentProfileCommand>(request);
            return HandleResult(await Mediator.Send(studentProfileRequest));
        }

        [HttpGet]
        [Route(UserProfileRoute.GetAllStudents)]
        [ResponseCache(Duration = 60)]
        [AllowAnonymous]
        public async Task<ActionResult<APIResponse>> GetAllStudents([FromQuery]StudentTypeParams studentTypeParams)
        {
            return HandlePagedResult(await Mediator.Send(new GetAllStudentsRequest { StudentTypeParams = studentTypeParams}));
        }

        [HttpGet]
        [Route(UserProfileRoute.GetStudentById)]
        [ResponseCache(Duration = 60)]
        [AllowAnonymous]
        public async Task<ActionResult<APIResponse>> GetUserProfileById(string studentId)
        {
            return HandleResult(await Mediator.Send(new GetStudentByIdRequest { StudentId = studentId }));
        }

        [HttpPut]
        [Route(UserProfileRoute.UploadUserProfilePicture)]
        public async Task<ActionResult<APIResponse>> UploadUserProfilePicture([FromForm] UploadUserprofilePictureRequest request)
        {
            return HandleResult(await Mediator.Send(new UploadUserProfilePictureCommand { File = request.File, UserId = request.UserId }));
        }


        [HttpPost]
        [Route(UserProfileRoute.OpenToRoommate)]
        public async Task<ActionResult<APIResponse>> OpenToRoommate(OpenToRoommateRequest request)
        {
            var openToRoommateRequest = _mapper.Map<OpenToRoommateCommand>(request);
            return HandleResult(await Mediator.Send(openToRoommateRequest));
        }

        [HttpPost]
        [Route(UserProfileRoute.ProfileViewsCount)]
        public async Task<ActionResult<APIResponse>> ProfileViewsCount(string userId)
        {
            return HandleResult(await Mediator.Send(new ProfileViewsCountCommand { UserId = userId}));
        }

        [HttpPost]
        [Route(UserProfileRoute.AddStudentLikes)]
        public async Task<ActionResult<APIResponse>> AddStudentLikes(StudentLikesRequest request)
        {
            return HandleResult(await Mediator.Send(new AddStudentLikesCommand {LikingUserId = request.LikingUserId, StudentLikeId = request.StudentLikeId }));
        }

        [HttpGet]
        [Route(UserProfileRoute.GetMyLikedStudents)]
        public async Task<ActionResult<APIResponse>> GetMyLikedStudents(string userId)
        {
            return HandleResult(await Mediator.Send(new GetMyLikedStudentsRequest {UserId = userId}));
        }

        [HttpGet]
        [Route(UserProfileRoute.GetAStudentLikedUsers)]
        public async Task<ActionResult<APIResponse>> GetAStudentLikedUsers(string studentId)
        {
            return HandleResult(await Mediator.Send(new GetAStudentLikedUsersRequest { StudentId = studentId }));
        }
    }

}
