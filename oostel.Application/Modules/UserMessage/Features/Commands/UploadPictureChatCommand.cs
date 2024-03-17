using Microsoft.AspNetCore.Http;
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
    public class UploadPictureChatCommand : IRequest<APIResponse>
    {
        public string Message { get; set; }
        public string ReceiverId { get; set; }
        public string SenderId { get; set; }
        public IFormFile MediaFile { get; set; }

        public sealed class UploadPictureChatCommandHandler : IRequestHandler<UploadPictureChatCommand, APIResponse>
        {
            private readonly IMessagingService _messagingService;

            public UploadPictureChatCommandHandler(IMessagingService messagingService)
            {
                _messagingService = messagingService;
            }
            public async Task<APIResponse> Handle(UploadPictureChatCommand request, CancellationToken cancellationToken)
            {
                var result = await _messagingService.SendMediaFile(request.MediaFile, request.SenderId, request.ReceiverId);
                if (result == null)
                {
                    return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.FailedCreation);
                }
                return APIResponse.GetSuccessMessage(HttpStatusCode.Created, data: result, ResponseMessages.SuccessfulCreation);
            }
        }
    }
}

