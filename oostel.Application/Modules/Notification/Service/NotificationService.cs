using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Oostel.Application.Modules.Hostel.DTOs;
using Oostel.Application.Modules.Notification.DTOs;
using Oostel.Common.Constants;
using Oostel.Common.Helpers;
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
        private readonly ApplicationDbContext _context;
        public NotificationService(UnitOfWork unitOfWork, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
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


        public async Task<ResultResponse<PagedList<Notifications>>> GetNotificationAsync(GetNotificationRequestDTO notificationRequestDTO, PagingParams paginationParameters)
        {
            if (Enum.IsDefined(typeof(NotificationType), notificationRequestDTO.NotificationType) && notificationRequestDTO.NotificationType != 0)
            {
                var notificationQuery = _context.Notifications
                                        .OrderByDescending(d => d.CreatedDate)
                                        .Where(x => x.UserId == notificationRequestDTO.UserId)
                                        .AsNoTracking()
                                        .AsQueryable();

                if (notificationRequestDTO.NotificationType != null)
                {
                    notificationQuery = notificationQuery.Where(o => o.NotificationType == notificationRequestDTO.NotificationType.GetEnumDescription());
                }
                else if (notificationRequestDTO.notificationDurationInDays > 0)
                {
                    notificationQuery.Where(t => t.CreatedDate >= DateTime.UtcNow.AddDays(-notificationRequestDTO.notificationDurationInDays));
                }

                return ResultResponse<PagedList<Notifications>>.Success(await PagedList<Notifications>.CreateAsync(notificationQuery, paginationParameters.PageNumber, paginationParameters.PageSize));
            }

            return ResultResponse<PagedList<Notifications>>.Failure(ResponseMessages.NotFound); ;
        }

        public async Task<bool> MarkNotificationAsReadAsync(string userId, List<string> notificationIds)

        {
            var markNotifications = _unitOfWork.NotificationRepository.FindByCondition(x => x.UserId == userId && notificationIds.Contains(x.Id), false);

            foreach (var notification in markNotifications)
            {
                notification.IsRead = true;
                await _unitOfWork.NotificationRepository.UpdateAsync(notification);
                await _context.SaveChangesAsync();
            }
            return true;
        }

        public async Task<bool> MarkAllNotificationAsReadAsync(string userId)
        {
            var markNotifications =  _unitOfWork.NotificationRepository.FindByCondition(x => x.UserId == userId, false);

            foreach (var notification in markNotifications)
            {
                notification.IsRead = true;
                await _unitOfWork.NotificationRepository.UpdateAsync(notification);
                await _context.SaveChangesAsync();
            }
            return true;
        }

        public async Task<int> GetNotificationCountAsync(string userId, NotificationType notificationType)
        {
            if (!Enum.IsDefined(typeof(NotificationType), notificationType) && notificationType != 0)
            {
                throw new ArgumentNullException(nameof(Notifications.NotificationType));
            }

            var notificationTypeString = notificationType == 0 ? string.Empty : notificationType.ToString();
            var notificationCountQuery = await _context.Notifications
                   .Where(x => x.UserId == userId && x.IsRead == false)
                   .ToListAsync();

            notificationCountQuery.Where(x => x.NotificationType == notificationTypeString);

            return notificationCountQuery.Count();
        }

        public async Task<bool> DeleteNotificationAsync(string notificationId, NotificationType notificationType)
        {
            if (!Enum.IsDefined(typeof(NotificationType), notificationType))
            {
                throw new ArgumentNullException(nameof(Notifications.NotificationType));
            }

            _unitOfWork.NotificationRepository.FindByCondition(x => x.Id == notificationId && x.NotificationType == notificationType.ToString(), false);

            return true;
        }
    }
}
