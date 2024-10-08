﻿using Newtonsoft.Json;
using Oostel.Application.Modules.Hostel.DTOs;
using Oostel.Common.Enums;
using Oostel.Domain.Hostel.Entities;

namespace Oostel.API.ViewModels.HostelsVM
{
    public record HostelRequest
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
        public string State { get; set; }
        public List<IFormFile>? HostelFrontViewPicture { get; set; }
        public string Country { get; set; }
        public IEnumerable<RoomToCreate>? Rooms { get; set; }
        public List<string> RuleAndRegulation { get; set; }
        public List<string> FacilityName { get; set; }
        public bool IsAnyRoomVacant { get; set; }
        public IFormFile? VideoUrl { get; set; }
    }
}
