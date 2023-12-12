namespace Oostel.API.ViewModels.UserProfilesVM
{
    public class OpenToRoommateRequest
    {
        public string StudentId { get; set; }
        public bool GottenAHostel { get; set; }
        public decimal HostelPrice { get; set; }
        public string HostelAddress { get; set; }
    }
}
