using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    public class LoginUserCommand : IRequest<APIResponse>
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }

        public sealed class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, APIResponse>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly ITokenService _tokenService;
            public LoginUserCommandHandler(UserManager<ApplicationUser> userManager, ITokenService tokenService)
            {
                _userManager = userManager;
                _tokenService = tokenService;   
            }

            public async Task<APIResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == request.EmailAddress);

                if (user is null)
                {
                    return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.NotFound);
                }

                if (user.IsBlocked)
                {
                    return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.UserBlockedErrorMessage);
                }


                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.EmailNotConfirmed);
                }

                var checkPassword = await _userManager.CheckPasswordAsync(user, request.Password);

                if (!checkPassword)
                {
                    return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, data: null, ResponseMessages.NotFound);
                }

                user.LastSeenDate = DateTime.UtcNow;
                await _userManager.UpdateAsync(user);

                return APIResponse.GetSuccessMessage(HttpStatusCode.Created, data: await _tokenService.CreateUserObject(user), ResponseMessages.LoginMessage);
            }
        }
    }
}
