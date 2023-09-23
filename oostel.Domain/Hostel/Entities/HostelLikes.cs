using Oostel.Common.Types;
using Oostel.Domain.UserAuthentication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Domain.Hostel.Entities
{
    public class HostelLikes : BaseEntity<string>
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string HostelId { get; set; }
        public Hostel Hostel { get; set; }

        public HostelLikes() 
        {
            LastModifiedDate = DateTime.UtcNow;
            CreatedDate = DateTime.UtcNow;
        }

        private HostelLikes(string userId, string hostelId)
        {
            UserId = userId;
            HostelId = hostelId;
            LastModifiedDate = DateTime.UtcNow;
            CreatedDate = DateTime.UtcNow;
            Id = Guid.NewGuid().ToString();
        }

        public static HostelLikes CreateHostelLikesFactory(string userId, string hostelId)
        {
            return new HostelLikes(userId, hostelId);
        }
    }
}
