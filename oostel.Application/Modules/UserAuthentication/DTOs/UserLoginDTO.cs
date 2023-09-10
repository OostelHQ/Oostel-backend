using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserAuthentication.DTOs
{
    public class UserLoginDTO
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
