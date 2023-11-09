using Oostel.Infrastructure.FlutterwaveIntegration.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Infrastructure.FlutterwaveIntegration.Models
{
    public class VerifyRequestModel
    {
        public string? TransactionReferenceNumber { get; set; }
        public string? TransactionId { get; set; }
        public string? Status { get; set; }
        public PayInType PayInType { get; set; }
    }
}
