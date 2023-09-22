﻿namespace Oostel.API.ViewModels.UserProfilesVM
{
    public class LandlordProfileRequest
    {
        public string UserId { get; set; }
        public string? FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string StateOfOrigin { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Religion { get; set; }
        public string Age { get; set; }
    }
}
