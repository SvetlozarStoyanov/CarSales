using CarSales.Core.Contracts;
using CarSales.Core.Models.Vehicles;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Web.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly IVehicleService vehicleService;
        private readonly IHtmlSanitizingService htmlSanitizingService;

        public VehiclesController(IVehicleService vehicleService, IHtmlSanitizingService htmlSanitizingService)
        {
            this.vehicleService = vehicleService;
            this.htmlSanitizingService = htmlSanitizingService;
        }

        public async Task<IActionResult> Index([FromQuery] VehiclesQueryModel model)
        {
            model = htmlSanitizingService.SanitizeObject(model);
            var queryResult = await vehicleService.GetVehiclesForSaleAsync(
                model.SearchTerm,
                model.VehiclesPerPage,
                model.CurrentPage,
                model.SelectedVehicleTypes,
                model.VehicleSorting);

            model = queryResult;

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
