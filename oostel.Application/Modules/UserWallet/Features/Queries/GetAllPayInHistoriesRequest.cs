using MapsterMapper;
using Oostel.Application.Modules.UserWallet.Services;
using Oostel.Common.Constants;
using Oostel.Common.Helpers;
using Oostel.Common.Types;
using Oostel.Common.Types.RequestFeatures;
using Oostel.Domain.UserWallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserWallet.Features.Queries
{
    public class GetAllPayInHistoriesRequest : IRequest<ResultResponse<PagedList<PayInHistory>>>
    {
        public int? PageNo { get; set; }
        public int? PageSize { get; set; }

        public sealed class GetAllPayInHistoriesRequestCommand : IRequestHandler<GetAllPayInHistoriesRequest, ResultResponse<PagedList<PayInHistory>>>
        {
            private readonly IUserWalletService _userWalletService;
            private readonly IMapper _mapper;
            public GetAllPayInHistoriesRequestCommand(IUserWalletService userWalletService, IMapper mapper)
            {
                _userWalletService = userWalletService;
                _mapper = mapper;
            }

            public async Task<ResultResponse<PagedList<PayInHistory>>> Handle(GetAllPayInHistoriesRequest request, CancellationToken cancellationToken)
            {
                var transaction = await _userWalletService.GetPayInHistories(request.PageSize, request.PageNo);

                if (transaction.Data is null) return ResultResponse<PagedList<PayInHistory>>.Failure(ResponseMessages.NotFound);

                return ResultResponse<PagedList<PayInHistory>>.Success(transaction.Data);
            }
        }
    }
}
