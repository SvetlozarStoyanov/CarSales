using CarSales.Core.Contracts;
using CarSales.Core.Models.Vehicles;
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

        public async Task<IActionResult> Index([FromQuery] VehiclesQueryModel model)
        {
            var queryResult = await vehicleService.GetAllVehiclesForSaleAsync(
                model.SearchTerm,
                model.VehiclesPerPage,
                model.CurrentPage,
                model.VehicleSorting
                /*model.SelectedVehicleTypes*/);

            model.SearchTerm = queryResult.SearchTerm;
            model.VehiclesPerPage = queryResult.VehiclesPerPage;
            model.CurrentPage = queryResult.CurrentPage;
            model.VehiclesCount = queryResult.VehiclesCount;
            model.VehicleSorting = queryResult.VehicleSorting;
            model.SortingOptions = queryResult.SortingOptions;
            model.VehicleTypes = queryResult.VehicleTypes;
            //model.SelectedVehicleTypes = queryResult.SelectedVehicleTypes;
            model.Vehicles = queryResult.Vehicles;

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
