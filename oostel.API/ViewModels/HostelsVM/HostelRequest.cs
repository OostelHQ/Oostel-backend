using Newtonsoft.Json;
using Oostel.Application.Modules.Hostel.DTOs;
using Oostel.Common.Enums;
using Oostel.Domain.Hostel.Entities;

namespace Oostel.API.ViewModels.HostelsVM
{
    public class HostelRequest
    {
        public string LandlordId { get; set; }
        public string HostelName { get; set; }
        public string HostelDescription { get; set; }
        public int TotalRoom { get; set; }
        public decimal HomeSize { get; set; }
        public string Street { get; set; }
        public string Junction { get; set; }
        public string PriceBudgetRange { get; set; }
        public HostelCategory HostelCategory { get; set; }
        public List<RoomToCreate>? Rooms { get; set; }
        public string State { get; set; }
        public IFormFile HostelFrontViewPicture { get; set; }
        public string Country { get; set; }
        public List<string> RuleAndRegulation { get; set; }
        public List<string> FacilityName { get; set; }
        public bool IsAnyRoomVacant { get; set; }
    }
}
