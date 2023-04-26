using CarSales.Core.Contracts;
using CarSales.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Web.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly IVehicleService vehicleService;

        public VehiclesController(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await vehicleService.GetAllVehiclesWhichAreForSaleAsync();
            var userId = User.Id();
            var userIsInRole = User.IsInRole("Owner");

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await vehicleService.GetVehicleByIdAsync(id);
            if (model == null)
            {
                TempData["error"] = "Vehicle does not exist!";
                return RedirectToAction("Index");
            }
            return View(model);
        }

    }
}
