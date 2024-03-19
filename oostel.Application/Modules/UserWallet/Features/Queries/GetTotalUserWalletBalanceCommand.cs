using Oostel.Application.Modules.UserWallet.Services;
using Oostel.Common.Constants;
using Oostel.Common.Types;
using System.Net;

namespace Oostel.Application.Modules.UserWallet.Features.Queries
{
    public class GetTotalUserWalletBalanceCommand : IRequest<APIResponse>
    {
        public sealed class GetTotalUserWalletBalanceCommandHandler : IRequestHandler<GetTotalUserWalletBalanceCommand, APIResponse>
        {
            private readonly IUserWalletService _userWalletService;
            public GetTotalUserWalletBalanceCommandHandler(IUserWalletService userWalletService)
            {
                _userWalletService = userWalletService;
            }

            public async Task<APIResponse> Handle(GetTotalUserWalletBalanceCommand request, CancellationToken cancellationToken)
            {
                var totalWallet = await _userWalletService.GetTotalUserWalletBalance();
                return APIResponse.GetSuccessMessage(HttpStatusCode.OK, totalWallet, ResponseMessages.FetchedSuccess);
            }
        }
    }
}
