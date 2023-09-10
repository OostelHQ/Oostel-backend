using Oostel.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Domain.UserAuthentication.Entities
{
    public class UserOTP : BaseEntity<string>
    {
        public string UserId { get; set; }
        public string Otp { get; set; }
        public ApplicationUser User { get; set; }

        public UserOTP()
        {

        }

        public UserOTP(string userId, string otp)
        {
            UserId = userId;
            Otp = otp;
            LastModifiedDate = DateTime.UtcNow;
            CreatedDate = DateTime.UtcNow;
            Id = Guid.NewGuid().ToString();
        }
    }
}
