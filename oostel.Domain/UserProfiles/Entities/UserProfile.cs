using Oostel.Common.Types;
using Oostel.Domain.UserAuthentication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Domain.UserProfiles.Entities
{
    public class UserProfile : BaseEntity<string>
    {
        public string StateOfOrigin { get; set; }
        public string Gender { get; set; }
        public string SchoolLevel { get; set; }
        public string? ProfilePhotoURL { get; set; }
        public string Religion { get; set; }
        public int Age { get; set; }
        public string Hobby { get; set; }
        public virtual ApplicationUser User { get; set; }
        public ICollection<Domain.Hostel.Entities.Hostel> Hostels { get; set; }

        public UserProfile()
        {
            LastModifiedDate = DateTime.UtcNow;
            CreatedDate = DateTime.UtcNow;
        }

        public UserProfile(string userId) : base(userId)
        {
            LastModifiedDate = DateTime.UtcNow;
            CreatedDate = DateTime.UtcNow;
        }
    }
}
