namespace Oostel.API.ViewModels.UserProfilesVM
{
    public class AgentProfileRequest
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string State { get; set; }
        public string? Denomination { get; set; }
        public string? Gender { get; set; }
        public string? Street { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Religion { get; set; }
    }
}
