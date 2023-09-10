using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Oostel.Application.Modules.UserAuthentication.DTOs;
using Oostel.Application.Modules.UserAuthentication.Services;
using Oostel.Common.Constants;
using Oostel.Common.Helpers;
using Oostel.Common.Types;
using Oostel.Domain.UserAuthentication.Entities;
using System.Net;

namespace Oostel.Application.Modules.UserAuthentication.Features.Commands
{
    public class RegisterUserCommand : IRequest<APIResponse>
    {
        public UserRegisterDTO userRegisterDTO { get; set; }
        public sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, APIResponse>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly ILogger<RegisterUserCommandHandler> _logger;
            private readonly IUserAuthenticationService _userAuthenticationService;
            public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager, ILogger<RegisterUserCommandHandler> logger,
                     IUserAuthenticationService userAuthenticationService)
            {
                _userManager = userManager;
                _logger = logger;
                _userAuthenticationService = userAuthenticationService;
            }
            public async Task<APIResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                var failedResponse = APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.FailedCreation);

                var newUser = new ApplicationUser()
                {
                    FirstName = request.userRegisterDTO.FirstName,
                    LastName = request.userRegisterDTO.LastName,
                    UserName = request.userRegisterDTO.EmailAddress,
                    Email = request.userRegisterDTO.EmailAddress,                   
                    RolesCSV = request.userRegisterDTO.RoleType.GetEnumDescription()
                };

                if (await _userManager.Users.AnyAsync(x => x.Email == request.userRegisterDTO.EmailAddress))
                {
                    failedResponse.Data = new { message = ResponseMessages.UserExists, StatusCode = "400" };
                    return failedResponse;
                }

                var result = await _userManager.CreateAsync(newUser, request.userRegisterDTO.Password);

                if (!result.Succeeded)
                {
                    failedResponse.Data = result.Errors.Select(x => x.Code + " : " + x.Description);
                    return failedResponse;
                }

                var addUserRole = await _userManager.AddToRoleAsync(newUser, request.userRegisterDTO.RoleType.GetEnumDescription());
                if (!addUserRole.Succeeded)
                {
                    failedResponse.Data = addUserRole.Errors.Select(x => x.Code + " : " + x.Description);
                    return failedResponse;
                }

                var user = await _userManager.FindByEmailAsync(request.userRegisterDTO.EmailAddress);
                if (user != null)
                {
                    var emailVerificationResult = await _userAuthenticationService.SendVerifyOTPToUserEmail(user, cancellationToken);
                    if(emailVerificationResult)
                        _logger.LogInformation($"Email Verification Send to User : {user.Id} on Email {user.Email} at {DateTime.Now}  ");
                    else
                    {
                        _logger.LogError($"Email Verification failed to Send to User : {user.Id} on Email {user.Email} at {DateTime.Now}  ");
                    }

                }

                    return APIResponse.GetSuccessMessage(HttpStatusCode.Created, null, ResponseMessages.SuccessfulCreation);
            }
        }

    }
}
