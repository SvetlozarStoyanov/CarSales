using CarSales.Infrastructure.Data.DataAccess.Repository;
using CarSales.Core.Contracts;
using CarSales.Core.Models.Notifications;
using CarSales.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using CarSales.Infrastructure.Data.DataAccess.UnitOfWork;

namespace CarSales.Core.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork unitOfWork;

        public NotificationService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> CanUserViewNotificationAsync(int notificationId, string userId)
        {
            var notification = await unitOfWork.NotificationRepository.GetByIdAsync(notificationId);

            if (notification != null && notification.UserId == userId)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DoesUserHaveUnreadNotificationsAsync(string userId)
        {
            var hasUnreadNotifications = await unitOfWork.NotificationRepository.AllReadOnly().AnyAsync(n => n.UserId == userId && !n.IsRead);

            return hasUnreadNotifications;
        }

        public async Task MarkNotificationAsReadAsync(int id)
        {
            var notification = await unitOfWork.NotificationRepository.GetByIdAsync(id);
            if (notification != null && !notification.IsRead)
            {
                notification.IsRead = true;
                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task CreateNotificationAsync(string userId, string title, string link)
        {
            var notification = new Notification()
            {
                UserId = userId,
                Title = title,
                Link = link
            };

            await unitOfWork.NotificationRepository.AddAsync(notification);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<NotificationListModel>> GetAllNotificationsAsync(string userId)
        {
            var notifications = await unitOfWork.NotificationRepository.AllReadOnly()
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.Id)
                .Select(n => new NotificationListModel()
                {
                    Id = n.Id,
                    Title = n.Title,
                    Link = n.Link,
                    IsRead = n.IsRead
                })
                .ToListAsync();

            return notifications;
        }

        public async Task<IEnumerable<NotificationListModel>> GetNotificationsAsync(string userId, int skipped)
        {
            var notifications = await unitOfWork.NotificationRepository.AllReadOnly()
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.Id)
                .Skip(skipped)
                .Take(5)
                .Select(n => new NotificationListModel()
                {
                    Id = n.Id,
                    Title = n.Title,
                    Link = n.Link,
                    IsRead = n.IsRead
                })
                .ToListAsync();

            return notifications;
        }

        public async Task<NotificationViewModel> GetNotificationByIdAsync(int id, string userId)
        {
            var notification = await unitOfWork.NotificationRepository.GetByIdAsync(id);
            if (!notification.IsRead)
            {
                notification.IsRead = true;
                await unitOfWork.SaveChangesAsync();
            }
            if (notification.UserId == userId)
            {
                var notificationViewModel = new NotificationViewModel()
                {
                    Id = notification.Id,
                    Title = notification.Title,
                    Link = notification.Link
                };
                return notificationViewModel;
            }

            return null!;
        }

        public async Task<IEnumerable<NotificationListModel>> GetLatestNotificationsAsync(string userId)
        {
            var notifications = await unitOfWork.NotificationRepository.AllReadOnly()
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.Id)
                .Select(n => new NotificationListModel()
                {
                    Id = n.Id,
                    Title = n.Title,
                    Link = n.Link,
                    IsRead = n.IsRead
                })
                .Take(3)
                .ToListAsync();
            return notifications;
        }


    }
}
