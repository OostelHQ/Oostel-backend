using Oostel.Domain.UserRoleProfiles.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Domain.UserRolesProfiles.Entities
{
    public class LandlordAgent
    {
        public string AgentId { get; set; }
        public Agent Agent { get; set; }
        public string LandlordId { get; set; }
        public Landlord Landlord { get; set; }
    }
}
