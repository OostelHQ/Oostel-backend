using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Domain.Hostel.Entities
{
    public class RoomPictures
    {
        public string Id { get; set; }
        public string PictureUrl { get; set; }

        public string RoomId { get; set; }
        public virtual Room Room { get; set; }
    }
}
