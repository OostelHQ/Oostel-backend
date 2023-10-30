using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oostel.API.APIRoutes;
using Oostel.API.ViewModels.UserAuthenticationsVM;
using Oostel.Application.Modules.UserAuthentication.Features.Commands;
using Oostel.Common.Types;

namespace Oostel.API.Controllers
{
    [AllowAnonymous]
    public class AuthenticationsController : BaseController
    {
        private readonly IMapper _mapper;
        public AuthenticationsController(IMapper mapper) => _mapper = mapper;

        [HttpPost()]
        [Route(UserAuthenticationRoute.RegisterUser)]
        public async Task<ActionResult<APIResponse>> RegisterUser([FromForm] RegisterUserRequest request)
        {
            var registerRequest = _mapper.Map<RegisterUserCommand>(request);
            return HandleResult(await Mediator.Send(registerRequest));
        }

        [HttpPost()]
        [Route(UserAuthenticationRoute.LoginUser)]
        public async Task<ActionResult<APIResponse>> LoginUser(LoginUserRequest request)
        {
            var loginRequest = _mapper.Map<LoginUserCommand>(request);
            return HandleResult(await Mediator.Send(loginRequest));
        }


        [HttpPost()]
        [Route(UserAuthenticationRoute.VerifyOTPEmail)]
        public async Task<ActionResult<APIResponse>> VerifyUserOTPEmail(VerifyOTPRequest request)
        {
            var verifyUserOTP = _mapper.Map<VerifyUserOTPFromEmailCommand>(request);
            return HandleResult(await Mediator.Send(verifyUserOTP));
        }

        [HttpPost()]
        [Route(UserAuthenticationRoute.SendResetPasswordOTP)]
        public async Task<ActionResult<APIResponse>> SendResetPasswordOTP(SendResetPasswordRequest request)
        {
            var sendResetPasswordOTP = _mapper.Map<SendResetPasswordEmailCommand>(request);
            return HandleResult(await Mediator.Send(sendResetPasswordOTP));
        }

        [HttpPost()]
        [Route(UserAuthenticationRoute.ResetPassword)]
        public async Task<ActionResult<APIResponse>> ResetPassword(ResetPasswordRequest request)
        {
            var resetPasswordOTP = _mapper.Map<ResetPasswordCommand>(request);
            return HandleResult(await Mediator.Send(resetPasswordOTP));
        }

    }
}
