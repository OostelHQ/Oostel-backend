using Oostel.Application.Modules.UserWallet.DTOs;
using Oostel.Common.Helpers;
using Oostel.Common.Types.RequestFeatures;
using Oostel.Domain.UserWallet;
using Oostel.Domain.UserWallet.Enum;
using Oostel.Infrastructure.FlutterwaveIntegration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _userWallet = Oostel.Domain.UserWallet.Wallet;

namespace Oostel.Application.Modules.UserWallet.Services
{
    public interface IUserWalletService
    {
        Task UpdateWalletBalance(decimal amount, string userId, TransactionType transactionType);
        Task<Wallet> GetUserWallet(string userId);
        Task<ResultResponse<PagedList<Transaction>>> GetTransaction(string userId, TransactionType transactionType, int pageSize, int pageNo);
        Task<List<FLBankModel>> GetNigeriaBanks();
        Task<PayInAndOutHistory> GetPayInHistoryById(string transactionId);
        Task UpdateTransaction(Transaction transaction);
        Task<ResultResponse<PagedList<PayInAndOutHistory>>> GetPayInHistories(int pageNo, int pageSize);
        Task<decimal> GetSumOfUserAvailableBalance(string userId);
        Task UpdateUserWallet(_userWallet transaction);
        Task<bool> UpdateUserWalletBalanceWithTransactionHistory(string userId);
        Task<decimal> GetTotalUserWalletBalance();
        Task<BasePaymentResponse> GeneratePaymentDetails(CustomerPaymentInfo customerPaymentInfo);
        Task<PayInAndOutHistory> CreateAndUpdatePayInHistory(PayInAndOutHistory payInHistory);
        Task<bool> CreateTransaction(string senderId, string title, decimal amount, string lastname, TransactionType type);
        Task<BasePaymentResponse> VerifyTransactionPayment(VerifyRequestModel verifyRequestModel);
        string GenerateReferenceNumber();
    }
}
