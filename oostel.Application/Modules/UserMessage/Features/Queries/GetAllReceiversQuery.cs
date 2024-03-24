using Oostel.Common.Constants;
using Oostel.Common.Types;
using Oostel.Common.Types.RequestFeatures;
using Oostel.Infrastructure.SignalR.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserMessage.Features.Queries
{
    public class GetAllReceiversQuery : IRequest<APIResponse>
    {
        public string SenderId { get; set; }
        public ChatParam? ChatParam { get; set; }

        public sealed class GetAllReceiversQueryRequest : IRequestHandler<GetAllReceiversQuery, APIResponse>
        {
            private readonly IMessagingService _messagingService;

            public GetAllReceiversQueryRequest(IMessagingService messagingService)
            {
                _messagingService = messagingService;
            }
            public async Task<APIResponse> Handle(GetAllReceiversQuery request, CancellationToken cancellationToken)
            {
                var result = await _messagingService.GetAllReceivers(request.SenderId, request.ChatParam);
                if (!result.Any())
                {
                    return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, new ArrayList(), ResponseMessages.NotFound);
                }
                return APIResponse.GetSuccessMessage(HttpStatusCode.OK, data: result, ResponseMessages.FetchedSuccess);
            }
        }
    }
}
