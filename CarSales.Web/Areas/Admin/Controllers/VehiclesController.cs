﻿using CarSales.Core.Contracts;
using CarSales.Core.Models.Vehicles;
using CarSales.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Web.Areas.Admin.Controllers
{
    public class VehiclesController : BaseController
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
                model.SelectedVehicleTypes,
                model.VehicleSorting);

            model.SearchTerm = queryResult.SearchTerm;
            model.VehiclesPerPage = queryResult.VehiclesPerPage;
            model.CurrentPage = queryResult.CurrentPage;
            model.VehiclesCount = queryResult.VehiclesCount;
            model.VehicleSorting = queryResult.VehicleSorting;
            model.SortingOptions = queryResult.SortingOptions;
            model.VehicleTypes = queryResult.VehicleTypes;
            model.SelectedVehicleTypes = queryResult.SelectedVehicleTypes;
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

        public async Task<IActionResult> Mine([FromQuery] VehiclesQueryModel model)
        {
            var queryResult = await vehicleService.GetOwnerVehiclesAsync(
                User.Id(),
                model.SearchTerm,
                model.VehiclesPerPage,
                model.CurrentPage,
                model.SelectedVehicleTypes,
                model.VehicleSorting);

            model.SearchTerm = queryResult.SearchTerm;
            model.VehiclesPerPage = queryResult.VehiclesPerPage;
            model.CurrentPage = queryResult.CurrentPage;
            model.VehiclesCount = queryResult.VehiclesCount;
            model.VehicleSorting = queryResult.VehicleSorting;
            model.SortingOptions = queryResult.SortingOptions;
            model.VehicleTypes = queryResult.VehicleTypes;
            model.SelectedVehicleTypes = queryResult.SelectedVehicleTypes;
            model.Vehicles = queryResult.Vehicles;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRating(VehicleChangeRatingModel model)
        {
            await vehicleService.ChangeVehicleRatingAsync(model.Id, (int)model.NewRating);
            return RedirectToAction(nameof(Details), new { id = model.Id });
        }
    }

}
