namespace CarSales.Core.Models.Notifications
{
    public class NotificationViewModel
    {
        public int Id { get; init; }
        public string Title { get; init; } = null!;
        public string Link { get; init; } = null!;
    }
}
