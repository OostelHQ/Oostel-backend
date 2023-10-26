using Oostel.Domain.UserWallet.Enum;

namespace Oostel.API.ViewModels.WalletVM
{
    public class TransactionRequest
    {
        public string UserId { get; set; }
        public TransactionType TransactionType { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }
}
