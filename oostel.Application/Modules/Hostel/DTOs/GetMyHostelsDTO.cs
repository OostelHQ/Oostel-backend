using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.Hostel.DTOs
{
    public class GetMyHostelsDTO
    {
        public string HostelId { get; set; }
        public string HostelName { get; set; }
        public string HostelDescription { get; set; }
        public int TotalRoom { get; set; }
        public decimal HomeSize { get; set; }
        public string PriceBudgetRange { get; set; }
        public string Street { get; set; }
        public string Junction { get; set; }
       // public int HostelLikesCount { get; set; }
        public int NumberOfRoomsLeft { get; set; }
        public HostelLikesAndCount HostelLikesAndCount { get; set; }
        public string HostelCategory { get; set; }
        public List<string> HostelFrontViewPicture { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public List<string> RulesAndRegulation { get; set; }
        public List<string> HostelFacilities { get; set; }

        public List<RoomToReturn> Rooms { get; set; }
    }
}
