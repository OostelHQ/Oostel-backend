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
    public class UpdateNotificationCommand : IRequest<APIResponse>
    {
        public string UserId { get; set; }
        public List<string> NotificationIds { get; set; }
        public string NotificationType { get; set; }

        public class UpdateNotificationCommandHandler : IRequestHandler<UpdateNotificationCommand, APIResponse>
        {
            private readonly INotificationService _notificationService;

            public UpdateNotificationCommandHandler(INotificationService notificationService)
            {
                _notificationService = notificationService;
            }

            public async Task<APIResponse> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
            {
                var result = await _notificationService.MarkNotificationAsReadAsync(request.UserId, request.NotificationIds);

                if (result == false)
                {
                    return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, result, ResponseMessages.FailToUpdateError);
                }

                return APIResponse.GetSuccessMessage(HttpStatusCode.OK, result, ResponseMessages.UpdateMessage);

            }
        }
    }
}
