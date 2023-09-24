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
        public string SourceUserId { get; set; }
        public ApplicationUser SourceUser { get; set; }
        public string LikedHostelId { get; set; }
        public Hostel LikedHostel { get; set; }

        public HostelLikes() 
        {
            LastModifiedDate = DateTime.UtcNow;
            CreatedDate = DateTime.UtcNow;
        }

        private HostelLikes(string sourceUserId, string likedHostelId)
        {
            SourceUserId = sourceUserId;
            LikedHostelId = likedHostelId;
            LastModifiedDate = DateTime.UtcNow;
            CreatedDate = DateTime.UtcNow;
            Id = Guid.NewGuid().ToString();
        }

        public static HostelLikes CreateHostelLikesFactory(string sourceUserId, string likedHostelId)
        {
            return new HostelLikes(sourceUserId, likedHostelId);
        }
    }
}
