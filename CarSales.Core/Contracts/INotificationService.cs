using CarSales.Core.Models.Notifications;

namespace CarSales.Core.Contracts
{
    public interface INotificationService
    {
        Task CreateNotificationAsync(string userId, string title, string link);

        Task<List<NotificationListModel>> GetAllNotificationsAsync(string userId);

        Task<NotificationViewModel> GetNotificationByIdAsync(int id, string userId);
    }
}
