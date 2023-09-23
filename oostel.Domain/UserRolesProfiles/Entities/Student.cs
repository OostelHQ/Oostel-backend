using Oostel.Common.Types;
using Oostel.Domain.UserAuthentication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Domain.UserRoleProfiles.Entities
{
    public class Student : BaseEntity<string>
    {
        public string StateOfOrigin { get; set; }
        public string Gender { get; set; }
        public string SchoolLevel { get; set; }
        public string? ProfilePhotoURL { get; set; }
        public string Religion { get; set; }
        public bool IsAvailable { get; set; } = false;
        public string Age { get; set; }
        public string Denomination { get; set; }
        public string Hobby { get; set; }
        public virtual ApplicationUser User { get; set; }

        public Student()
        {
            LastModifiedDate = DateTime.UtcNow;
            CreatedDate = DateTime.UtcNow;
        }

        public Student(string userId) : base(userId)
        {
            LastModifiedDate = DateTime.UtcNow;
            CreatedDate = DateTime.UtcNow;
        }
    }
}

