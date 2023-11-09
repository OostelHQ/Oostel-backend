using Oostel.Infrastructure.FlutterwaveIntegration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Infrastructure.FlutterwaveIntegration
{
    public interface IFlutterwaveClient
    {
        Task<BankTransferResponseData> ProcessTransfer(BankTransferRequest transferRequest);
        Task<GetBanksResponse> GetBanks();
        Task<VerifyTransactionResponse> VerifyTransactionPayment(VerifyTransactionRequest verifyTransactionRequest);
        Task<GeneratePaymentResponse> GeneratePaymentLink(GeneratePaymentRequest generatePaymentRequest);
    }
}
