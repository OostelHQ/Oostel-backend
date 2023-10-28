using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Oostel.Infrastructure.FlutterwaveIntegration.Models
{
    public class GeneratePaymentRequest
    {
        // Use User WalletId Here 
        [JsonPropertyName("tx_ref")]
        public string TransactionReference { get; set; }

        [JsonPropertyName("amount")]
        public Decimal Amount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        // Frontend URL to redirect to which will call Verify Endpoint
        [JsonPropertyName("redirect_url")]
        public string RedirectURL { get; set; }

        [JsonPropertyName("customer")]
        public PaymentRequestCustomer Customer { get; set; }

        [JsonPropertyName("payment_options")]
        public string? PaymentOptionsCSV { get; set; }

        // Used for Creating Subscriptions 
        [JsonPropertyName("payment_plan")]
        public string? PaymentPlan { get; set; }
    }

    public class PaymentRequestCustomer
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phonenumber")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
