using Microsoft.AspNetCore.Http;
using Oostel.Common.Enums;
using Oostel.Domain.Hostel.Entities;

namespace Oostel.Application.Modules.Hostel.DTOs
{
    public class HostelDTO
    {
        public string LandlordId { get; set; }
        public string HostelName { get; set; }
        public string HostelDescription { get; set; }
        public int TotalRoom { get; set; }
        public decimal HomeSize { get; set; }
        public string PriceBudgetRange { get; set; }
        public string Street { get; set; }
        public string Junction { get; set; }
        public HostelCategory HostelCategory { get; set; }
        public IFormFile HostelFrontViewPicture { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public IEnumerable<RoomDTO>? Rooms { get; set; }
        public List<string> RulesAndRegulation { get; set; }
        public List<string> HostelFacilities { get; set; }
        public bool IsAnyRoomVacant { get; set; }
    }
}
