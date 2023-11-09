namespace Oostel.API.ViewModels.WalletVM
{
    public class VerifyTransactionPaymentRequest
    {
        public string TransactionReferenceNumber { get; set; }
        public string TransactionId { get; set; }
        public string Status { get; set; }
    }
}
