using CarSales.Core.Contracts;
using CarSales.Core.Models.Notifications;
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

        public async Task<IActionResult> GetNotificationsPartial()
        {
            var model = await notificationService.GetLatestNotificationsAsync(User.Id());
            
            return PartialView("_NotificationsPartial", model);
        }

        public async Task<IActionResult> Details(int notificationId, string link, bool isRead)
        {
            if (!isRead)
            {
                await notificationService.MarkNotificationAsReadAsync(notificationId);
            }
            var linkSplit = link.Split('/');
            var area = "Owner";
            var controller = linkSplit[0];
            var action = linkSplit[1];
            var id = string.Empty;
            if (linkSplit.Length > 2)
            {
                id = linkSplit[2];
            }
            if (link.Contains("Logout"))
            {
                area = string.Empty;
                //return RedirectToAction("LogoutAndLogin", "Users", new { area = "" });
            }
            return RedirectToAction(action, controller, new { id, area });
        }
    }
}
