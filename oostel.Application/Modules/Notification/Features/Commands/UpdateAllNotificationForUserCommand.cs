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
    public class UpdateAllNotificationForUserCommand : IRequest<APIResponse>
    {
        public string UserId { get; set; }

        public class UpdateAllNotificationCommandHandler : IRequestHandler<UpdateAllNotificationForUserCommand, APIResponse>
        {
            private readonly INotificationService _notificationService;

            public UpdateAllNotificationCommandHandler(INotificationService notificationService)
            {
                _notificationService = notificationService;
            }

            public async Task<APIResponse> Handle(UpdateAllNotificationForUserCommand request, CancellationToken cancellationToken)
            {
                var result = await _notificationService.MarkAllNotificationAsReadAsync(request.UserId);

                if (result == false)
                {
                    return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, result, ResponseMessages.FailToUpdateError);
                }

                return APIResponse.GetSuccessMessage(HttpStatusCode.OK, result, ResponseMessages.UpdateMessage);

            }
        }
    }
}
