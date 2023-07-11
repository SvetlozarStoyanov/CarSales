﻿using CarSales.Core.Contracts;
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
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await userService.EditUserAsync(model);
            return RedirectToAction("Index", "Home", new { area = "Owner" });
        }
    }
}
