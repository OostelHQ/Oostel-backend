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
    public class SendResetPasswordEmailCommand : IRequest<APIResponse>
    {
        public string Email { get; set; }
        public sealed class SendResetPasswordMailCommandHandler : IRequestHandler<SendResetPasswordEmailCommand, APIResponse>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IUserAuthenticationService _userAuthenticationService;
            public SendResetPasswordMailCommandHandler(UserManager<ApplicationUser> userManager, IUserAuthenticationService userAuthenticationService)
            {
                _userManager = userManager;
                _userAuthenticationService = userAuthenticationService;
            }

            public async Task<APIResponse> Handle(SendResetPasswordEmailCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    return APIResponse.GetFailureMessage(HttpStatusCode.NotFound, null, ResponseMessages.NotFound);
                }
                //send the reset OTP to the user
                await _userAuthenticationService.SendVerifyOTPToUserEmail(user, cancellationToken);

                return APIResponse.GetSuccessMessage(HttpStatusCode.Created, null, ResponseMessages.SuccessfulCreation);
            }
        }
    }
}
