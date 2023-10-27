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
    public class GetPayInHistoryByIdRequest : IRequest<APIResponse>
    {
        public string TransactionId { get; set; }
        public sealed class GetPayInHistoryByIdRequestCommand : IRequestHandler<GetPayInHistoryByIdRequest, APIResponse>
        {
            private readonly IUserWalletService _userWalletService;
            public GetPayInHistoryByIdRequestCommand(IUserWalletService userWalletService)
            {
                _userWalletService = userWalletService;
            }
            public async Task<APIResponse> Handle(GetPayInHistoryByIdRequest request, CancellationToken cancellationToken)
            {
                var payInHistory = await _userWalletService.GetPayInHistoryById(request.TransactionId);
                if(payInHistory is null)
                    return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.NotFound);

                return APIResponse.GetSuccessMessage(HttpStatusCode.OK, data: payInHistory, ResponseMessages.FetchedSuccess);
            }
        }
    }
}
