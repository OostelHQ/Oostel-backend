﻿namespace Oostel.API.ViewModels.UserAuthenticationsVM
{
    public class RegisterAgentRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string ReferralCode { get; set; }
    }
}
