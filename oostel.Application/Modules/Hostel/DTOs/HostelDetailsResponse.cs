namespace Oostel.Application.Modules.Hostel.DTOs
{
    public record HostelDetailsResponse
    {
        public HostelDetails HostelDetails { get; set; }
        public List<RoomToReturn>? Rooms { get; set; }
        public List<CommentDTO> CommentDTO { get; set; }
        public LandlordProfileToDisplay LandlordProfile { get; set; }
        public AgentProfileToDisplay AgentProfileToDisplay { get; set; }
    }

    public class HostelDetails
    {
        public string HostelName { get; set; }
        public string HostelDescription { get; set; }
        public int TotalRoom { get; set; }
        public decimal HomeSize { get; set; }
        public string PriceBudgetRange { get; set; }
        public string Street { get; set; }
        public string Junction { get; set; }
        public int HostelLikesCount { get; set; }
        public int NumberOfRoomsLeft { get; set; }
        public string HostelCategory { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public List<string> RulesAndRegulation { get; set; }
        public List<string> HostelFacilities { get; set; }
    }

    public class LandlordProfileToDisplay
    {
        public string LandlordId { get; set; }
        public string FullName { get; set; }
        public string RoleCVS { get; set; }
        public string ProfilePicture { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
        public DateTime RegisterdOn { get; set; }
        public bool IsVerified { get; set; }
    }

    public class AgentProfileToDisplay
    {
        public string AgentId { get; set; }
        public string FullName { get; set; }
        public string RoleCVS { get; set; }
        public string ProfilePicture { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
        public DateTime? RegisterdOn { get; set; }
    }
}
