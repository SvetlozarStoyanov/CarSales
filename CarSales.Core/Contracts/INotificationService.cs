using CarSales.Core.Models.Notifications;
using CarSales.Infrastructure.Data.Entities;

namespace CarSales.Core.Contracts
{
    public interface INotificationService
    {

        /// <summary>
        /// Returns true if <see cref="User"/> can view <see cref="Notification"/> with <paramref name="notificationId"/>,
        /// false otherwise
        /// </summary>
        /// <param name="notificationId"></param>
        /// <param name="userId"></param>
        /// <returns><see cref="bool"/></returns>
        Task<bool> CanUserViewNotificationAsync(int notificationId, string userId);

        /// <summary>
        /// Returns true if <see cref="User"/> has unread <see cref="Notification"/>s
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><see cref="bool"/></returns>
        Task<bool> DoesUserHaveUnreadNotificationsAsync(string userId);

        /// <summary>
        /// Marks <see cref="Notification"/> with given <paramref name="id"/> as read
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task MarkNotificationAsReadAsync(int id);

        /// <summary>
        /// Creates a new <see cref="Notification"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="title"></param>
        /// <param name="link"></param>
        /// <returns></returns>
        Task CreateNotificationAsync(string userId, string title, string link);

        /// <summary>
        /// Returns all <see cref="Notification"/>s for given <see cref="User"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<NotificationListModel>> GetAllNotificationsAsync(string userId);

        /// <summary>
        /// Returns latest <see cref="Notification"/>s for given <see cref="User"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><see cref="IEnumerable{}" of<see cref="NotificationListModel"/>/></returns>
        Task<IEnumerable<NotificationListModel>> GetLatestNotificationsAsync(string userId);

        /// <summary>
        /// Returns <see cref="Notification"/> with given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns><see cref="NotificationViewModel"/></returns>
        Task<NotificationViewModel> GetNotificationByIdAsync(int id, string userId);
    }
}
