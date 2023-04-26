﻿using CarSales.Core.Contracts;
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
            var model = await vehicleService.GetAllVehiclesWhichAreForSale();
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await vehicleService.GetVehicleById(id);
            if (model == null)
            {
                TempData["error"] = "Vehicle does not exist!";
                return RedirectToAction("Index");
            }
            return View(model);
        }

    }
}