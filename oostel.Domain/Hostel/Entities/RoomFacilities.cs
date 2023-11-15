using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Domain.Hostel.Entities
{
    public class RoomFacilities
    {
        public string Id { get; set; }
        public string FacilityName { get; set; }

        public string RoomId { get; set; }
        public Room Room { get; set; }

    }
}
