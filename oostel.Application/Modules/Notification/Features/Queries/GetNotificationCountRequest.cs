using Mailjet.Client.Resources;
using Oostel.Application.Modules.Notification.Service;
using Oostel.Common.Constants;
using Oostel.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Oostel.Application.Modules.Notification.Features.Queries
{
    public class GetNotificationCountRequest : IRequest<APIResponse>
    {
        public string UserId { get; set; }
        public NotificationType NotificationType { get; set; }

        public sealed class GetNotificationCountRequestHandler : IRequestHandler<GetNotificationCountRequest, APIResponse>
        {
            private readonly INotificationService _notificationService;

            public GetNotificationCountRequestHandler(INotificationService notificationService)
            {
                _notificationService = notificationService;
            }

            public async Task<APIResponse> Handle(GetNotificationCountRequest request, CancellationToken cancellationToken)
            {
                var result = await _notificationService.GetNotificationCountAsync(request.UserId, request.NotificationType);
                if (result == 0)
                {
                    return APIResponse.GetFailureMessage(HttpStatusCode.NotFound, null, ResponseMessages.NotFound);
                }
                return APIResponse.GetSuccessMessage(HttpStatusCode.OK, result, ResponseMessages.FetchedSuccess);
            }
        }
    }
}
