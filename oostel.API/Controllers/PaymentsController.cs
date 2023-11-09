using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oostel.API.APIRoutes;
using Oostel.API.ViewModels.WalletVM;
using Oostel.Application.Modules.UserWallet.Features.Commands;
using Oostel.Application.Modules.UserWallet.Features.Queries;
using Oostel.Common.Types;
using Oostel.Infrastructure.Repositories;

namespace Oostel.API.Controllers
{
    [Authorize]
    public class PaymentsController : BaseController
    {
        public readonly IMapper _mapper;
        public readonly IMediator _mediator;
        public PaymentsController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        [Route(PaymentRoute.GetNGNBanks)]
        public async Task<ActionResult<APIResponse>> GetNGNBanks()
        {
            return HandleResult(await _mediator.Send(new GetNGNBanksRequest()));
        }

      
        [HttpGet]
        [Route(PaymentRoute.GeneratePaymentLink)]
        public async Task<ActionResult<APIResponse>> GeneratePaymentLink(PayInRequest payInRequest)
        {
            var payInHistoryRequest = (new PayInCommand
            {
                Amount = payInRequest.Amount,
                Currency = payInRequest.Currency,
                PayInType = payInRequest.PayInType,
                UserId = payInRequest.UserId
            });
            var result = await _mediator.Send(payInHistoryRequest);

            return Ok(result);
        }

        [HttpGet]
        [Route(PaymentRoute.VerifyTransactionPayment)]
        public async Task<ActionResult<APIResponse>> VerifyTransactionPayment(VerifyTransactionPaymentRequest verifyTransaction)
        {
            var mapData = _mapper.Map<VerifyTransactionPaymentCommand>(verifyTransaction);
            var result = await _mediator.Send(mapData);

            return Ok(result);
        }
    }
}
