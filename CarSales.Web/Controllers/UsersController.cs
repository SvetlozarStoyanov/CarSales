﻿using CarSales.Core.Models.Users;
using CarSales.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarSales.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<Role> roleManager;
        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }


        [HttpGet]
        public IActionResult Register()
        {
            var model = new UserRegisterModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Username,
                Email = model.Email
            };
            var userNameIsTaken = await userManager.Users.AnyAsync(u => u.UserName == model.Username);
            if (userNameIsTaken)
            {
                ModelState.AddModelError(string.Empty, "Username already taken!");
                TempData["error"] = "Username is already taken!";
                return RedirectToAction(nameof(Register));
            }
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, false);
                await userManager.AddToRoleAsync(user, "Owner");
                TempData["success"] = "Successful registration!";
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Registration unsuccessful!");

            return RedirectToAction(nameof(Register));
        }

        [HttpGet]
        public IActionResult Login()
        {
            var model = new UserLoginModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Invalid login!");
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }



        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}