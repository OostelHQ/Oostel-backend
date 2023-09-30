using Oostel.Domain.UserWallet;
using Oostel.Domain.UserWallet.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserWallet.Services
{
    public interface IUserWalletService
    {
        Task UpdateWalletBalance(decimal amount, string userId, TransactionType transactionType);
        Task<Wallet> GetUserWallet(string userId);
    }
}
