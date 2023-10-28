using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Oostel.Infrastructure.FlutterwaveIntegration.Models
{
    public class VerifyTransactionResponse
    {
        [JsonPropertyName("status")]
        public string status { get; set; }

        [JsonPropertyName("message")]
        public string message { get; set; }

        [JsonPropertyName("data")]
        public Data data { get; set; }
    }

    public class Data
    {
        [JsonPropertyName("id")]
        public int id { get; set; }

        [JsonPropertyName("tx_ref")]
        public string tx_ref { get; set; }

        [JsonPropertyName("flw_ref")]
        public string flw_ref { get; set; }

        [JsonPropertyName("device_fingerprint")]
        public string device_fingerprint { get; set; }

        [JsonPropertyName("amount")]
        public int amount { get; set; }

        [JsonPropertyName("currency")]
        public string currency { get; set; }

        [JsonPropertyName("charged_amount")]
        public int charged_amount { get; set; }

        [JsonPropertyName("app_fee")]
        public double app_fee { get; set; }

        [JsonPropertyName("merchant_fee")]
        public int merchant_fee { get; set; }

        [JsonPropertyName("processor_response")]
        public string processor_response { get; set; }

        [JsonPropertyName("auth_model")]
        public string auth_model { get; set; }

        [JsonPropertyName("ip")]
        public string ip { get; set; }

        [JsonPropertyName("narration")]
        public string narration { get; set; }

        [JsonPropertyName("status")]
        public string status { get; set; }

        [JsonPropertyName("payment_type")]
        public string payment_type { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime created_at { get; set; }

        [JsonPropertyName("account_id")]
        public int account_id { get; set; }

        [JsonPropertyName("card")]
        public Card card { get; set; }

        [JsonPropertyName("meta")]
        public object meta { get; set; }

        [JsonPropertyName("amount_settled")]
        public double amount_settled { get; set; }

        [JsonPropertyName("customer")]
        public Customer customer { get; set; }
    }

    public class Customer
    {
        [JsonPropertyName("id")]
        public int id { get; set; }

        [JsonPropertyName("name")]
        public string name { get; set; }

        [JsonPropertyName("phone_number")]
        public string phone_number { get; set; }

        [JsonPropertyName("email")]
        public string email { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime created_at { get; set; }
    }

    public class Card
    {
        [JsonPropertyName("first_6digits")]
        public string first_6digits { get; set; }

        [JsonPropertyName("last_4digits")]
        public string last_4digits { get; set; }

        [JsonPropertyName("issuer")]
        public string issuer { get; set; }

        [JsonPropertyName("country")]
        public string country { get; set; }

        [JsonPropertyName("type")]
        public string type { get; set; }

        [JsonPropertyName("token")]
        public string token { get; set; }

        [JsonPropertyName("expiry")]
        public string expiry { get; set; }
    }
}
