using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Oostel.Application.Modules.UserAuthentication.Services;
using Oostel.Common.Constants;
using Oostel.Common.Helpers;
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
    public class RegisterAgentCommand : IRequest<APIResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string ReferralCode { get; set; }

        public sealed class RegisterAgentCommandHandler : IRequestHandler<RegisterAgentCommand, APIResponse>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly ILogger<RegisterAgentCommandHandler> _logger;
            private readonly IUserAuthenticationService _userAuthenticationService;
            public RegisterAgentCommandHandler(UserManager<ApplicationUser> userManager, ILogger<RegisterAgentCommandHandler> logger,
                     IUserAuthenticationService userAuthenticationService)
            {
                _userManager = userManager;
                _logger = logger;
                _userAuthenticationService = userAuthenticationService;
            }
            public async Task<APIResponse> Handle(RegisterAgentCommand request, CancellationToken cancellationToken)
            {
                var failedResponse = APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.FailedCreation);

                var checkAndValidateTheReferralCode = await _userAuthenticationService.ValidateReferralCode(request.ReferralCode);
                if (checkAndValidateTheReferralCode is null)
                    return APIResponse.GetSuccessMessage(HttpStatusCode.BadRequest, null, ResponseMessages.InValidReferralCode);

                var newUser = new ApplicationUser()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    UserName = request.EmailAddress,
                    Email = request.EmailAddress,
                    RolesCSV = RoleString.Agent
                };

                if (await _userManager.Users.AnyAsync(x => x.Email == request.EmailAddress))
                {
                    failedResponse.Data = new { message = ResponseMessages.UserExists, StatusCode = "400" };
                    return failedResponse;
                }

                var result = await _userManager.CreateAsync(newUser, request.Password);

                if (!result.Succeeded)
                {
                    failedResponse.Data = result.Errors.Select(x => x.Code + " : " + x.Description);
                    return failedResponse;
                }

                var addUserRole = await _userManager.AddToRoleAsync(newUser, RoleString.Agent);
                if (!addUserRole.Succeeded)
                {
                    failedResponse.Data = addUserRole.Errors.Select(x => x.Code + " : " + x.Description);
                    return failedResponse;
                }

                if (request.ReferralCode != null)
                {
                    var referredData = await _userAuthenticationService.CreateReferralAgent(newUser.Id, request.ReferralCode, cancellationToken);

                    if (!referredData)
                    {
                        await _userManager.DeleteAsync(newUser);
                        return failedResponse;
                    }
                }

              /*  var user = await _userManager.FindByEmailAsync(request.EmailAddress);
                if (user != null)
                {
                    var emailVerificationResult = await _userAuthenticationService.SendVerifyOTPToUserEmail(user, cancellationToken);
                    if (emailVerificationResult)
                        _logger.LogInformation($"Email Verification Send to User : {user.Id} on Email {user.Email} at {DateTime.Now}  ");
                    else
                    {
                        _logger.LogError($"Email Verification failed to Send to User : {user.Id} on Email {user.Email} at {DateTime.Now}  ");
                    }

                }*/

                return APIResponse.GetSuccessMessage(HttpStatusCode.Created, null, ResponseMessages.SuccessfulCreation);
            }
        }
    }
}
