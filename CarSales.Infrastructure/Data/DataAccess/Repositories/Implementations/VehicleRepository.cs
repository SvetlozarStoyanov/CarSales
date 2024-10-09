using CarSales.Infrastructure.Data.DataAccess.Repositories.Contracts;
using CarSales.Infrastructure.Data.DataAccess.Repository;
using CarSales.Infrastructure.Data.Entities;

namespace CarSales.Infrastructure.Data.DataAccess.Repositories.Implementations
{
    public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(CarSalesDbContext context) : base(context)
        {
        }
    }
}
