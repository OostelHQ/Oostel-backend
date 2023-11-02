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
    public class ReferralAgentInfo: BaseEntity<string>
    {
        public string UserId { get; set; }
        public string ReferralCode { get; set; }

        public ApplicationUser User { get; set; }

        private ReferralAgentInfo(string userId, string referralCode)
        :base(Guid.NewGuid().ToString())
        {
            UserId = userId;
            ReferralCode = referralCode;
        }

        public ReferralAgentInfo()
        {

        }

        public static ReferralAgentInfo CreateUserReferralInfoFactory(string UserId, string referralCode)
        {
            var referralInfo = new ReferralAgentInfo(UserId, referralCode);
            return referralInfo;
        }
    }
}
