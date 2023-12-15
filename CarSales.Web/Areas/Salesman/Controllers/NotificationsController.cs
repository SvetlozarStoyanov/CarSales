using CarSales.Core.Contracts;
using CarSales.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Web.Areas.Salesman.Controllers
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

        public async Task<IActionResult> GetNotifications(int skipped)
        {
            var model = await notificationService.GetNotificationsAsync(User.Id(), skipped);
            return PartialView("_NotificationsPartial", model);
        }

        public async Task<IActionResult> DoesUserHaveUnreadNotifications()
        {
            var model = await notificationService.DoesUserHaveUnreadNotificationsAsync(User.Id());
            return Json(model);
        }

        public async Task<IActionResult> GetNotificationsDropdownPartial()
        {
            var model = await notificationService.GetLatestNotificationsAsync(User.Id());

            return PartialView("_NotificationsDropdownPartial", model);
        }

        public async Task MarkAsRead(int id)
        {
            var canViewNotification = await notificationService.CanUserViewNotificationAsync(id, User.Id());
            if (canViewNotification)
            {
                await notificationService.MarkNotificationAsReadAsync(id);
            }
        }

        public async Task<IActionResult> Details(int notificationId, string link, bool isRead)
        {
            var canViewNotification = await notificationService.CanUserViewNotificationAsync(notificationId, User.Id());
            if (!canViewNotification)
            {
                return RedirectToAction("Home", "Index");
            }
            if (!isRead)
            {
                await notificationService.MarkNotificationAsReadAsync(notificationId);
            }
            var linkSplit = link.Split('/');
            var area = "Salesman";
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
