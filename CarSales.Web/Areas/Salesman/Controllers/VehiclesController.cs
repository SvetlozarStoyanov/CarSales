﻿using CarSales.Core.Contracts;
using CarSales.Core.Models.Vehicles;
using CarSales.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Web.Areas.Salesman.Controllers
{
    public class VehiclesController : BaseController
    {
        private readonly IVehicleService vehicleService;
        private readonly IOfferService offerService;
        private readonly IUserService userService;

        public VehiclesController(IVehicleService vehicleService, IOfferService offerService, IUserService userService)
        {
            this.vehicleService = vehicleService;
            this.offerService = offerService;
            this.userService = userService;
        }

        public async Task<IActionResult> Index([FromQuery] VehiclesQueryModel model)
        {
            var queryResult = await vehicleService.GetAllVehiclesForSaleAsync(
                model.SearchTerm,
                model.VehiclesPerPage,
                model.CurrentPage,
                model.SelectedVehicleTypes,
                model.VehicleSorting);

            model = queryResult;

            return View(model);
        }

        public async Task<IActionResult> Imported([FromQuery] VehiclesQueryModel model)
        {
            var queryResult = await vehicleService.GetImportedVehicles(
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
            ViewBag.CanMakeOffer = await offerService.CanCreateOfferAsync(User.Id(), id);
            ViewBag.CanMakeOffer = await offerService.CanCreateOfferAsync(User.Id(), id);
            if (!ViewBag.CanMakeOffer)
            {
                var offerId = await offerService.GetOfferIdAsync(User.Id(), id);
                ViewBag.AvailableCredits = await userService.GetUserAvailableCreditsAsync(User.Id(), offerId);
            }
            else
            {
                ViewBag.AvailableCredits = await userService.GetUserAvailableCreditsAsync(User.Id());
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

        public async Task<IActionResult> MyVehiclesOnSale([FromQuery] VehiclesQueryModel model)
        {
            var queryResult = await vehicleService.GetSalesmanVehiclesAsync(
                User.Id(),
                model.SearchTerm,
                model.VehiclesPerPage,
                model.CurrentPage,
                model.SelectedVehicleTypes,
                model.VehicleSorting);

            model = queryResult;

            return View(model);
        }



        public async Task<IActionResult> BuyFromSalesman(int id)
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

        public async Task<IActionResult> BuyFromImporter(int id)
        {
            try
            {
                await vehicleService.BuyVehicleFromImporterAsync(id, User.Id());
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
                return RedirectToAction(nameof(Imported));
            }
            return RedirectToAction(nameof(Details), new { id = id });
        }

        [HttpGet]
        public async Task<IActionResult> Sell(int id)
        {
            var model = await vehicleService.CreateVehicleSellModelAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Sell(VehicleSellModel model)
        {
            try
            {
                if (model.OldPrice < model.Price)
                {
                    model = await vehicleService.CreateVehicleSellModelAsync(model.Id);
                    return View(model);
                }
                await vehicleService.PutVehicleForSaleAsync(model);
            }
            catch (Exception e)
            {

            }
            return RedirectToAction(nameof(MyVehiclesOnSale));
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
            return RedirectToAction(nameof(MyVehiclesOnSale));
        }
    }
}
