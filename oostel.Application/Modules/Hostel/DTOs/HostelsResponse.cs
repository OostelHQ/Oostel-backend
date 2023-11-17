using Oostel.Domain.Hostel.Entities;

namespace Oostel.Application.Modules.Hostel.DTOs
{
    public record HostelsResponse
    {
        public string UserId { get; set; }
        public string HostelId { get; set; }
        public string HostelName { get; set; }
        public string HostelDescription { get; set; }
        public string PriceBudgetRange { get; set; }
        public int TotalRoom { get; set; }
        public decimal HomeSize { get; set; }
        public string Street { get; set; }
        public string Junction { get; set; }
        public string HostelCategory { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int NumberOfRoomsLeft { get; set; }
        public int HostelLikesCount { get; set; }
        public List<string> RulesAndRegulation { get; set; }
        public List<string> HostelFacilities { get; set; }
    }
}
