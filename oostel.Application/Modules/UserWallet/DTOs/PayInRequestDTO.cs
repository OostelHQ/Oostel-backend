using Oostel.Infrastructure.FlutterwaveIntegration.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserWallet.DTOs
{
    public class PayInRequestDTO
    {
        public string UserId { get; set; }
        public PayInType PayInType { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
