using Oostel.Common.Constants;
using Oostel.Common.Types;
using Oostel.Infrastructure.SignalR.Services;
using System.Collections;
using System.Net;

namespace Oostel.Application.Modules.UserMessage.Features.Queries
{
    public class GetAllReceiversWithLastChatQuery : IRequest<APIResponse>
    {
        public string UserId { get; set; }

        public sealed class GetAllReceiversWithLastChatQueryRequest : IRequestHandler<GetAllReceiversWithLastChatQuery, APIResponse>
        {
            private readonly IMessagingService _messagingService;
            public GetAllReceiversWithLastChatQueryRequest(IMessagingService messagingService) => 
                _messagingService = messagingService;

            public async Task<APIResponse> Handle(GetAllReceiversWithLastChatQuery request, CancellationToken cancellationToken)
            {
                var result = await _messagingService.GetAllReceiversWithLastChat(request.UserId);
                if (!result.Any())
                {
                    return APIResponse.GetFailureMessage(HttpStatusCode.OK, new ArrayList(), ResponseMessages.NotFound);
                }
                return APIResponse.GetSuccessMessage(HttpStatusCode.OK, data: result, ResponseMessages.FetchedSuccess);
            }
        }
    }
}
