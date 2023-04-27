﻿using CarSales.Core.Contracts;
using CarSales.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Web.Areas.Salesman.Controllers
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
            var model = await vehicleService.GetAllVehiclesWhichAreForSaleAsync();
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

        public async Task<IActionResult> MyVehiclesOnSale()
        {
            var model = await vehicleService.GetSalesmanVehiclesAsync(User.Id());
            return View(model);
        }
    }

}