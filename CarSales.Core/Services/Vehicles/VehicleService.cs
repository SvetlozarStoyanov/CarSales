using AirsoftMatchMaker.Infrastructure.Data.Common.Repository;
using CarSales.Core.Contracts;
using CarSales.Core.Models.Vehicles;
using CarSales.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarSales.Core.Services.Vehicles
{
    public class VehicleService : IVehicleService
    {
        private readonly IRepository repository;
        public VehicleService(IRepository repository)
        {
            this.repository = repository;
        }
        public async Task<ICollection<VehicleListViewModel>> GetAllVehiclesWhichAreForSale()
        {
            var vehicles = repository.AllReadOnly<Vehicle>()
                .Where(v => v.OwnerId == null)
                .Select(v => new VehicleListViewModel()
                {
                    Id = v.Id,
                    Name = $"{v.Brand} {v.Model} {v.YearProduced}",
                    VehicleType = v.VehicleType,
                    VehicleRating = v.VehicleRating,
                    Price = v.Price,
                    SalesmanName = $"{v.Salesman.User.FirstName} {v.Salesman.User.LastName}"
                })
                .ToList();
            return vehicles;
        }

        public async Task<VehicleViewModel> GetVehicleById(int id)
        {
            var vehicle = await repository.AllReadOnly<Vehicle>()
                .Where(v => v.Id == id)
                .Select(v => new VehicleViewModel()
                {
                    Id = v.Id,
                    Brand = v.Brand,
                    Model = v.Model,
                    Description = v.Description,
                    YearProduced = v.YearProduced,
                    TopSpeed = v.TopSpeed,
                    KilometersDriven = v.KilometersDriven,
                    Price = v.Price,
                    VehicleType = v.VehicleType,
                    VehicleRating = v.VehicleRating,
                    SalesmanId = v.SalesmanId,
                    SalesmanName = v.Salesman != null ?  $"{v.Salesman.User.FirstName} {v.Salesman.User.LastName}" : null,
                    OwnerId = v.OwnerId,
                    OwnerName = v.Owner != null ? $"{v.Owner.User.FirstName} {v.Owner.User.LastName}" : null
                }).FirstOrDefaultAsync();
            return vehicle;
        }
    }
}
