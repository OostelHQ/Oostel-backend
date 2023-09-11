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
    public class VerifyUserOTPFromEmailCommand : IRequest<APIResponse>
    {
        public string Email { get; set; }
        public string Otp { get; set; }


        public sealed class VerifyUserOTPFromEmailCommandHandler : IRequestHandler<VerifyUserOTPFromEmailCommand, APIResponse>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IUserAuthenticationService _userAuthenticationService;
            public VerifyUserOTPFromEmailCommandHandler(UserManager<ApplicationUser> userManager, IUserAuthenticationService userAuthenticationService)
            {
                _userManager = userManager;
                _userAuthenticationService = userAuthenticationService;
            }

            public async Task<APIResponse> Handle(VerifyUserOTPFromEmailCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user == null)
                {
                    return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.FailedCreation);
                }

                var verifyStatus = await _userAuthenticationService.VerifyUserOTPFromEmail(request.Otp, user.Id);

                if (verifyStatus)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var result = await _userManager.ConfirmEmailAsync(user, token);

                    if (!result.Succeeded)
                    {
                        return APIResponse.GetFailureMessage(HttpStatusCode.NotFound, null, ResponseMessages.EmailNotConfirmed);
                    }
                    return APIResponse.GetSuccessMessage(HttpStatusCode.Created, data: user.Id, ResponseMessages.SuccessfulCreation);
                }
                return APIResponse.GetFailureMessage(HttpStatusCode.NotFound, null, ResponseMessages.InvalidOTPRequest);
            }
        }
        
    }
}
