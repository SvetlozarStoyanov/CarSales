using CarSales.Core.Contracts;
using CarSales.Core.Models.Vehicles;
using CarSales.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Web.Areas.Admin.Controllers
{
    public class VehiclesController : BaseController
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

        public async Task<IActionResult> Mine([FromQuery] VehiclesQueryModel model)
        {
            var queryResult = await vehicleService.GetOwnerVehiclesAsync(
                User.Id(),
                model.SearchTerm,
                model.VehiclesPerPage,
                model.CurrentPage,
                model.SelectedVehicleTypes,
                model.VehicleSorting);

            model = queryResult;

            return View(model);
        }


    }

}
