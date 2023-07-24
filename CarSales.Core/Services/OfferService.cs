﻿using AirsoftMatchMaker.Infrastructure.Data.Common.Repository;
using CarSales.Core.Contracts;
using CarSales.Core.Enums;
using CarSales.Core.Models.Offers;
using CarSales.Core.Models.Vehicles;
using CarSales.Infrastructure.Data.Entities;
using CarSales.Infrastructure.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace CarSales.Core.Services
{
    public class OfferService : IOfferService
    {
        private readonly IRepository repository;

        public OfferService(IRepository repository)
        {
            this.repository = repository;
        }


        public async Task<bool> CanCreateOfferAsync(string userId, int vehicleId)
        {
            var owner = await repository.AllReadOnly<Owner>()
                .Where(o => o.UserId == userId)
                .Include(o => o.Offers)
                .FirstOrDefaultAsync();
            var vehicle = await repository.AllReadOnly<Vehicle>()
                .Where(v => v.Id == vehicleId)
                .FirstOrDefaultAsync();
            if (vehicle.SalesmanId == null || owner.Offers.Any(of => of.VehicleId == vehicleId))
            {
                return false;
            }
            return true;
        }

        public async Task<bool> CanViewOfferAsync(string userId, int offerId)
        {
            var offer = await repository.AllReadOnly<Offer>()
                .Where(o => o.Id == offerId)
                .Include(o => o.Offeror)
                .Include(o => o.Salesman)
                .FirstOrDefaultAsync();
            if (offer.Offeror.UserId != userId && offer.Salesman.UserId != userId)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> CanRespondToOfferAsync(string userId, int offerId)
        {
            var offer = await repository.AllReadOnly<Offer>()
                .Where(o => o.Id == offerId)
                .Include(o => o.Salesman)
                .FirstOrDefaultAsync();
            if (offer.Salesman.UserId != userId)
            {
                return false;
            }
            return true;
        }

        public async Task<int> GetOfferIdAsync(string userId, int vehicleId)
        {
            var offerId = await repository.AllReadOnly<Offer>()
                .Where(o => o.Offeror.UserId == userId && o.VehicleId == vehicleId)
                .Select(o => o.Id)
                .FirstOrDefaultAsync();

            return offerId;
        }



        public async Task<OffersQueryModel> GetOwnerOffersAsync(string userId,
            int currentPage = 1,
            int offersPerPage = 12,
            string vehicleName = null,
            OfferSorting offerSorting = OfferSorting.Newest)
        {
            var owner = await repository.AllReadOnly<Owner>()
                .FirstOrDefaultAsync(o => o.UserId == userId);

            var offers = await repository.AllReadOnly<Offer>()
                .Where(o => o.OfferorId == owner.Id)
                .Include(o => o.Vehicle)
                .Include(o => o.Salesman)
                .ThenInclude(s => s.User)
                .ToListAsync();

            var vehicleNames = offers.Select(r => $"{r.Vehicle.Brand} {r.Vehicle.Model}").ToHashSet();
            if (!string.IsNullOrWhiteSpace(vehicleName))
            {
                offers = offers
                    .Where(o => $"{o.Vehicle.Brand} {o.Vehicle.Model}".ToLower() == vehicleName.ToLower())
                    .ToList();
            }
            switch (offerSorting)
            {
                case OfferSorting.Newest:
                    offers = offers.OrderByDescending(o => o.Id).ToList();
                    break;
                case OfferSorting.Oldest:
                    offers = offers.OrderBy(o => o.Id).ToList();
                    break;
                case OfferSorting.PriceDescending:
                    offers = offers.OrderByDescending(o => o.Price).ToList();
                    break;
                case OfferSorting.PriceAscending:
                    offers = offers.OrderBy(o => o.Price).ToList();
                    break;
            }

            var offerCount = offers.Count;
            var sortedOffers = offers
                .Skip(offersPerPage * (currentPage - 1))
                .Take(offersPerPage)
                .Select(o => new OfferListModel()
                {
                    Id = o.Id,
                    Status = o.Status,
                    VehicleId = o.VehicleId,
                    VehicleName = $"{o.Vehicle.Brand} {o.Vehicle.Model}",
                    SalesmanId = o.SalesmanId,
                    SalesmanName = $"{o.Salesman.User.FirstName} {o.Salesman.User.LastName}"
                })
                .ToList();


            var queryModel = CreateOffersQueryModel(currentPage, offersPerPage, vehicleName, offerSorting, offerCount, vehicleNames, sortedOffers);

            return queryModel;
        }

        public async Task<IEnumerable<OfferListModel>> GetSalesmanOffersAsync(string userId)
        {
            var offers = await repository.AllReadOnly<Salesman>()
                .Where(s => s.UserId == userId)
                .Select(s => s.Offers.Where(o => o.Status == OfferStatus.Pending).Select(o => new OfferListModel()
                {
                    Id = o.Id,
                    VehicleId = o.VehicleId,
                    VehicleName = $"{o.Vehicle.Brand} {o.Vehicle.Model}",
                    OfferorId = o.OfferorId,
                    OfferorName = $"{o.Offeror.User.FirstName} {o.Offeror.User.LastName}"
                }))
                .FirstOrDefaultAsync();

            return offers;
        }

        public async Task<OfferViewModel> GetOfferByIdAsync(int id)
        {
            var offer = await repository.AllReadOnly<Offer>()
                .Where(o => o.Id == id)
                .Select(o => new OfferViewModel()
                {
                    Id = o.Id,
                    Description = o.Description,
                    Price = o.Price,
                    Status = o.Status,
                    VehicleId = o.VehicleId,
                    Vehicle = new VehicleMinModel()
                    {
                        Name = $"{o.Vehicle.Brand} {o.Vehicle.Model}",
                        ImageUrl = o.Vehicle.ImageUrl,
                        Price = o.Vehicle.Price,
                        SalesmanId = o.Vehicle.SalesmanId,
                        SalesmanName = $"{o.Vehicle.Salesman.User.FirstName} {o.Vehicle.Salesman.User.LastName}",
                        VehicleType = o.Vehicle.VehicleType,
                        VehicleRating = o.Vehicle.Reviews.Count() > 0 ? (VehicleRating)o.Vehicle.Reviews.Where(r => r.ReviewStatus == ReviewStatus.Completed).Average(r => (int)r.VehicleRating)
                        : VehicleRating.NotRated,
                    },
                    OfferorId = o.OfferorId,
                    OfferorName = $"{o.Offeror.User.FirstName} {o.Offeror.User.LastName}",
                    SalesmanId = o.OfferorId,
                    SalesmanName = $"{o.Salesman.User.FirstName} {o.Salesman.User.LastName}"
                })
                .FirstOrDefaultAsync();

            return offer;
        }

        public async Task<OfferCreateModel> CreateOfferCreateModelAsync(string userId, int vehicleId)
        {
            var vehicle = await repository.GetByIdAsync<Vehicle>(vehicleId);
            if (vehicle.SalesmanId == null)
            {
                throw new InvalidOperationException("Vehicle is not for sale!");
            }
            var offeror = await repository.AllReadOnly<Owner>()
                .Where(o => o.UserId == userId)
                .Include(o => o.User)
                .Include(o => o.Offers)
                .FirstOrDefaultAsync();

            var availableCredits = offeror.User.Credits - offeror.Offers
                .Where(o => o.Status == OfferStatus.Pending)
                .Sum(o => o.Price);

            var model = new OfferCreateModel()
            {
                Price = vehicle.Price,
                InitialPrice = vehicle.Price,
                AvailableCredits = availableCredits,
                VehicleId = vehicleId,
                OfferorId = offeror.Id,
                SalesmanId = (int)vehicle.SalesmanId
            };
            return model;
        }

        public async Task CreateOfferAsync(OfferCreateModel model)
        {
            var offer = new Offer()
            {
                Description = model.Description,
                Price = model.Price,
                Status = OfferStatus.Pending,
                VehicleId = model.VehicleId,
                OfferorId = model.OfferorId,
                SalesmanId = model.SalesmanId
            };
            await repository.AddAsync(offer);
            await repository.SaveChangesAsync();
        }

        public async Task<OfferEditModel> CreateOfferEditModelAsync(int id)
        {
            var offer = await repository.AllReadOnly<Offer>()
                .Where(o => o.Id == id)
                //.Select(o => new OfferEditModel()
                //{
                //    Id = o.Id,
                //    Description = o.Description,
                //    Price = o.Price,
                //    OldPrice = o.Price
                //})
                .Include(o => o.Offeror)
                .ThenInclude(o => o.User)
                .FirstOrDefaultAsync();
            var offeror = await repository.AllReadOnly<Owner>()
                .Where(o => o.Id == offer.OfferorId)
                .Include(o => o.User)
                .Include(o => o.Offers)
                .FirstOrDefaultAsync();

            var offerSum = offeror.Offers
                .Where(o => o.Status == OfferStatus.Pending && o.Id != id)
                .Sum(o => o.Price);
            var availableCredits = offeror.User.Credits - offerSum;
            var model = new OfferEditModel()
            {
                Id = offer.Id,
                Description = offer.Description,
                Price = offer.Price,
                OldPrice = offer.Price,
                AvailableCredits = availableCredits
            };

            return model;
        }

        public async Task EditOfferAsync(OfferEditModel model)
        {
            var offer = await repository.GetByIdAsync<Offer>(model.Id);

            offer.Description = model.Description;
            offer.Price = model.Price;

            await repository.SaveChangesAsync();
        }

        public async Task AcceptOfferAsync(int id)
        {
            var offer = await repository.All<Offer>()
                .Where(o => o.Id == id)
                .Include(o => o.Offeror)
                .ThenInclude(o => o.User)
                .Include(o => o.Salesman)
                .ThenInclude(s => s.User)
                .Include(o => o.Vehicle)
                .FirstOrDefaultAsync();
            offer.Status = OfferStatus.Accepted;
            offer.Offeror.User.Credits -= offer.Price;
            offer.Salesman.User.Credits += offer.Price;
            offer.Vehicle.OwnerId = offer.OfferorId;
            offer.Vehicle.SalesmanId = null;
            await repository.SaveChangesAsync();

            await DeclineAllOffersForVehicleAsync(offer.VehicleId);
        }

        public async Task DeclineOfferAsync(int id)
        {
            var offer = await repository.GetByIdAsync<Offer>(id);
            offer.Status = OfferStatus.Declined;

            await repository.SaveChangesAsync();
        }

        public async Task CancelOfferAsync(int id)
        {
            await repository.DeleteAsync<Offer>(id);
            await repository.SaveChangesAsync();
        }

        private OffersQueryModel CreateOffersQueryModel(int currentPage,
            int offersPerPage,
            string? vehicleName,
            OfferSorting offerSorting,
            int offerCount,
            ICollection<string> vehicleNames,
            IEnumerable<OfferListModel> offers)
        {
            var model = new OffersQueryModel()
            {
                CurrentPage = currentPage,
                OffersPerPage = offersPerPage,
                MaxPage = (int)Math.Ceiling(offerCount / (double)offersPerPage),
                VehicleName = vehicleName,
                OfferSorting = offerSorting,
                OfferCount = offerCount,
                VehicleNames = vehicleNames,
                Offers = offers
            };
            if (model.MaxPage > 1)
            {
                var previousPages = new HashSet<int>();
                var nextPages = new HashSet<int>();
                var numberOfPages = 0;
                var index = 1;
                while (numberOfPages < 4 && numberOfPages < model.MaxPage - 1)
                {
                    var previousPage = model.CurrentPage - index;
                    var nextPage = model.CurrentPage + index;
                    if (previousPage >= 1)
                    {
                        previousPages.Add(previousPage);
                        numberOfPages++;
                    }
                    if (nextPage <= model.MaxPage)
                    {
                        nextPages.Add(nextPage);
                        numberOfPages++;
                    }
                    index++;
                }
                model.PreviousPages = previousPages.Reverse();
                model.NextPages = nextPages;
            }
            return model;
        }

        private async Task DeclineAllOffersForVehicleAsync(int vehicleId)
        {
            var offers = await repository.All<Offer>()
                .Where(o => o.VehicleId == vehicleId && o.Status == OfferStatus.Pending)
                .ToListAsync();
            foreach (var offer in offers)
            {
                offer.Status = OfferStatus.Declined;
            }
            await repository.SaveChangesAsync();
        }

    }
}
