using Oostel.Application.Modules.UserProfiles.Features.Queries;
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
    public class GetNGNBanksRequest : IRequest<APIResponse>
    {

        public sealed class GetNGNBanksRequestCommand : IRequestHandler<GetNGNBanksRequest, APIResponse>
        {
            private readonly IUserWalletService _userWalletService;
            public GetNGNBanksRequestCommand(IUserWalletService userWalletService)
            {
                _userWalletService = userWalletService;
            }
            public async Task<APIResponse> Handle(GetNGNBanksRequest request, CancellationToken cancellationToken)
            {
                var NGNBanks = await _userWalletService.GetNigeriaBanks();

                return APIResponse.GetSuccessMessage(HttpStatusCode.OK, data: NGNBanks, ResponseMessages.FetchedSuccess);
            }
        }
    }
}
