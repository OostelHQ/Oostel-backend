using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Oostel.Infrastructure.FlutterwaveIntegration.Models
{
    public class BankTransaferResponse
    {
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("data")]
        public BankTransferResponseData? FlTransferData { get; set; }
    }

    public class BankTransferResponseData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("account_number")]
        public string? AccountNumber { get; set; }

        [JsonPropertyName("bank_code")]
        public string? BankCode { get; set; }

        [JsonPropertyName("full_name")]
        public string? FullName { get; set; }

        [JsonPropertyName("created_at")]
        public string? CreatedAt { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("debit_currency")]
        public string? DebitCurrency { get; set; }

        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("fee")]
        public double Fee { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("reference")]
        public string? Reference { get; set; }

        [JsonPropertyName("meta")]
        public object? Meta { get; set; }

        [JsonPropertyName("narration")]
        public string? Narration { get; set; }

        [JsonPropertyName("complete_message")]
        public string? CompleteMessage { get; set; }

        [JsonPropertyName("requires_approval")]
        public int RequiresSApproval { get; set; }

        [JsonPropertyName("is_approved")]
        public int? IsApproved { get; set; }

        [JsonPropertyName("bank_name")]
        public string? BankName { get; set; }
    }
}
