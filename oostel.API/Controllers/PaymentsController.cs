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
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;
        public PaymentsController(IMapper mapper, UnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route(PaymentRoute.GetNGNBanks)]
        public async Task<ActionResult<APIResponse>> GetNGNBanks()
        {
            return HandleResult(await Mediator.Send(new GetNGNBanksRequest()));
        }
    }
}
