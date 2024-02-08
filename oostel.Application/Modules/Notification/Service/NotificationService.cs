using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Oostel.Application.Modules.Notification.DTOs;
using Oostel.Common.Constants;
using Oostel.Common.Types.RequestFeatures;
using Oostel.Domain.Notification;
using Oostel.Infrastructure.Data;
using Oostel.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.Notification.Service
{
    public class NotificationService: INotificationService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public NotificationService(UnitOfWork unitOfWork, IMapper mapper, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context= context;
        }

        public async Task<bool> CreateNotificationAsync(NotificationDTO notificationDTO)
        {
            var notificationType = notificationDTO.NotificationType == 0
                ? string.Empty : notificationDTO.NotificationType.ToString();

            if (!Enum.IsDefined(typeof(NotificationType), notificationDTO.NotificationType))
            {
                throw new ArgumentNullException(nameof(notificationDTO.NotificationType));
            }

            Notifications notification = new Notifications(Guid.NewGuid().ToString(),
                notificationDTO.UserId, notificationType,
                notificationDTO.Content, notificationDTO.UserProfilePicUrl, DateTime.Now, notificationDTO.IsRead, notificationDTO.NotificationTypeValueId);

            await _unitOfWork.NotificationRepository.Add(notification);
            await _unitOfWork.SaveAsync();
            return true;
        }


        public async Task<IEnumerable<Notifications>> GetNotificationAsync(GetNotificationRequestDTO notificationRequestDTO, PagingParams paginationParameters)
        {
            if (Enum.IsDefined(typeof(NotificationType), notificationRequestDTO.NotificationType) && notificationRequestDTO.NotificationType != 0)
            {
                var notification = await _context.Notifications
                                        .Where(x => x.UserId == notificationRequestDTO.UserId)
                                        .OrderByDescending(d => d.CreatedDate)
                                        .AsNoTracking()
                                        .ToListAsync();
                return null;

            }
            return null;
        }

        public async Task<bool> MarkNotificationAsReadAsync(string userId, List<string> notificationIds)
        {
            var markNotifications = await _unitOfWork.NotificationRepository.Find(x => x.UserId == userId && notificationIds.Contains(g.Id));

            foreach (var notification in markNotifications)
            {
                notification.IsRead = true;
                await _unitOfWork.NotificationRepository.UpdateAsync(notification);
            }
            return true;
        }

        public async Task<bool> MarkAllNotificationAsReadAsync(string userId)
        {
            var markNotifications = await _unitOfWork.NotificationRepository.Find(x => x.UserId == userId);

            foreach (var notification in markNotifications)
            {
                notification.IsRead = true;
                await _unitOfWork.NotificationRepository.UpdateAsync(notification);
            }
            return true;
        }

        public async Task<int> GetNotificationCountAsync(string userId, NotificationType notificationType)
        {
            if (!Enum.IsDefined(typeof(NotificationType), notificationType) && notificationType != 0)
            {
                throw new ArgumentNullException(nameof(_notification.NotificationType));
            }

            var notificationTypeString = notificationType == 0 ? string.Empty : notificationType.ToString();
            return await _notificationOrchestrator.GetNotificationCountAsync(userId, notificationTypeString);
        }

        public async Task<bool> DeleteNotificationAsync(string notificationId, NotificationType notificationType)
        {
            if (!Enum.IsDefined(typeof(NotificationType), notificationType))
            {
                throw new ArgumentNullException(nameof(_notification.NotificationType));
            }

            return await _notificationOrchestrator.DeleteNotificationAsync(notificationId, notificationType.ToString());
        }
    }
}
