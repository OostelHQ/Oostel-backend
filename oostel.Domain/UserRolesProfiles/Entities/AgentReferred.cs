using Oostel.Common.Types;
using Oostel.Domain.UserAuthentication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Domain.UserRolesProfiles.Entities
{
    public class AgentReferred : BaseEntity<string>
    {
        public string UserId { get; set; }

        public string ReferrerId { get; set; }
        public string ReferrerCode { get; set; }

        public ApplicationUser User { get; set; }

        public AgentReferred()
        {

        }
        public AgentReferred(string userId, string referrerId, string referrerCode)
            : base(Guid.NewGuid().ToString())
        {
            UserId = userId;
            ReferrerCode = referrerCode;
            ReferrerId = referrerId;
            LastModifiedDate = DateTime.UtcNow;
            CreatedDate = DateTime.UtcNow;
        }
    }
}
