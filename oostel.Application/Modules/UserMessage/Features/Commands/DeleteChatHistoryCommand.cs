using MapsterMapper;
using Oostel.Common.Constants;
using Oostel.Common.Types;
using Oostel.Infrastructure.SignalR.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserMessage.Features.Commands
{
    public class DeleteChatHistoryCommand : IRequest<APIResponse>
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }

        public sealed class DeleteChatHistoryCommandHandler : IRequestHandler<DeleteChatHistoryCommand, APIResponse>
        {
            private readonly IMessagingService _messagingService;
            public DeleteChatHistoryCommandHandler(IMessagingService messagingService)
            {
                _messagingService = messagingService;
            }
            public async Task<APIResponse> Handle(DeleteChatHistoryCommand request, CancellationToken cancellationToken)
            {

                var result = await _messagingService.DeleteChatHistory(request.SenderId, request.ReceiverId);
                if (result == false)
                {
                    return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.FailedToDeleteMessage);
                }
                return APIResponse.GetSuccessMessage(HttpStatusCode.Created, data: result, ResponseMessages.DeleteMessage);
            }
        }
    }
}
