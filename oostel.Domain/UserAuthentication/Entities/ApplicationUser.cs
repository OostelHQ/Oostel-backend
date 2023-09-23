using Microsoft.AspNetCore.Identity;
using Oostel.Domain.UserRoleProfiles.Entities;

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
        public int ProfileViewCount { get; set; }

        public ICollection<UserOTP> UserOTPs { get; set; }
        public virtual Landlord Landlord { get; set; }
        public virtual Student Student { get; set; }

        public ApplicationUser()
        {
            CreatedDate = DateTime.UtcNow;
            LastSeenDate = DateTime.UtcNow;
        }
    }
}
