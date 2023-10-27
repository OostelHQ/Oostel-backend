using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserWallet.DTOs
{
    public class PayInHistoryDTO
    {
        public string UserId { get; set; }
        public string TransactionId { get; set; }
        public string ReferenceNumber { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string ProviderName { get; set; }
        public string Status { get; set; }
    }
}
