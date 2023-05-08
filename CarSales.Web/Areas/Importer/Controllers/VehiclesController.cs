﻿using CarSales.Core.Contracts;
using CarSales.Core.Models.Vehicles;
using CarSales.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Web.Areas.Importer.Controllers
{
    public class VehiclesController : BaseController
    {
        private readonly IVehicleService vehicleService;

        public VehiclesController(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await vehicleService.GetAllVehiclesForSaleAsync();
            return View(model);
        }

        public async Task<IActionResult> AllImportedVehicles()
        {
            var model = await vehicleService.GetAllImportedVehiclesAsync();
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

        public async Task<IActionResult> Mine()
        {
            var model = await vehicleService.GetOwnerVehiclesAsync(User.Id());
            return View(model);
        }

        public async Task<IActionResult> MyImportedVehicles()
        {
            var model = await vehicleService.GetImporterVehiclesAsync(User.Id());
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRating(VehicleChangeRatingModel model)
        {
            await vehicleService.ChangeVehicleRatingAsync(model.Id, (int)model.NewRating);
            return RedirectToAction(nameof(Details), new { id = model.Id });
        }

        public async Task<IActionResult> Buy(int id)
        {
            try
            {
                await vehicleService.BuyVehicleFromSalesmanAsync(id, User.Id());
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Details), new { id = id });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await vehicleService.CreateVehicleSellModelAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(VehicleSellModel model)
        {
            try
            {
                if (model.OldPrice < model.Price)
                {
                    model = await vehicleService.CreateVehicleSellModelAsync(model.Id);
                    return View(model);
                }
                await vehicleService.EditVehicleForSaleAsync(model);
            }
            catch (Exception e)
            {

            }
            return RedirectToAction(nameof(MyImportedVehicles));
        }

        [HttpGet]
        public async Task<IActionResult> Import()
        {
            var model = await vehicleService.CreateVehicleImportModelAsync(User.Id());
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Import(VehicleImportModel model)
        {
            if (!ModelState.IsValid)
            {
                model = await vehicleService.CreateVehicleImportModelAsync(User.Id());
                return View(model);
            }
            await vehicleService.ImportVehicleAsync(model);
            return RedirectToAction(nameof(Index));
        }
    }
}
