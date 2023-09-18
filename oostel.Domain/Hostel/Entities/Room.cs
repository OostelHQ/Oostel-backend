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
        public List<string> RoomPictures { get; set; }
        public bool IsRented { get; set; }
        public List<string>? RoomFacilities { get; set; }
        public string HostelId { get; set; }
        public Hostel Hostel { get; set; }

        public Room() 
        {
            LastModifiedDate = DateTime.UtcNow;
            CreatedDate = DateTime.UtcNow;
        }

        private Room(string roomNumber, decimal price, string duration, List<string> roomFacilities,
            bool isRented, string hostelId) : base(Guid.NewGuid().ToString())
        {
            RoomNumber = roomNumber;
            Price = price;
            Duration = duration;
            RoomFacilities = roomFacilities;
            IsRented = isRented;
            HostelId = hostelId;
            LastModifiedDate = DateTime.UtcNow;
            CreatedDate = DateTime.UtcNow;
        }

        public static Room CreateRoomForHostelFactory(string roomNumber, decimal price, string duration, List<string> roomFacilities,
             bool isRented, string hostelId)
        {
            return new Room(roomNumber, price, duration, roomFacilities,isRented, hostelId);
        }
    }
}
