using Oostel.Common.Enums;
using Oostel.Common.Helpers;
using Oostel.Common.Types;
using Oostel.Domain.UserAuthentication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Domain.UserWallet
{
    public class PayInAndOutHistory : BaseEntity<string>
    {
        public string UserId { get; set; }
        public string TransactionId { get; set; }
        public string ReferenceNumber { get; set; }
        public string PaymentType { get; set; }
        public decimal AmountPaid { get; set; }
        public string Currency { get; set; }
        public string ProviderName { get; set; }
        public string Status { get; set; }

        public ApplicationUser User { get; set; }

        public PayInAndOutHistory()
        {
            Status = PaymentStatus.Pending.GetEnumDescription();
            CreatedDate = DateTime.UtcNow;
            LastModifiedDate = DateTime.UtcNow;
        }

        public PayInAndOutHistory(Guid? id = null) : base()
        {
            Status = PaymentStatus.Pending.GetEnumDescription();
            Id = id.ToString() ?? Guid.NewGuid().ToString();
            CreatedDate = DateTime.UtcNow;
            LastModifiedDate = DateTime.UtcNow;
        }

        public static PayInAndOutHistory CreatePayInHistoryFactory
        (
            string userId,
            string transactionId,
            string referenceNumber,
            decimal amountPaid,
            string currency,
            string paymentType,
            string providerName
        )
        {
            return new PayInAndOutHistory()
            {
                UserId = userId,
                TransactionId = transactionId,
                ReferenceNumber = referenceNumber,
                AmountPaid = amountPaid,
                Currency = currency,
                PaymentType= paymentType,
                ProviderName = providerName
            };
        }
    }
}
