using CarSales.Core.Contracts;
using CarSales.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Web.Areas.Owner.Controllers
{
    public class NotificationsController : BaseController
    {
        private readonly INotificationService notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await notificationService.GetAllNotificationsAsync(User.Id());
            return View(model);
        }
    }
}
