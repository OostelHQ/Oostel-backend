using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Oostel.Infrastructure.FlutterwaveIntegration.Models
{
    public class GetBanksResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("data")]
        public List<Bank> Banks { get; set; }

        public class Bank
        {
            [JsonPropertyName("id")]
            public int id { get; set; }

            [JsonPropertyName("code")]
            public string code { get; set; }

            [JsonPropertyName("name")]
            public string name { get; set; }
        }
    }
}
