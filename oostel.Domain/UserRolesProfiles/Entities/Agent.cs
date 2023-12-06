using Oostel.Common.Types;
using Oostel.Domain.UserAuthentication.Entities;
using Oostel.Domain.UserRoleProfiles.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Domain.UserRolesProfiles.Entities
{
    public class Agent : BaseEntity<string>
    {
        public string State { get; set; }
        public string Religion { get; set; }
        public string? Country { get; set; }
        public string? Street { get; set; }
        public string Gender { get; set; }
        public bool IsVerified { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Denomination { get; set; }
        public ICollection<LandlordAgent> LandlordAgents { get; set; } = new List<LandlordAgent>(); 
        public ApplicationUser User { get; set; }

        public Agent()
        {
            LastModifiedDate = DateTime.UtcNow;
            CreatedDate = DateTime.UtcNow;
        }

        public Agent(string userId) : base(userId)
        {
            LastModifiedDate = DateTime.UtcNow;
            CreatedDate = DateTime.UtcNow;
        }
    }
}
