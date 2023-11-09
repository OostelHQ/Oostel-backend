﻿using Oostel.Application.Modules.UserWallet.DTOs;
using Oostel.Common.Helpers;
using Oostel.Common.Types.RequestFeatures;
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
        Task<ResultResponse<PagedList<Transaction>>> GetTransaction(string userId, TransactionType transactionType, int pageSize, int pageNo);
        Task<List<FLBankModel>> GetNigeriaBanks();
        Task<PayInHistory> GetPayInHistoryById(string transactionId);
        Task<ResultResponse<PagedList<PayInHistory>>> GetPayInHistories(int pageNo, int pageSize);
        Task<BasePaymentResponse> GeneratePaymentDetails(CustomerPaymentInfo customerPaymentInfo);
    }
}
