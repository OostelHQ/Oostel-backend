using Microsoft.AspNetCore.Identity;
using Oostel.Application.Modules.UserAuthentication.Services;
using Oostel.Common.Constants;
using Oostel.Common.Types;
using Oostel.Domain.UserAuthentication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserAuthentication.Features.Commands
{
    public class ResetPasswordCommand : IRequest<APIResponse>
    {
        public string Email { get; set; }
        public string OTP { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }


        public sealed class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, APIResponse>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IUserAuthenticationService _userAuthenticationService;

            public ResetPasswordCommandHandler(UserManager<ApplicationUser> userManager, IUserAuthenticationService userAuthenticationService)
            {
                _userManager = userManager;
                _userAuthenticationService = userAuthenticationService;
            }

            public async Task<APIResponse> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                    return APIResponse.GetFailureMessage(HttpStatusCode.NotFound, null, ResponseMessages.NotFound);

                var verifyOtp = await _userAuthenticationService.VerifyResetPasswordOTPEmail(user, request.OTP);
                if (!verifyOtp.Success)
                    return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, verifyOtp.Message);

                var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                var result = await _userManager.ResetPasswordAsync(user, passwordResetToken, request.Password);
               
                var failedResponse = APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.FailedCreation);
                if (!result.Succeeded)
                {
                    failedResponse.Data = result.Errors.Select(x => x.Code + " : " + x.Description);
                    return failedResponse;
                }
                return APIResponse.GetSuccessMessage(HttpStatusCode.Created, null, ResponseMessages.SuccessfulCreation);
            }
        }
    }


}
