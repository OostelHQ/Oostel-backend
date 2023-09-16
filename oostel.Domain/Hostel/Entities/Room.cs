using Oostel.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Domain.Hostel.Entities
{
    public class Room : BaseEntity<string>
    {
        public string RoomNumber { get; set; }
        public decimal Price { get; set; }
        public string Duration { get; set; }
        public string RoomPicture { get; set; }
        public string RoomCategory { get; set; }
        public List<string> RoomPictures { get; set; }
        public bool IsRented { get; set; }

        public string HostelId { get; set; }
        public Hostel Hostel { get; set; }

    }
}
