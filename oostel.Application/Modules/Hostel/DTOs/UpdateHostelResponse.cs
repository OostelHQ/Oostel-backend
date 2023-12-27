using Microsoft.AspNetCore.Http;
using Oostel.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.Hostel.DTOs
{
    public class UpdateHostelResponse
    {
        public string LandlordId { get; set; }
        public string HostelId { get; set; }
        public string HostelName { get; set; }
        public string HostelDescription { get; set; }
        public int TotalRoom { get; set; }
        public decimal HomeSize { get; set; }
        public string PriceBudgetRange { get; set; }
        public string Street { get; set; }
        public string Junction { get; set; }
        public string HostelCategory { get; set; }
        public List<string> HostelFrontViewPicture { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public IEnumerable<RoomDTO>? Rooms { get; set; }
        public List<string> RulesAndRegulation { get; set; }
        public List<string> HostelFacilities { get; set; }
        public bool IsAnyRoomVacant { get; set; }
        public string? VideoUrl { get; set; }
    }
}
