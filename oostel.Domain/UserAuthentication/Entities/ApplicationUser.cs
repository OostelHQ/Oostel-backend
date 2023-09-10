using Microsoft.AspNetCore.Identity;
using Oostel.Domain.UserProfiles.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Domain.UserAuthentication.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RolesCSV { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastSeenDate { get; set; }
        public bool IsBlocked { get; set; } = false;

        public ICollection<UserOTP> UserOTPs { get; set; }
        public virtual UserProfile UserProfile { get; set; }

        public ApplicationUser()
        {
            CreatedDate = DateTime.UtcNow;
            LastSeenDate = DateTime.UtcNow;
        }
    }
}
