using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserWallet.DTOs
{
    public class UserWalletBalanceDTO
    {
        public decimal AvailableBalance { get; set; }
        public DateTime LastTransactionDate { get; set; }
    }
}
