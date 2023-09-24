using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Domain.UserWallet.Enum
{
    public enum TransactionType
    {
        [Description("Unknown")]
        Unknown = 0,
        [Description("Debit")]
        Debit = 1,
        [Description("Credit")]
        Credit = 2,
    }
}
