using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Infrastructure.FlutterwaveIntegration
{
    public class AppSettings
    {
        public string BaseUrl { get; set; }
        public string SecretKey { get; set; }
        public string PubKey { get; set; }
        public string EncKey { get; set; }
        public string WebhookUrl { get; set; }
        public string SecretValue { get; set; }
        public string SecretValueHash { get; set; }
    }
}
