using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Oostel.API.APIRoutes;
using Oostel.API.ViewModels.UserProfilesVM;
using Oostel.Application.Modules.UserProfiles.Features.Commands;
using Oostel.Application.Modules.UserProfiles.Features.Queries;
using Oostel.Application.Modules.UserRolesProfiles.Features.Commands;
using Oostel.Common.Types;
using Oostel.Common.Types.RequestFeatures;

namespace Oostel.API.Controllers
{
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
        public async Task<ActionResult<APIResponse>> UpdateUserProfile(StudentProfileRequest request)
        {
            var studentProfileRequest = _mapper.Map<UpdateStudentProfileCommand>(request);
            return HandleResult(await Mediator.Send(studentProfileRequest));
        }

        [HttpGet]
        [Route(UserProfileRoute.GetAllStudents)]
        public async Task<ActionResult<APIResponse>> GetAllStudents([FromQuery]StudentTypeParams studentTypeParams)
        {
            return HandlePagedResult(await Mediator.Send(new GetAllStudentsRequest { StudentTypeParams = studentTypeParams}));
        }

        [HttpGet]
        [Route(UserProfileRoute.GetStudentById)]
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
    }
}
