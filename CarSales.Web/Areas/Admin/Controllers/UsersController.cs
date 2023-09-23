using CarSales.Core.Contracts;
using CarSales.Core.Models.Users;
using CarSales.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Web.Areas.Admin.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService userService;
        private readonly IHtmlSanitizingService htmlSanitizingService;

        public UsersController(IUserService userService, IHtmlSanitizingService htmlSanitizingService)
        {
            this.userService = userService;
            this.htmlSanitizingService = htmlSanitizingService;
        }

        public async Task<IActionResult> Details(string id)
        {
            var model = await userService.GetUserByIdAsync(id);
            ViewBag.CanEditProfile = await userService.CanEditProfileAsync(id, User.Id());
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var model = await userService.CreateUserEditModelAsync(id);
            var canEditProfile = await userService.CanEditProfileAsync(id, User.Id());
            if (!canEditProfile)
            {
                TempData["error"] = "Can only edit own profile!";
                return RedirectToAction(nameof(Details), new { id = id });
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditModel model)
        {
            model = htmlSanitizingService.SanitizeObject(model);
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await userService.IsUserNameTakenAsync(User.Id(), model.UserName))
            {
                TempData["error"] = $"Username {model.UserName} is already taken!";
                return View(model);
            }

            await userService.EditUserAsync(model);
            return RedirectToAction(nameof(Details), new { id = model.Id });
        }
    }
}
