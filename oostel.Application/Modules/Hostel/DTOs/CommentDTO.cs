using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.Hostel.DTOs
{
    public class CommentDTO
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserComment { get; set; }
        public string DisplayName { get; set; }
        public string ProfilePicture { get; set; }
    }
}
