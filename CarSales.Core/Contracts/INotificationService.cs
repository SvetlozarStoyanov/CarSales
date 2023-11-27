using CarSales.Core.Models.Notifications;

namespace CarSales.Core.Contracts
{
    public interface INotificationService
    {
        Task<bool> DoesUserHaveUnreadNotificationsAsync(string userId);


        Task MarkNotificationAsReadAsync(int id);


        Task CreateNotificationAsync(string userId, string title, string link);


        Task<IEnumerable<NotificationListModel>> GetAllNotificationsAsync(string userId);


        Task<IEnumerable<NotificationListModel>> GetLatestNotificationsAsync(string userId);


        Task<NotificationViewModel> GetNotificationByIdAsync(int id, string userId);
    }
}
