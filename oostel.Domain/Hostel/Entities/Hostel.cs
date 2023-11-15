using Oostel.Common.Types;
using Oostel.Domain.UserRoleProfiles.Entities;

namespace Oostel.Domain.Hostel.Entities
{
    public class Hostel: BaseEntity<string>
    {
        public string LandlordId { get; set; }
        public string HostelName { get; set; }
        public string HostelDescription { get; set; }
        public int TotalRoom { get; set; }
        public decimal HomeSize { get; set; }
        public string Street { get; set; }
        public string Junction { get; set; }
        public string HostelCategory { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PriceBudgetRange { get; set; }
        public List<HostelRulesAndRegulations> RulesAndRegulation { get; set; } = new List<HostelRulesAndRegulations>();
        public List<HostelFacilities> HostelFacilities { get; set; } = new List<HostelFacilities>();
        public string HostelFrontViewPicture { get; set; }
        public bool IsAnyRoomVacant { get; set; }
        public ICollection<Room>? Rooms { get; set; }
        public Landlord Landlord { get; set; }
        public ICollection<HostelLikes> HostelLikes { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public Hostel()
        {
            LastModifiedDate = DateTime.UtcNow;
            CreatedDate = DateTime.UtcNow;
        }

        private Hostel(string userId, string hostelName, string hostelDescription, int totalRoom, decimal homeSize,
            string street, string junction, string hostelCategory, string state, string priceBudgetRange, string country, List<HostelRulesAndRegulations> rulesAndRegulation, List<HostelFacilities> hostelFacilities, string hostelFrontViewPicture,
            bool isAnyRoomVacant, ICollection<Room> rooms) : base(Guid.NewGuid().ToString())
        {
            LandlordId = userId;
            HostelName = hostelName;    
            HostelDescription = hostelDescription;
            TotalRoom = totalRoom;
            HomeSize= homeSize;
            Street = street;
            Junction = junction;
            HostelCategory = hostelCategory;
            State = state;
            PriceBudgetRange = priceBudgetRange;
            Country = country;
            RulesAndRegulation = rulesAndRegulation;
            HostelFacilities = hostelFacilities;
            HostelFrontViewPicture = hostelFrontViewPicture;
            IsAnyRoomVacant= isAnyRoomVacant;
            Rooms = rooms;
            LastModifiedDate = DateTime.UtcNow;
            CreatedDate = DateTime.UtcNow;
        }

        public static Hostel CreateHostelFactory(string userId, string hostelName, string hostelDescription, int totalRoom, decimal homeSize,
            string street, string junction, string hostelCategory, string state, string priceBudgetRange, string country, List<HostelRulesAndRegulations> rulesAndRegulation, List<HostelFacilities> hostelFacilities,
            string hostelFrontViewPicture, bool isAnyRoomVacant, List<Room> rooms)
        {
            return new Hostel(userId, hostelName, hostelDescription, totalRoom, homeSize, street, junction, hostelCategory, state, priceBudgetRange, country,
                rulesAndRegulation, hostelFacilities, hostelFrontViewPicture, isAnyRoomVacant, rooms);
        }
    }
}
