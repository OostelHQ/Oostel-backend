using MapsterMapper;
using Oostel.Application.Modules.UserWallet.Services;
using Oostel.Common.Constants;
using Oostel.Common.Helpers;
using Oostel.Common.Types.RequestFeatures;
using Oostel.Domain.UserWallet;
using Oostel.Domain.UserWallet.Enum;

namespace Oostel.Application.Modules.UserWallet.Features.Queries
{
    public class GetTransactionRequest : IRequest<ResultResponse<PagedList<Transaction>>>
    {
        public string UserId { get; set; }
        public TransactionType TransactionType { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }

        public sealed class GetTransactionRequestCommand : IRequestHandler<GetTransactionRequest, ResultResponse<PagedList<Transaction>>>
        {
            private readonly IUserWalletService _userWalletService;
            private readonly IMapper _mapper;
            public GetTransactionRequestCommand(IUserWalletService userWalletService, IMapper mapper)
            {
                _userWalletService = userWalletService;
                _mapper = mapper;
            }

            public async Task<ResultResponse<PagedList<Transaction>>> Handle(GetTransactionRequest request, CancellationToken cancellationToken)
            {
                var transaction = await _userWalletService.GetTransaction(request.UserId, request.TransactionType, request.PageSize, request.PageNo);

                if (transaction.Data is null) return ResultResponse<PagedList<Transaction>>.Failure(ResponseMessages.NotFound);

                return ResultResponse<PagedList<Transaction>>.Success(transaction.Data);
            }
        }
    }
}
