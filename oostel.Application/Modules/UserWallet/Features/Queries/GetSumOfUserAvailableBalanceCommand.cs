using Oostel.Application.Modules.UserWallet.Services;
using Oostel.Common.Constants;
using Oostel.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserWallet.Features.Queries
{
    public class GetSumOfUserAvailableBalanceCommand : IRequest<APIResponse>
    {
        public string UserId { get; set; }

        public sealed class GetSumOfUserAvailableBalanceCommandHandler : IRequestHandler<GetSumOfUserAvailableBalanceCommand, APIResponse>
        {
            private readonly IUserWalletService _userWalletService;
            public GetSumOfUserAvailableBalanceCommandHandler(IUserWalletService userWalletService)
            {
                _userWalletService = userWalletService;
            }

            public async Task<APIResponse> Handle(GetSumOfUserAvailableBalanceCommand request, CancellationToken cancellationToken)
            {
                var availableWallet = await _userWalletService.GetSumOfUserAvailableBalance(request.UserId);
                return APIResponse.GetSuccessMessage(HttpStatusCode.OK, availableWallet, ResponseMessages.FetchedSuccess);
            }
        }
    }
}
