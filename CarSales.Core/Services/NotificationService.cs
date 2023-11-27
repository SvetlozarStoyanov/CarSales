using AirsoftMatchMaker.Infrastructure.Data.Common.Repository;
using CarSales.Core.Contracts;
using CarSales.Core.Models.Notifications;
using CarSales.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarSales.Core.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IRepository repository;

        public NotificationService(IRepository repository)
        {
            this.repository = repository;
        }


        public async Task<bool> DoesUserHaveUnreadNotificationsAsync(string userId)
        {
            var hasUnreadNotifications = await repository.AllReadOnly<Notification>().AnyAsync(n => n.UserId == userId && !n.IsRead);

            return hasUnreadNotifications;
        }

        public async Task MarkNotificationAsReadAsync(int id)
        {
            var notification = await repository.GetByIdAsync<Notification>(id);
            notification.IsRead = true;
            await repository.SaveChangesAsync();
        }

        public async Task CreateNotificationAsync(string userId, string title, string link)
        {
            var notification = new Notification()
            {
                UserId = userId,
                Title = title,
                Link = link
            };

            await repository.AddAsync<Notification>(notification);

            await repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<NotificationListModel>> GetAllNotificationsAsync(string userId)
        {
            var notifications = await repository.AllReadOnly<Notification>()
                .Where(n => n.UserId == userId)
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
            var notification = await repository.GetByIdAsync<Notification>(id);
            if (!notification.IsRead)
            {
                notification.IsRead = true;
                await repository.SaveChangesAsync();
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
            var notifications = await repository.AllReadOnly<Notification>()
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
