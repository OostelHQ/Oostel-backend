using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oostel.API.APIRoutes;
using Oostel.API.ViewModels.NotificationVM;
using Oostel.Application.Modules.Notification.DTOs;
using Oostel.Application.Modules.Notification.Features.Commands;
using Oostel.Application.Modules.Notification.Features.Queries;
using Oostel.Common.Constants;
using Oostel.Common.Types;
using Oostel.Common.Types.RequestFeatures;

namespace Oostel.API.Controllers
{
    [AllowAnonymous]
    public class NotificationController : BaseController
    {
        public readonly IMapper _mapper;
        public readonly IMediator _mediator;
        public NotificationController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        [Route(NotificationRoute.GetNotification)]

        public async Task<ActionResult<APIResponse>> GetNotification([FromQuery] PagingParams paginationParameters, [FromQuery] GetNotificationsRequest notificationRequestDTO)
        {
            var query = new GetNotificationRequest
            {
                UserId = notificationRequestDTO.UserId,
                NotificationType = notificationRequestDTO.NotificationType,
                NotificationDurationInDays = notificationRequestDTO.NotificationDurationInDays,
                PaginationParameters = paginationParameters
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route(NotificationRoute.GetNotificationCount)]
        public async Task<ActionResult<APIResponse>> GetNotificationCount([FromQuery] string userId, NotificationType notificationType)
        {
            var query = new GetNotificationCountRequest
            {
                UserId = userId,
                NotificationType = notificationType
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPut]
        [Route(NotificationRoute.MarkNotificationAsRead)]
        public async Task<ActionResult<APIResponse>> MarkNotificationAsRead([FromBody] UpdateNotificationRequest updateNotificationDto)
        {
            var updateNotificationCommand = _mapper.Map<UpdateNotificationCommand>(updateNotificationDto);
            var result = await _mediator.Send(updateNotificationCommand);
            return Ok(result);
        }

        [HttpPut]
        [Route(NotificationRoute.MarkAllNotificationAsRead)]
        public async Task<ActionResult<APIResponse>> MarkAllNotificationAsRead(string userId)
        {
            var updateNotificationCommand = new UpdateAllNotificationForUserCommand();
            updateNotificationCommand.UserId = userId;
            var result = await _mediator.Send(updateNotificationCommand);
            return Ok(result);
        }

        [HttpDelete]
        [Route(NotificationRoute.DeleteNotification)]
        public async Task<ActionResult<APIResponse>> DeleteNotification([FromQuery] string notificationId, NotificationType notificationType)
        {
            var deleteNotificationCommand = new DeleteNotificationCommand();
            deleteNotificationCommand.Id = notificationId;
            deleteNotificationCommand.NotificationType = notificationType;
            var result = await _mediator.Send(deleteNotificationCommand);
            return Ok(result);
        }
    }
}
