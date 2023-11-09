using Oostel.Infrastructure.FlutterwaveIntegration.Enums;

namespace Oostel.API.ViewModels.WalletVM
{
    public class PayInRequest
    {
        public string UserId { get; set; }
        public PayInType PayInType { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
