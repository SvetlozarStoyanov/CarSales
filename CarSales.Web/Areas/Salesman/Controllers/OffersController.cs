﻿using CarSales.Core.Contracts;
using CarSales.Core.Models.Offers;
using CarSales.Infrastructure.Data.Enums;
using CarSales.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Web.Areas.Salesman.Controllers
{
    public class OffersController : BaseController
    {
        private readonly IOfferService offerService;
        private readonly ISalesmanService salesmanService;
        private readonly IOwnerService ownerService;
        private readonly INotificationService notificationService;
        private readonly IHtmlSanitizingService htmlSanitizingService;

        public OffersController(IOfferService offerService,
            ISalesmanService salesmanService,
            IOwnerService ownerService,
            INotificationService notificationService,
            IHtmlSanitizingService htmlSanitizingService)
        {
            this.offerService = offerService;
            this.salesmanService = salesmanService;
            this.ownerService = ownerService;
            this.notificationService = notificationService;
            this.htmlSanitizingService = htmlSanitizingService;
        }
        public async Task<IActionResult> Outgoing([FromQuery] OffersQueryModel model)
        {
            model = htmlSanitizingService.SanitizeObject(model);
            var queryResult = await offerService.GetOwnerOffersAsync(User.Id(),
                model.CurrentPage,
                model.OffersPerPage,
                model.VehicleName,
                model.SalesmanName,
                model.OfferStatus,
                model.OfferSorting);

            model = queryResult;
            return View(model);
        }

        public async Task<IActionResult> Incoming([FromQuery] OffersQueryModel model)
        {
            model = htmlSanitizingService.SanitizeObject(model);
            var queryResult = await offerService.GetSalesmanOffersAsync(User.Id(),
                model.CurrentPage,
                model.OffersPerPage,
                model.VehicleName,
                model.OfferorName,
                model.OfferSorting);

            model = queryResult;
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var offerExists = await offerService.OfferExistsAsync(id);
            if (!offerExists)
            {
                TempData["error"] = "Offer not found!";
                return RedirectToAction(nameof(Incoming));
            }
            if (!await offerService.CanViewOfferAsync(User.Id(), id))
            {
                return RedirectToAction(nameof(Outgoing));
            }
            var model = await offerService.GetOfferByIdAsync(id);
            //Must be optimized to be included only for user's outgoing offers
            if (model.Status == OfferStatus.Pending)
            {
                model.OfferEditModel = await offerService.CreateOfferEditModelAsync(id);
            }
            ViewBag.CanRespondToOffer = await offerService.CanRespondToOfferAsync(User.Id(), id);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int vehicleId)
        {
            try
            {
                var model = await offerService.CreateOfferCreateModelAsync(User.Id(), vehicleId);
                return View(model);
            }
            catch (Exception)
            {
                TempData["error"] = "Cannot complete operation!";
                return RedirectToAction("Index", "Vehicles");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(OfferCreateModel model)
        {
            model = htmlSanitizingService.SanitizeObject(model);
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await offerService.CreateOfferAsync(model);

            var salesmanUserId = await salesmanService.GetSalesmanUserIdAsync(model.SalesmanId);
            var newOfferId = await offerService.GetOfferIdAsync(User.Id(), model.VehicleId);
            await notificationService.CreateNotificationAsync(salesmanUserId,
                "New offer received!",
                $"Offers/Details/{newOfferId}");

            TempData["success"] = "Succesfully created offer!";
            if (model.ReturnUrl != null)
            {
                return Redirect(model.ReturnUrl);

            }
            return RedirectToAction("Details", "Vehicles", new { id = model.VehicleId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await offerService.CreateOfferEditModelAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OfferEditModel model)
        {
            model = htmlSanitizingService.SanitizeObject(model);
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await offerService.EditOfferAsync(model);
            TempData["success"] = "Successfully edited offer";
            if (model.ReturnUrl != null)
            {
                return Redirect(model.ReturnUrl);
            }
            return RedirectToAction("Details", "Offers", new { id = model.Id });
        }

        public async Task<IActionResult> Cancel(int id)
        {
            await offerService.CancelOfferAsync(id);
            return RedirectToAction(nameof(Outgoing));
        }

        public async Task<IActionResult> Accept(int id)
        {
            if (!await offerService.CanViewOfferAsync(User.Id(), id))
            {
                return RedirectToAction(nameof(Outgoing));
            }
            var offer = await offerService.GetOfferByIdAsync(id);
            var ownerUserId = await ownerService.GetOwnerUserIdAsync(offer.OfferorId);
            await offerService.AcceptOfferAsync(id);
            await notificationService.CreateNotificationAsync(ownerUserId,
                "Offer accepted!",
                $"Offers/Details/{id}");
            TempData["success"] = "Offer accepted!";
            return RedirectToAction(nameof(Incoming));
        }


        public async Task<IActionResult> Decline(int id)
        {
            if (!await offerService.CanViewOfferAsync(User.Id(), id))
            {
                return RedirectToAction(nameof(Outgoing));
            }

            var offer = await offerService.GetOfferByIdAsync(id);
            var ownerUserId = await ownerService.GetOwnerUserIdAsync(offer.OfferorId);
            await offerService.DeclineOfferAsync(id);
            await notificationService.CreateNotificationAsync(ownerUserId,
               "Offer declined!",
               $"Offers/Details/{id}");
            TempData["success"] = "Offer declined!";
            return RedirectToAction(nameof(Incoming));
        }
    }
}
