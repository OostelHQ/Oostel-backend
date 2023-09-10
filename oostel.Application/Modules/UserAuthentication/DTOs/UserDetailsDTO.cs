using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserAuthentication.DTOs
{
    public class UserDetailsDTO
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string Role { get; set; }
    }
}
