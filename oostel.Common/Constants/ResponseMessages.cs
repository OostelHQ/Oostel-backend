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
        public const string NoUserExists = "Invalid Crendentials. There was a problem with your login";
        public const string EmailConfirmationSuccessful = "ConfirmationSuccessful.";
        public const string UsernameExist = "Username is already being used";
        public const string RegistrationMessage = "Registration success. Please verify your email";
        public const string EmailNotConfirmed = "Email not confirmed";
        public const string FailedCreation = "Creation failed";
        public const string SuccessfulCreation = "Created Successfully";
        public const string RegistrationError = "Could not complete your registration. Please retry later";
        public const string NotFound = "No wishList exist in the database";
        public const string ForgotPasswordException = "Check your Email, mail just sent!";
        public const string ResetPasswordException = "Your password has been saved!";
        public const string ConfirmPasswordException = "Password doesn't match its confirmation";

    }
}
