﻿using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Oostel.Application.Modules.Notification.DTOs;
using Oostel.Application.Modules.Notification.Service;
using Oostel.Application.Modules.UserAuthentication.DTOs;
using Oostel.Common.Constants;
using Oostel.Common.Helpers;
using Oostel.Domain.UserAuthentication.Entities;
using Oostel.Domain.UserAuthentication.Events;
using Oostel.Domain.UserRolesProfiles.Entities;
using Oostel.Infrastructure.EmailService;
using Oostel.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserAuthentication.Services
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly INotificationService _notificationService;
        public UserAuthenticationService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ITokenService tokenService, UserManager<ApplicationUser> userManager,
            UnitOfWork unitOfWork, IEmailSender emailSender, INotificationService notificationService)
        {
            _configuration = configuration;
            _emailSender = emailSender;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _tokenService = tokenService;
            _notificationService = notificationService;
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


        public async Task<bool> SendVerifyResetPasswordOTPToUserEmail(ApplicationUser user, CancellationToken cancellationToken)
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

            return saveState > 0 ? await SendResetPasswordVerifyEmail(user.Email, generatedCode, user.LastName) : false;

        }

        public async Task<GetCurrentUserDTO> GetCurrentUser()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId is null)
                return null;

            var user = await _userManager.FindByIdAsync(userId);

            return new GetCurrentUserDTO()
            {
                UserId = user.Id,
                FullName = $"{user.FirstName} {user.LastName}",
                RoleCSV = user.RolesCSV,
                ProfilePicture = user.ProfilePhotoURL,
                CreatedAt = user.CreatedDate
            };
        }

        public async Task<bool> VerifyUserOTPFromEmail(string codeReceived, string userId)
        {
            var OtpFromDB = await _unitOfWork.UserOTPRepository.Find(x => x.UserId == userId);

            if (OtpFromDB != null)
            {
                if (OtpFromDB.Otp == codeReceived)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> CreateReferralAgent(string userId, string referredCode, CancellationToken cancellationToken)
        {
            var result = false;
            var referralFromDb = await _unitOfWork.ReferralAgentInfoRepository.Find(x => x.ReferralCode == referredCode);

            if (referralFromDb != null)
            {
                var userReferral = new AgentReferred(userId, referralFromDb.Id.ToString(), referredCode);
                await _unitOfWork.AgentReferredRepository.Add(userReferral);

                var commitState = await _unitOfWork.SaveAsync(cancellationToken);
                if (commitState > 0)
                {
                    var username = _userManager.FindByIdAsync(referralFromDb.UserId).Result.UserName.ToString();
                    var userProfilePicUrl = _userManager.FindByIdAsync(userId).Result.ProfilePhotoURL;

                    await _notificationService.CreateNotificationAsync(new NotificationDTO
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = referralFromDb.UserId,
                        NotificationType = NotificationType.ReferralRegistration,
                        Content = String.Format(NotificationMessageConstants.ReferralRegistration, username),
                        UserProfilePicUrl = userProfilePicUrl,
                        IsRead = false,
                        NotificationTypeValueId = userReferral.Id
                    });
                    result = true;
                    
                   // ReferreeAddedEventPublisher.OnReferreeAdded(new ReferreeAddedEventArgs(referralFromDb.UserId.ToString()));
                };
            }

            return true;
        }

        public async Task<string> ValidateReferralCode(string referralCode)
        {
            var checkCodeExist = await _unitOfWork.ReferralAgentInfoRepository.Find(x => x.ReferralCode == referralCode);
            if (checkCodeExist is null) return null;

            return checkCodeExist.ReferralCode;
        }

        public async Task<OtpVerificationResponse> VerifyResetPasswordOTPEmail(ApplicationUser user, string Otp)
        {
            //get the otp
            var userOtpFromDB = (await _unitOfWork.UserOTPRepository.Find(x => x.UserId == user.Id));
            if (userOtpFromDB == null)
                return new OtpVerificationResponse { Message = "Invalid OTP", Success = false };

            //if otp is expired return false
            var machineDate = DateTime.UtcNow;
            if (machineDate > userOtpFromDB.LastModifiedDate.Value.AddHours(1))
                return new OtpVerificationResponse { Message = "OTP Expired", Success = false }; ;

            if (userOtpFromDB.Otp == Otp)
                return new OtpVerificationResponse { Message = "OTP Validated", Success = true }; ;
            return new OtpVerificationResponse { Message = "Invalid OTP", Success = false }; ;
        }

        private async Task<bool> SendRegisterVerifyEmail(string email, string generatedCode, string lastname)
        {

            var message = $"<p><b>Hello, {lastname}!</b></p>" +
             $"<p>You're welcome on board! Before you take any step further, do well to first verify your email</p><p>" +
             $"<p><b>{generatedCode}</b></p>";

            //var emailParams = new EmailParameter(email, $"Please Verify Your Email", message);

            await _emailSender.SendEmailAsync(email, $"Please Verify Your Email", message);

            return true;
        }

        private async Task<bool> SendResetPasswordVerifyEmail(string email, string generatedCode, string lastname)
        {

            var message = $"<p><b>Hello, {lastname}!</b></p>" +
             $"<p>Kindly reset your password with verified OTP below</p><p>" +
             $"<p><b>{generatedCode}</b></p>";

           // var emailParams = new EmailParameter(email, $"Reset Password", message);

            await _emailSender.SendEmailAsync(email, $"Please Verify Your Email", message);

            return true;
        }


    }
}
