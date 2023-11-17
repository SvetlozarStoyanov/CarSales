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

        public async Task<List<NotificationListModel>> GetAllNotificationsAsync(string userId)
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
    }
}
