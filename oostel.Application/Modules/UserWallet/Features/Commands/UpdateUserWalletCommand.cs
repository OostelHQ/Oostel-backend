using Oostel.Application.Modules.UserWallet.Services;
using Oostel.Common.Constants;
using Oostel.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserWallet.Features.Commands
{
    public class UpdateUserWalletCommand : IRequest<APIResponse>
    {
        public string UserId { get; set; }

        public sealed class UpdateUserWalletCommandHandler : IRequestHandler<UpdateUserWalletCommand, APIResponse>
        {
            private readonly IUserWalletService _userWalletService;
            public UpdateUserWalletCommandHandler(IUserWalletService userWalletService)
            {
                _userWalletService = userWalletService;
            }

            public async Task<APIResponse> Handle(UpdateUserWalletCommand request, CancellationToken cancellationToken)
            {
                var updateWallet = await _userWalletService.UpdateUserWalletBalanceWithTransactionHistory(request.UserId);

                if (!updateWallet)
                {
                    return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, updateWallet, ResponseMessages.FailToUpdateError);
                }

                return APIResponse.GetSuccessMessage(HttpStatusCode.OK, data: updateWallet, ResponseMessages.UpdateMessage);
            }
        }
    }
}
