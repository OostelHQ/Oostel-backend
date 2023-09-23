namespace Oostel.API.ViewModels.UserProfilesVM
{
    public class CreateLandlordRequest
    {
        public string UserId { get; set; }
        public string? StateOfOrigin { get; set; }
        public string? Gender { get; set; }
        public string? SchoolLevel { get; set; }
        public string? Religion { get; set; }
        public string? Denomination { get; set; }
        public int Age { get; set; }
        public string? Hobby { get; set; }
    }
}
