namespace Oostel.API.APIRoutes
{
    public class UserAuthenticationRoute
    {
        public const string RegisterUser = "authenticateuser/register-user";
        public const string LoginUser = "authenticateuser/login-user";
        public const string VerifyOTPEmail = "authenticateuser/verify-otp-email";
        public const string SendResetPasswordOTP = "authenticateuser/Send-reset-password-otp";
        public const string ResetPassword = "authenticateuser/reset-password";
        public const string RegisterAgent = "authenticateuser/register-agent";
        public const string GetCurrentUser = "authenticateuser/get-current-user";
        public const string GetAnyUserDetails = "authenticateuser/get-any-user-details";
        public const string DeleteUserAccount = "user-delete/delete-user-account";
    }
}
