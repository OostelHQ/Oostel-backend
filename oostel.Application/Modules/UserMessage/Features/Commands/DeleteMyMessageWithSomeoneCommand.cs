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
    public class DeleteMyMessageWithSomeoneCommand : IRequest<APIResponse>
    {
        public string Id { get; set; }
        public string SenderId { get; set; }
        public string RecipientId { get; set; }

        public sealed class DeleteMyMessageWithSomeoneCommandHandler : IRequestHandler<DeleteMyMessageWithSomeoneCommand, APIResponse>
        {
            private readonly IMessagingService _messagingService;

            public DeleteMyMessageWithSomeoneCommandHandler(IMessagingService messagingService)
            {
                _messagingService = messagingService;
            }
            public async Task<APIResponse> Handle(DeleteMyMessageWithSomeoneCommand request, CancellationToken cancellationToken)
            {
                var result = await _messagingService.DeleteMyChatWithSomeone(request.Id, request.SenderId, request.RecipientId);
                if (result != true)
                {
                    return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.FailedCreation);
                }
                return APIResponse.GetSuccessMessage(HttpStatusCode.Created, data: result, ResponseMessages.SuccessfulCreation);
            }
        }
    }
}
