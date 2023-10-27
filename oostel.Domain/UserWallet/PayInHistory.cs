using Oostel.Common.Enums;
using Oostel.Common.Helpers;
using Oostel.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Domain.UserWallet
{
    public class PayInHistory : BaseEntity<string>
    {
        public string UserId { get; set; }
        public string TransactionId { get; set; }
        public string ReferenceNumber { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string ProviderName { get; set; }
        public string Status { get; set; }

        public PayInHistory() : base(Guid.NewGuid().ToString())
        {
            Status = PaymentStatus.Pending.GetEnumDescription();
        }

        public static PayInHistory CreatePayInHistoryFactory
        (
            string userId,
            string transactionId,
            string referenceNumber,
            decimal amount,
            string currency,
            string providerName
        )
        {
            return new PayInHistory()
            {
                UserId = userId,
                TransactionId = transactionId,
                ReferenceNumber = referenceNumber,
                Amount = amount,
                Currency = currency,
                ProviderName = providerName
            };
        }
    }
}
