using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Oostel.API.APIRoutes;
using Oostel.Application.Modules.UserWallet.Features.Queries;
using Oostel.Common.Types;
using Oostel.Infrastructure.Repositories;

namespace Oostel.API.Controllers
{
    public class PaymentsController : BaseController
    {

        [HttpGet]
        [Route(PaymentRoute.GetNGNBanks)]
        public async Task<ActionResult<APIResponse>> GetNGNBanks()
        {
            return HandleResult(await Mediator.Send(new GetNGNBanksRequest()));
        }
    }
}
