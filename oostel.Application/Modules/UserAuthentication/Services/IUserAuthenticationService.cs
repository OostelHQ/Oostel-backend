using Oostel.Application.Modules.UserAuthentication.DTOs;
using Oostel.Domain.UserAuthentication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserAuthentication.Services
{
    public interface IUserAuthenticationService
    {
        Task<bool> SendVerifyOTPToUserEmail(ApplicationUser user, CancellationToken cancellationToken);
        Task<bool> VerifyUserOTPFromEmail(string codeReceived, string userId);
        Task<OtpVerificationResponse> VerifyResetPasswordOTPEmail(ApplicationUser user, string Otp);
        Task<bool> SendVerifyResetPasswordOTPToUserEmail(ApplicationUser user, CancellationToken cancellationToken);

    }
}
