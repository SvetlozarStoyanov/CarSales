﻿using CarSales.Core.Contracts;
using CarSales.Core.Models.Offers;
using CarSales.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Web.Areas.Owner.Controllers
{
    public class OffersController : BaseController
    {
        private readonly IOfferService offerService;
        public OffersController(IOfferService offerService)
        {
            this.offerService = offerService;
        }

        public async Task<IActionResult> Outgoing()
        {
            var model = await offerService.GetOwnerOffersAsync(User.Id());
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (!await offerService.CanViewOfferAsync(User.Id(), id))
            {
                return RedirectToAction(nameof(Outgoing));
            }
            var model = await offerService.GetOfferByIdAsync(id);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int vehicleId)
        {
            var model = await offerService.CreateOfferCreateModelAsync(User.Id(), vehicleId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OfferCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await offerService.CreateOfferAsync(model);
            TempData["success"] = "Succesfully created offer";
            return RedirectToAction("Index", "Vehicles");
        }

        [HttpGet]
        public async Task<IActionResult> Edit (int id)
        {
            var model = await offerService.CreateOfferEditModelAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OfferEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await offerService.EditOfferAsync(model);

            return RedirectToAction("Details", "Offers", new { id = model.Id });
        }

        public async Task<IActionResult> Cancel(int id)
        {
            await offerService.CancelOfferAsync(id);
            return RedirectToAction(nameof(Outgoing));
        }
    }
}
