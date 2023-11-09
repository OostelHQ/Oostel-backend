using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Oostel.Application.Modules.UserWallet.DTOs;
using Oostel.Application.Modules.UserWallet.Services;
using Oostel.Common.Enums;
using Oostel.Common.Types;
using Oostel.Domain.UserAuthentication.Entities;
using Oostel.Domain.UserWallet.Enum;
using Oostel.Domain.UserWallet;
using Oostel.Infrastructure.FlutterwaveIntegration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oostel.Common.Helpers;
using Oostel.Common.Constants;

namespace Oostel.Application.Modules.UserWallet.Features.Commands
{
    public class VerifyTransactionPaymentCommand: IRequest<APIResponse>
    {
        public string TransactionReferenceNumber { get; set; }
        public string TransactionId { get; set; }
        public string Status { get; set; }
        public sealed class VerifyTransactionPaymentCommandHandler : IRequestHandler<VerifyTransactionPaymentCommand, APIResponse>
        {
            private readonly IUserWalletService _userWalletService;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IMapper _mapper;
            public VerifyTransactionPaymentCommandHandler(IUserWalletService userWalletService, IMapper mapper, UserManager<ApplicationUser> userManager)
            {
                _userWalletService = userWalletService;
                _mapper = mapper;
                _userManager = userManager;
            }

            public async Task<APIResponse> Handle(VerifyTransactionPaymentCommand request, CancellationToken cancellationToken)
            {
                var mapData = _mapper.Map<VerifyRequestModel>(request);
                var response = new BasePaymentResponse();

                var payInHistory = await _userWalletService.GetPayInHistoryById(request.TransactionId);
                if (payInHistory is null) return null;

                var verifyPayment = await _userWalletService.VerifyTransactionPayment(mapData);

                if (verifyPayment.IsSuccessful == true)
                {

                    // Confirm the Response Status = successful
                    if (verifyPayment.VerificationData.Status == "success" || verifyPayment.VerificationData.Status == "finished")
                    {
                        // Confirm the Response Currency > or = Amount Saved in Currency
                        // Confirm the Response Amount > or = Amount Saved in PayInHistory 
                        if (verifyPayment.VerificationData.Amount >= payInHistory.Amount && verifyPayment.VerificationData.Currency == payInHistory.Currency)
                        {
                            payInHistory.Status = PaymentStatus.Completed.GetEnumDescription();

                            // If Status is Completed 
                            // Create a Transaction Object 
                            await _userWalletService.CreateTransaction(payInHistory.UserId, ResponseMessages.PayInPayment, verifyPayment.VerificationData.Amount ?? 0m, payInHistory.ProviderName, TransactionType.Credit);

                            var mapCompletedPayment = _mapper.Map<PayInHistory>(payInHistory);

                            // Completed : If All goes Well, Update Payment History =   else Failed 
                            await _userWalletService.CreateAndUpdatePayInHistory(mapCompletedPayment);

                            response.VerificationData = verifyPayment.VerificationData;
                            return response;
                        }

                        // On Hold: Successul Status But Amount or Currency not Match
                        payInHistory.Status = PaymentStatus.OnHold.GetEnumDescription();

                        var mapOnHoldPayment = _mapper.Map<PayInHistory>(payInHistory);
                        await _userWalletService.CreateAndUpdatePayInHistory(mapOnHoldPayment);
                        response.VerificationData = verifyPayment.VerificationData;
                        return response;
                    }

                }

                // Failed : Status is not Successful 
                payInHistory.Status = PaymentStatus.Failed.GetEnumDescription();

                var mapFailedPayment = _mapper.Map<PayInHistory>(payInHistory);
                await _userWalletService.CreateAndUpdatePayInHistory(mapFailedPayment);
                response.VerificationData = verifyPayment.VerificationData;
                return response;
            }
        }      
    
    
    }

    }
