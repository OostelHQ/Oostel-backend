using Oostel.Common.Types;
using Oostel.Domain.UserAuthentication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Domain.Hostel.Entities
{
    public class Comment : BaseEntity<string>
    {
        public string UserComment { get; set; }
        public int rating { get; set; }
        public string HostelId { get; set; }
        public string CommenterId { get; set; }
        public string? ParentCommentId { get; set; }
        public List<Comment> Comments { get; set; }

        public Comment():base(Guid.NewGuid().ToString())
        {
            LastModifiedDate = DateTime.UtcNow;
            CreatedDate = DateTime.UtcNow;
        }
    }
}
