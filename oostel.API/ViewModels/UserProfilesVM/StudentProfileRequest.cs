﻿namespace Oostel.API.ViewModels.UserProfilesVM
{
    public class StudentProfileRequest
    {
        public string UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? StateOfOrigin { get; set; }
        public string? Gender { get; set; }
        public string? SchoolLevel { get; set; }
        public string? Religion { get; set; }
        public string? Denomination { get; set; }
        public string? Age { get; set; }
        public string? Hobby { get; set; }
    }
}
