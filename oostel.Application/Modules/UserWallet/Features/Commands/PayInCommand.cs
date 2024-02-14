using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Oostel.Application.Modules.UserWallet.DTOs;
using Oostel.Application.Modules.UserWallet.Features.Queries;
using Oostel.Application.Modules.UserWallet.Services;
using Oostel.Common.Constants;
using Oostel.Common.Helpers;
using Oostel.Common.Types;
using Oostel.Common.Types.RequestFeatures;
using Oostel.Domain.UserAuthentication.Entities;
using Oostel.Domain.UserWallet;
using Oostel.Infrastructure.FlutterwaveIntegration.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserWallet.Features.Commands
{
    public class PayInCommand : IRequest<APIResponse>
    {
        public string UserId { get; set; }
        public PayInType PayInType { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }

        public sealed class PayInCommandHandler : IRequestHandler<PayInCommand, APIResponse>
        {
            private readonly IUserWalletService _userWalletService;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IMapper _mapper;
            public PayInCommandHandler(IUserWalletService userWalletService, IMapper mapper, UserManager<ApplicationUser> userManager)
            {
                _userWalletService = userWalletService;
                _mapper = mapper;
                _userManager = userManager;
            }

            public async Task<APIResponse> Handle(PayInCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByIdAsync(request.UserId);

                var customerPaymentInfo = new CustomerPaymentInfo()
                {
                    UserId = request.UserId.ToString(),
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PaymentData = new PaymentInfo()
                    {
                        Amount = request.Amount,
                        Currency = request.Currency,
                        ReferenceNumber = _userWalletService.GenerateReferenceNumber(),
                    }
                };

                var result = await _userWalletService.GeneratePaymentDetails(customerPaymentInfo);
                if (result.IsSuccessful)
                {
                    // Create Payment History 
                    var paymentHistory = PayInAndOutHistory.CreatePayInHistoryFactory(customerPaymentInfo.UserId, result.PaymentGenerationData.Id,
                        customerPaymentInfo.PaymentData.ReferenceNumber,
                        request.Amount, request.Currency, request.PayInType.GetEnumDescription(), result.PaymentProvider);

                    await _userWalletService.CreateAndUpdatePayInHistory(paymentHistory);
                }

                return APIResponse.GetSuccessMessage(HttpStatusCode.Created, data: result , ResponseMessages.SuccessfulCreation);
            }
        }
    }
}
