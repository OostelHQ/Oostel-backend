using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserAuthentication.DTOs
{
    public class OtpVerificationResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
