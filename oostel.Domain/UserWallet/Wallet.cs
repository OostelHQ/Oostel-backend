using Oostel.Common.Types;
using Oostel.Domain.UserAuthentication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Domain.UserWallet
{
    public class Wallet : BaseEntity<string>
    {
        public string UserId { get; set; }
        public decimal AvailableBalance { get; set; }
        public DateTime LastTransactionDate { get; set; } = DateTime.Now;

        public ApplicationUser User { get; set; }

        private Wallet(string userId, decimal availableBalance) : base(Guid.NewGuid().ToString())
        {
            UserId = userId;
            AvailableBalance = availableBalance;
            LastTransactionDate = DateTime.UtcNow;
        }

        public Wallet()
        {

        }

        public static Wallet CreateWalletFactory(string UserId)
        {
            var wallet = new Wallet(UserId, 0.0M);
            return wallet;
        }
    }
}
