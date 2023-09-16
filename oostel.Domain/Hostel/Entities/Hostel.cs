using Oostel.Common.Types;
using Oostel.Domain.UserProfiles.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Domain.Hostel.Entities
{
    public class Hostel: BaseEntity<string>
    {
        public string HostelName { get; set; }
        public string HostelDescription { get; set; }
        public int TotalRoom { get; set; }
        public decimal HomeSize { get; set; }
        public string Street { get; set; }
        public string Junction { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public List<string> RulesAndRegulation { get; set; }
        public List<string> HostelFacilities { get; set; }
        public string HostelFrontViewPicture { get; set; }
        public bool IsAnyRoomVacant { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public UserProfile User { get; set; }
    }
}
