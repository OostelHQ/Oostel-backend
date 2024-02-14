using Oostel.Application.Modules.Notification.Service;
using Oostel.Common.Constants;
using Oostel.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.Notification.Features.Commands
{
    public class DeleteNotificationCommand : IRequest<APIResponse>
    {
        public string Id { get; set; }
        public NotificationType NotificationType { get; set; }

        public sealed class DeleteNotificationCommandHandler : IRequestHandler<DeleteNotificationCommand , APIResponse>
        {
            private readonly INotificationService _notificationService;
            public DeleteNotificationCommandHandler(INotificationService notificationService) 
            {
                _notificationService = notificationService;
            }

            public async Task<APIResponse> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
            {
                var result = await _notificationService.DeleteNotificationAsync(request.Id, request.NotificationType);

                if (result == false)
                {
                    return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, result, ResponseMessages.FailedToDeleteMessage);
                }

                return APIResponse.GetSuccessMessage(HttpStatusCode.OK, result, ResponseMessages.DeleteMessage);
            }
        }
    }
}
