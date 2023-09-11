using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Common.Constants
{
    public static class ResponseMessages
    {
        public const string ExceptionMessage = "Sorry, something went wrong. Kindly retry";
        public const string UserExists = "Email address is in use";
        public const string UnAuthorizedMessage = "You don't have access to this resource";
        public const string NoUserExists = "Invalid Crendentials. There was a problem with your login";
        public const string EmailConfirmationSuccessful = "Email confirmed - you can now login...";
        public const string LoginMessage = "User Login Successfully!";
        public const string RegistrationMessage = "Registration success. Please verify your email";
        public const string EmailNotConfirmed = "Email not confirmed";
        public const string ErrorWithEmailVerification = "Could not verify email address";
        public const string FailedCreation = "Creation failed";
        public const string SuccessfulCreation = "Created Successfully";
        public const string RegistrationError = "Could not complete your registration. Please retry later";
        public const string NotFound = "Data not found";
        public const string FetchedSuccess = "Fetched data successfully";
        public const string ForgotPasswordResponse = "Check your Email, mail just sent!";
        public const string ResetPasswordResponse = "Your password has been saved!";
        public const string ConfirmPasswordResponse = "Password doesn't match its confirmation";
        public const string ResendEmailVerificationLink = "Email verification resent!";
        public const string DeleteUserAccountErrorMessage = "User deleted permanently!";
        public const string UserBlockedErrorMessage = "User account is blocked!";
        public const string InvalidOTPRequest = "Invalid OTP";

    }
}
