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
using _userMessage = Oostel.Domain.UserMessage.UserMessage;

namespace Oostel.Application.Modules.UserMessage.Features.Commands
{
    public class SendMessageCommand : IRequest<APIResponse>
    {
        public string Message { get; set; }
        public string RecipientId { get; set; }
        public string SenderId { get; set; }

        public sealed class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, APIResponse>
        {
            private readonly IMessagingService _messagingService;
            private readonly IMapper _mapper;
            public SendMessageCommandHandler(IMessagingService messagingService, IMapper mapper) 
            {
                _messagingService = messagingService;
                _mapper = mapper;
            }

            public async Task<APIResponse> Handle(SendMessageCommand request, CancellationToken cancellationToken)
            {
                var mapData = _mapper.Map<_userMessage>(request);
                var result = await _messagingService.CreateChat(mapData);
                if (result == null)
                {
                    return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.FailedCreation);
                }
                return APIResponse.GetSuccessMessage(HttpStatusCode.Created, data: result, ResponseMessages.SuccessfulCreation);
            }
        }
    }
}
