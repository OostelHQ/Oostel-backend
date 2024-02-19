using Oostel.Domain.Hostel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.Hostel.DTOs
{
    public class CommentResponse
    {
        public string postId { get; set; }
        public string CommenterId { get; set; }
        public string UserComment { get; set; }
        public string? ParentCommentId { get; set; }
        public List<Comment> Comments { get; set; }
    }

    public class CommentModelResponse
    {
        public string Id { get; set; }
        public string Comment { get; set; }
    }
}
