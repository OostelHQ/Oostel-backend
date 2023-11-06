using Oostel.Common.Types;
using Oostel.Domain.UserAuthentication.Entities;
using Oostel.Domain.UserRolesProfiles.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Domain.UserRoleProfiles.Entities
{
    public class Landlord : BaseEntity<string>
    {
        public string StateOfOrigin { get; set; }
        public string Religion { get; set; }
        public string? Country { get; set; }
        public bool IsVerified { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Area { get; set; }
        public string State { get; set; }
        public int Age { get; set; }
        public string? Denomination { get; set; }
        public virtual ApplicationUser User { get; set; }
        public ICollection<Domain.Hostel.Entities.Hostel> Hostels { get; set; }
        public ICollection<LandlordAgent> LandlordAgents { get; set; } = new List<LandlordAgent>();

        public Landlord()
        {
            LastModifiedDate = DateTime.UtcNow;
            CreatedDate = DateTime.UtcNow;
        }

        public Landlord(string userId) : base(userId)
        {
            LastModifiedDate = DateTime.UtcNow;
            CreatedDate = DateTime.UtcNow;
        }
    }
}
