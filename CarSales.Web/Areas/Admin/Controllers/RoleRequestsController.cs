using CarSales.Core.Contracts;
using CarSales.Core.Models.RoleRequests;
using CarSales.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Web.Areas.Admin.Controllers
{
    public class RoleRequestsController : BaseController
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<Role> roleManager;
        private readonly IRoleRequestService roleRequestService;
        private readonly INotificationService notificationService;
        private readonly IHtmlSanitizingService htmlSanitizingService;

        public RoleRequestsController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<Role> roleManager,
            IRoleRequestService roleRequestService,
            INotificationService notificationService,
            IHtmlSanitizingService htmlSanitizingService
            )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.roleRequestService = roleRequestService;
            this.notificationService = notificationService;
            this.htmlSanitizingService = htmlSanitizingService;
        }

        public async Task<IActionResult> Index([FromQuery] RoleRequestsQueryModel model)
        {
            model = htmlSanitizingService.SanitizeObject(model);
            var queryResult = await roleRequestService.GetRoleRequestsAsync(model.SearchTerm,
                model.CurrentPage,
                model.RoleRequestsPerPage,
                model.SelectedUserName,
                model.SelectedRoleNames,
                model.Sorting);
            model = queryResult;
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await roleRequestService.GetRoleRequestByIdAsync(id);
            return View(model);
        }

        public async Task<IActionResult> GrantRole([FromQuery] RoleRequestRouteModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                TempData.Add("error", "There is no such user");
                return RedirectToAction(nameof(Index));
            }
            await userManager.AddToRoleAsync(user, model.RoleName);
            await notificationService.CreateNotificationAsync(model.UserId,
                $"You have been assigned to {model.RoleName} role!",
                "Users/LogoutAndLogin");
            TempData.Add("success", $"Successfully assigned user to role {model.RoleName}!");
            await roleRequestService.DeleteRoleRequestAsync(int.Parse(model.RoleRequestId));
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoveFromRole([FromQuery] RoleRequestRouteModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                TempData.Add("error", "There is no such user");
                return RedirectToAction(nameof(Index));
            }
            await userManager.RemoveFromRoleAsync(user, model.RoleName);
            await notificationService.CreateNotificationAsync(model.UserId,
                $"Your have been removed from {model.RoleName} role!",
                "Home/Index");
            TempData.Add("success", $"User is no longer in {model.RoleName} role!");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteRequest(int id)
        {
            var roleRequest = await roleRequestService.GetRoleRequestByIdAsync(id);
            await notificationService.CreateNotificationAsync(roleRequest.UserModel.Id,
                $"Your request to join {roleRequest.RoleName} role has been denied!",
                "Home/Index");
            await roleRequestService.DeleteRoleRequestAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task LogoutAndLogin(User user)
        {
            await signInManager.SignOutAsync();
            await signInManager.SignInAsync(user, isPersistent: false);
        }
    }
}
