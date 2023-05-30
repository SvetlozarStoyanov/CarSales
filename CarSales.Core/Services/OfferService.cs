using AirsoftMatchMaker.Infrastructure.Data.Common.Repository;
using CarSales.Core.Contracts;
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
            if (owner.Offers.Any(of => of.VehicleId == vehicleId))
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
                .FirstOrDefaultAsync();
            if (offer.Offeror.UserId != userId)
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

        public async Task<IEnumerable<OfferListModel>> GetOwnerOffersAsync(string userId)
        {
            var offers = await repository.AllReadOnly<Owner>()
                .Where(s => s.UserId == userId)
                .Select(s => s.Offers.Select(o => new OfferListModel()
                {
                    Id = o.Id,
                    Status = o.Status,
                    VehicleId = o.VehicleId,
                    VehicleName = $"{o.Vehicle.Brand} {o.Vehicle.Model}",
                    SalesmanId = o.OfferorId,
                    SalesmanName = $"{o.Salesman.User.FirstName} {o.Salesman.User.LastName}"
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
                        VehicleRating = o.Vehicle.VehicleRating,
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
            var offerorId = await repository.AllReadOnly<Owner>()
                .Where(o => o.UserId == userId)
                .Select(o => o.Id)
                .FirstOrDefaultAsync();
            var model = new OfferCreateModel()
            {
                Price = vehicle.Price,
                InitialPrice = vehicle.Price,
                VehicleId = vehicleId,
                OfferorId = offerorId,
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
            offer.Vehicle.OwnerId = offer.VehicleId;
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
