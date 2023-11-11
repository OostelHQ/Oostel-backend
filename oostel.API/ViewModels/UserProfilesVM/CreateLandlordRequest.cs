namespace Oostel.API.ViewModels.UserProfilesVM
{
    public class CreateLandlordRequest
    {
        public string UserId { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Street { get; set; }
        public string? Religion { get; set; }
    }
}
