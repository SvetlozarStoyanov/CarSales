using CarSales.Core.Models.Vehicles;

namespace CarSales.Core.Contracts
{
    public interface IVehicleService
    {
        Task<ICollection<VehicleListViewModel>> GetAllVehiclesWhichAreForSale();

        Task<VehicleViewModel> GetVehicleById(int id);

    }
}
