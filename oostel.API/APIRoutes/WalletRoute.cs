namespace Oostel.API.APIRoutes
{
    public class WalletRoute
    {
        public const string GetUserTransaction = "wallet/get-user-transaction";
        public const string GetAllPayInHistories = "wallet/get-all-payIn-history";
        public const string GetPayInHistoryId = "wallet/get-payIn-history-by-id";
        public const string UpdateUserWallet = "wallet/update-user-wallet";
        public const string GetSumOfUserAvailableBalance = "wallet/get-sum-of-user-available-balance";
    }
}
