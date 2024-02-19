using Oostel.Application.Modules.Hostel.DTOs;
using Oostel.Application.Modules.Hostel.Services;
using Oostel.Common.Constants;
using Oostel.Common.Helpers;
using Oostel.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.Hostel.Features.Queries
{
    public class GetCommentsRequest : IRequest<APIResponse>
    {
        public string HostelId { get; set; }

        public sealed class GetCommentsRequestCommand : IRequestHandler<GetCommentsRequest, APIResponse>
        {
            private readonly IHostelService _hostelService;
            public GetCommentsRequestCommand(IHostelService hostelService) => _hostelService = hostelService;

            public async Task<APIResponse> Handle(GetCommentsRequest request, CancellationToken cancellationToken)
            {
                var dataToReturn = await _hostelService.GetAllHostelCommentsAsync(request.HostelId);
                if (dataToReturn is null)
                {
                    return APIResponse.GetSuccessMessage(HttpStatusCode.OK, null, ResponseMessages.NotFound);
                }
                return APIResponse.GetSuccessMessage(HttpStatusCode.OK, dataToReturn, ResponseMessages.FetchedSuccess);
            }

        }
    }
}
