using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Oostel.Infrastructure.FlutterwaveIntegration.Models
{
    public class BankTransferRequest
    {
        [JsonPropertyName("account_bank")]
        public string? AccountBankCode { get; set; }
        [JsonPropertyName("account_number")]
        public string AccountNumber { get; set; }
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
        [JsonPropertyName("currency")]
        public string Currency { get; set; }
        [JsonPropertyName("narration")]
        public string? Narration { get; set; }
        [JsonPropertyName("reference")]
        public string? Reference { get; set; }
        [JsonPropertyName("callback_url")]
        public string? CallbackUrl { get; set; }
        [JsonPropertyName("debit_currency")]
        public string DebitCurrency { get; set; }

        public BankTransferRequest()
        {
            DebitCurrency = "NGN";
        }
    }
}
