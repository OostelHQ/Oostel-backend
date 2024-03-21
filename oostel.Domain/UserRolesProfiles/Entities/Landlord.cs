using Oostel.Common.Types;
using Oostel.Domain.UserAuthentication.Entities;
using Oostel.Domain.UserRolesProfiles.Entities;
using Oostel.Domain.UserWallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Domain.UserRoleProfiles.Entities
{
    public class Landlord : BaseEntity<string>
    {
        public string State { get; set; }
        public string Religion { get; set; }
        public string? Country { get; set; }
        public bool IsVerified { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Street { get; set; }
        public string? Gender { get; set; }
        public string? Denomination { get; set; }
        public virtual ApplicationUser User { get; set; }
        public ICollection<Domain.Hostel.Entities.Hostel> Hostels { get; set; }
       // public Wallet Wallet { get; set; }
        public ICollection<LandlordAgent> LandlordAgents { get; set; } = new List<LandlordAgent>();

        public Landlord(string userId, string state, string religion, string country, string gender, string denomination)
        {
            Id = userId;
            State = state;
            Religion = religion;
            Country = country;
            Gender = gender;
            Denomination = denomination;
           // SetDefaultWallet();
        }

        public static Landlord CreateLandlordProfileFactory(string userId, string state, string religion, string country, string gender, string denomination)
        {
            return new Landlord(userId, state, religion, country, gender, denomination);
        }

        public Landlord()
        {
            LastModifiedDate = DateTime.UtcNow;
            CreatedDate = DateTime.UtcNow;
        }

    }
}
