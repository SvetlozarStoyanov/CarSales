using CarSales.Core.Contracts;
using CarSales.Core.Models.Users;
using CarSales.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Web.Areas.Owner.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<IActionResult> MyAccount()
        {
            var model = await userService.GetUserByIdAsync(User.Id());
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditModel userEditModel)
        {
            if (ModelState.IsValid)
            {
                await userService.EditUserAsync(userEditModel);
            }
            return RedirectToAction(nameof(MyAccount));
        }
    }
}
