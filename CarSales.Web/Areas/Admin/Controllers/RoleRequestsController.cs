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

        public RoleRequestsController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager, IRoleRequestService roleRequestService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.roleRequestService = roleRequestService;
        }

        public async Task<IActionResult> Index([FromQuery] RoleRequestsQueryModel model)
        {
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
            TempData.Add("success", $"User is no longer in {model.RoleName} role!");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteRequest(int id)
        {
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
