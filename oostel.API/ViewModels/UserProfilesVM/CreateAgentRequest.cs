namespace Oostel.API.ViewModels.UserProfilesVM
{
    public class CreateAgentRequest
    {
        public string UserId { get; set; }
        public string StateOfOrigin { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Religion { get; set; }
        public int Age { get; set; }
    }
}
