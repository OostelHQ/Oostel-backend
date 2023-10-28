using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Oostel.Infrastructure.FlutterwaveIntegration.Models
{
    public class GeneratePaymentResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("data")]
        public GeneratePaymentResponseData Data { get; set; }
    }

    public class GeneratePaymentResponseData
    {
        [JsonPropertyName("link")]
        public string? Link { get; set; }
    }
}
