using Oostel.Common.Constants;
using Oostel.Common.Types;
using Oostel.Common.Types.RequestFeatures;
using Oostel.Infrastructure.SignalR.Services;
using System.Collections;
using System.Net;

namespace Oostel.Application.Modules.UserMessage.Features.Queries
{
    public class GetMyChatWithSomeoneQuery : IRequest<APIResponse>
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public ChatParam? ChatParam { get; set; }

        public sealed class GetMyChatWithSomeoneQueryRequest : IRequestHandler<GetMyChatWithSomeoneQuery, APIResponse>
        {
            private readonly IMessagingService _messagingService;

            public GetMyChatWithSomeoneQueryRequest(IMessagingService messagingService)
            {
                _messagingService = messagingService;
            }
            public async Task<APIResponse> Handle(GetMyChatWithSomeoneQuery request, CancellationToken cancellationToken)
            {
                var result = await _messagingService.GetMyChatWithSomeone(request.SenderId, request.ReceiverId, request.ChatParam);
                if (!result.Any())
                {
                    return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, new ArrayList(), ResponseMessages.NotFound);
                }
                return APIResponse.GetSuccessMessage(HttpStatusCode.OK, data: result, ResponseMessages.FetchedSuccess);
            }
        }
    }
}
