using Oostel.Application.Modules.Notification.DTOs;
using Oostel.Common.Constants;
using Oostel.Common.Helpers;
using Oostel.Common.Types.RequestFeatures;
using Oostel.Domain.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.Notification.Service
{
    public interface INotificationService
    {
        Task<bool> CreateNotificationAsync(NotificationDTO notificationDTO);
        Task<ResultResponse<PagedList<Notifications>>> GetNotificationAsync(GetNotificationRequestDTO notificationRequestDTO, PagingParams paginationParameters);
        Task<bool> MarkNotificationAsReadAsync(string userId, List<string> notificationIds);
        Task<bool> MarkAllNotificationAsReadAsync(string userId);
        Task<int> GetNotificationCountAsync(string userId, NotificationType notificationType);
        Task<bool> DeleteNotificationAsync(string notificationId, NotificationType notificationType);
    }
}
