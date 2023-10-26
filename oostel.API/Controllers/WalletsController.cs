using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oostel.API.APIRoutes;
using Oostel.Application.Modules.Hostel.Features.Queries;
using Oostel.Common.Types.RequestFeatures;
using Oostel.Common.Types;
using Oostel.Infrastructure.Repositories;
using Oostel.API.ViewModels.WalletVM;
using Oostel.Application.Modules.UserWallet.Features.Queries;

namespace Oostel.API.Controllers
{
    public class WalletsController : BaseController
    {
        private readonly IMapper _mapper;
        public WalletsController(IMapper mapper) => _mapper = mapper;


        [HttpGet]
        [Route(WalletRoute.GetUserTransaction)]
        public async Task<ActionResult<APIResponse>> GetUserTransaction([FromQuery] TransactionRequest transactionRequest)
        {
            return HandlePagedResult(await Mediator.Send(new GetTransactionRequest { UserId = transactionRequest.UserId,
            TransactionType = transactionRequest.TransactionType, PageNo = transactionRequest.PageNo, PageSize = transactionRequest.PageSize }));
        }


    }
}
