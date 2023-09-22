using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Oostel.API.APIRoutes;
using Oostel.API.ViewModels.UserProfilesVM;
using Oostel.Application.Modules.UserProfiles.Features.Commands;
using Oostel.Application.Modules.UserProfiles.Features.Queries;
using Oostel.Common.Types;

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
        public async Task<ActionResult<APIResponse>> GetAllStudents()
        {
            return HandleResult(await Mediator.Send(new GetAllStudentsRequest()));
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

    }
}
