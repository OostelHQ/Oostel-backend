using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserRolesProfiles.DTOs
{
    public class GetUserDetailsWithId
    {
        public string UserId { get; set; }
        public string Fullname { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string Role { get; set; }
    }
}
