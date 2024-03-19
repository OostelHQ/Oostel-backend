using Oostel.Common.Types;
using Oostel.Domain.UserAuthentication.Entities;
using Oostel.Domain.UserWallet.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Domain.UserWallet
{
    public class Transaction: BaseEntity<string>
    {
        public string UserId { get; set; }
        public string Title { get; set; }
        public string FromLastname { get; set; }
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public bool Isprocessed { get; set; }
        public virtual ApplicationUser User { get; set; }

        public Transaction():base(Guid.NewGuid().ToString())
        {

        }
    }
}
