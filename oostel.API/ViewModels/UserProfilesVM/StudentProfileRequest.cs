namespace Oostel.API.ViewModels.UserProfilesVM
{
    public class StudentProfileRequest
    {
        public string UserId { get; set; }
        public string? StateOfOrigin { get; set; }
        public string? Country { get; set; }
        public string? Gender { get; set; }
        public string? SchoolLevel { get; set; }
        public string? Religion { get; set; }
        public string? Denomination { get; set; }
        public string? Age { get; set; }
        public string? Hobby { get; set; }
        public string? GuardianPhoneNumber { get; set; }
    }
}
