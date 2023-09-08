using CarSales.Core.Contracts;
using CarSales.Infrastructure.Data.Entities;
using CarSales.Web.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Web.Areas.Importer.Controllers
{
    public class RoleRequestsController : BaseController
        {
            private readonly RoleManager<Role> roleManager;
            private readonly UserManager<User> userManager;
            private readonly IRoleService roleService;
            private readonly IRoleRequestService roleRequestService;

            public RoleRequestsController(UserManager<User> userManager, RoleManager<Role> roleManager, IRoleService roleService, IRoleRequestService roleRequestService)
            {
                this.userManager = userManager;
                this.roleManager = roleManager;
                this.roleService = roleService;
                this.roleRequestService = roleRequestService;
            }

            public async Task<IActionResult> Index()
            {
                var user = await userManager.GetUserAsync(User);
                var userRoleNames = await userManager.GetRolesAsync(user);
                ViewBag.UserRoles = await roleService.GetUserRolesAsync(userRoleNames);
                var requestedRoles = await roleRequestService.GetRoleRequestsByUserIdAsync(User.Id());
                if (requestedRoles.Any())
                {
                    ViewBag.RequestedRoles = await roleService.GetRoleByIdsAsync(requestedRoles.Select(rrq => rrq.RoleId));
                    ViewBag.RequestedRoleIds = requestedRoles.Select(rrq => rrq.Id).ToArray();
                    return View(null);
                }
                var model = await roleService.GetRequestableRolesAsync(userRoleNames);
                return View(model);
            }

            public async Task<IActionResult> RequestRole(string roleName)
            {
                if (!roleManager.Roles.Any(r => r.Name == roleName))
                {
                    TempData.Add("error", $"No role with {roleName}!");
                    return RedirectToAction(nameof(Index));
                }
                var roleId = roleManager.Roles.FirstOrDefault(r => r.Name == roleName).Id;
                await roleRequestService.CreateRoleRequestAsync(roleId, User.Id());
                TempData.Add("success", $"Successfully requested role {roleName}!");
                return RedirectToAction(nameof(Index));
            }

            public async Task<IActionResult> LeaveRole(string roleName)
            {
                var user = await userManager.GetUserAsync(User);
                await userManager.RemoveFromRoleAsync(user, roleName);
                TempData.Add("success", $"You are no longer a {roleName}!");
                
                return RedirectToAction("LeaveImporterRole", "Importers");
            }

            public async Task<IActionResult> DeleteRoleRequest(int id)
            {
                await roleRequestService.DeleteRoleRequestAsync(id);
                TempData.Add("success", $"Successfully cancelled role request!");

                return RedirectToAction(nameof(Index));
            }
        }
    }

