﻿namespace Oostel.API.ViewModels.UserAuthenticationsVM
{
    public class ResetPasswordRequest
    {
        public string Email { get; set; }
        public string OTP { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
