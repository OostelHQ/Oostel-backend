using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Oostel.Common.Helpers;
using Oostel.Domain.UserAuthentication.Entities;
using Oostel.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserAuthentication.Services
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        public UserAuthenticationService(IConfiguration configuration, ITokenService tokenService, IEmailSender emailSender, UserManager<ApplicationUser> userManager,
            UnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _emailSender = emailSender;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public async Task<bool> SendVerifyOTPToUserEmail(ApplicationUser user, CancellationToken cancellationToken)
        {
            var userOtpFromDb = await _unitOfWork.UserOTPRepository.Find(x => x.UserId == user.Id);

            var generatedCode = RandomCodeGenerator.GenerateNumbers();
            if (userOtpFromDb == null)
            {
                userOtpFromDb = new UserOTP(user.Id, generatedCode);
                await _unitOfWork.UserOTPRepository.Add(userOtpFromDb); 
            }
            else
            {
                userOtpFromDb.LastModifiedDate = DateTime.UtcNow;
                userOtpFromDb.Otp = generatedCode;
                await _unitOfWork.UserOTPRepository.UpdateAsync(userOtpFromDb); 
            }

            var saveState = await _unitOfWork.SaveAsync(cancellationToken);

            return saveState > 0 ? await SendRegisterVerifyEmail(user.Email, generatedCode, user.LastName) : false;

        }

        private async Task<bool> SendRegisterVerifyEmail(string email, string generatedCode, string lastname)
        {

            var message = $"<p><b>Hello, {lastname}!</b></p>" +
             $"<p>You're welcome on board! Before you take any step further, do well to first verify your email</p><p>" +
             $"<p><b>{generatedCode}</b></p>";

            await _emailSender.SendEmailAsync(email, $"Please Verify Your Email", message);

            return true;
        }
    }
}
