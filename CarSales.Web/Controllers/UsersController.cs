using CarSales.Core.Contracts;
using CarSales.Core.Models.Users;
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
        private readonly IOwnerService ownerService;
        private readonly IUserService userService;
        private readonly IHtmlSanitizingService htmlSanitizingService;

        public UsersController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<Role> roleManager,
            IOwnerService ownerService,
            IUserService userService,
            IHtmlSanitizingService htmlSanitizingService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.ownerService = ownerService;
            this.userService = userService;
            this.htmlSanitizingService = htmlSanitizingService;
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
            model = htmlSanitizingService.SanitizeObject(model);
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
                Gender = model.Gender,
                ImageUrl = model.ImageUrl,
                PhoneNumber = model.PhoneNumber
            };
            var userNameIsTaken = await userManager.Users.AnyAsync(u => u.UserName == model.UserName);
            if (userNameIsTaken)
            {
                ModelState.AddModelError(string.Empty, "Username already taken!");
                TempData["error"] = "Username is already taken!";
                return RedirectToAction(nameof(Register));
            }
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                //await signInManager.SignInAsync(user, false);
                await userManager.AddToRoleAsync(user, "Owner");
                await ownerService.CreateOwnerAsync(user.Id);
                TempData["success"] = "Successful registration!";
                return RedirectToAction(nameof(Login));
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
            model = htmlSanitizingService.SanitizeObject(model);
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Invalid login!");
                return View(model);
            }

            return RedirectToAction(nameof(LoginRedirect));
        }
        public async Task<IActionResult> LoginRedirect()
        {
            var user = await userManager.GetUserAsync(User);
            if (await userManager.IsInRoleAsync(user, "Importer"))
            {
                return Redirect("/Importer/Home/Index");
            }
            else if (await userManager.IsInRoleAsync(user, "Salesman"))
            {
                return Redirect("/Salesman/Home/Index");
            }
            else if (await userManager.IsInRoleAsync(user, "Reviewer"))
            {
                return Redirect("/Reviewer/Home/Index");
            }
            else if (await userManager.IsInRoleAsync(user, "Administrator"))
            {
                return Redirect("/Admin/Home/Index");
            }
            else if (await userManager.IsInRoleAsync(user, "Owner"))
            {
                return Redirect("/Owner/Home/Index");
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Vehicles");
        }

        public async Task<IActionResult> LogoutAndLogin()
        {
            var user = await userManager.GetUserAsync(User);
            await signInManager.SignOutAsync();
            await signInManager.SignInAsync(user, false);
            return RedirectToAction(nameof(LoginRedirect));
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
