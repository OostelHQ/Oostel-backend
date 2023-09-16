using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserProfiles.DTOs
{
    public class GetUserProfileDTO
    {
        public string UserId { get; set; }
        public string? FullName { get; set; }
        public string StateOfOrigin { get; set; }
        public string PictureUrl { get; set; }
        public string Gender { get; set; }
        public string SchoolLevel { get; set; }
        public string Religion { get; set; }
        public int Age { get; set; }
        public string Hobby { get; set; }
    }
}
