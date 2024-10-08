﻿using MapsterMapper;
using Oostel.Application.Modules.Hostel.DTOs;
using Oostel.Application.Modules.Notification.DTOs;
using Oostel.Application.Modules.Notification.Service;
using Oostel.Common.Constants;
using Oostel.Common.Helpers;
using Oostel.Common.Types;
using Oostel.Common.Types.RequestFeatures;
using Oostel.Domain.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Oostel.Application.Modules.Notification.Features.Queries
{
    public class GetNotificationRequest : IRequest<ResultResponse<PagedList<Notifications>>>
    {
        public string UserId { get; set; }
        public NotificationType? NotificationType { get; set; }
        public int NotificationDurationInDays { get; set; }
        public NotificationParam? PaginationParameters { get; set; }

        public class GetNotificationRequestHandler : IRequestHandler<GetNotificationRequest, ResultResponse<PagedList<Notifications>>>
        {
            private readonly INotificationService _notificationService;

            public GetNotificationRequestHandler(INotificationService notificationService) =>              
                _notificationService = notificationService;

            public async Task<ResultResponse<PagedList<Notifications>>> Handle(GetNotificationRequest request, CancellationToken cancellationToken)
            {
                var getNotificationRequestDto = new GetNotificationRequestDTO()
                {
                    UserId = request.UserId,
                    NotificationType = request.NotificationType,
                    notificationDurationInDays = request.NotificationDurationInDays
                };
                var result = await _notificationService.GetNotificationAsync(getNotificationRequestDto, request.PaginationParameters);
                if (result.IsSuccess = false)
                {
                    return ResultResponse<PagedList<Notifications>>.Failure(ResponseMessages.NotFound);
                }
                return ResultResponse<PagedList<Notifications>>.Success(result.Data);
            }
        }
    }
}
