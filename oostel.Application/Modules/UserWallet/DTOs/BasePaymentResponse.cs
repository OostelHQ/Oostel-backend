using Oostel.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserWallet.DTOs
{
    public class BasePaymentResponse : APIResponse
    {
        public string PaymentProvider { get; set; }
        public PaymentGenerationData? PaymentGenerationData { get; set; }
        public VerificationResponse? VerificationData { get; set; }

        public static BasePaymentResponse GetSuccessMessage(string msg, object data = null)
        {
            return new BasePaymentResponse
            {
                IsSuccessful = true,
                Message = msg,
                StatusCode = HttpStatusCode.OK,
                Data = data
            };
        }


        public static BasePaymentResponse GetFailureMessage(string msg, object data = null)
        {
            return new BasePaymentResponse
            {
                IsSuccessful = false,
                Message = msg,
                StatusCode = HttpStatusCode.BadRequest,
            };
        }
    }

    public class PaymentGenerationData
    {
        public string Id { get; set; }
        public string? PaymentLink { get; set; }
        public string? AddressToPayTo { get; set; }
        public string? CurrencyToPay { get; set; }
        public decimal? AmountToPay { get; set; }
        public decimal? AmountConverted { get; set; }
    }

    public class VerificationResponse
    {
        public string? Id { get; set; }
        public string? Status { get; set; }
        public decimal? Amount { get; set; }
        public decimal? ChargedAmount { get; set; }
        public string? Currency { get; set; }

        public string? ReferenceNumber { get; set; }
        public string? ProcessorResponse { get; set; }
    }
}
