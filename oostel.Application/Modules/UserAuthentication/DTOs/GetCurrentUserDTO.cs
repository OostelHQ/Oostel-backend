using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserAuthentication.DTOs
{
    public class GetCurrentUserDTO
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string ProfilePicture { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string RoleCSV { get; set; }
    }
}
