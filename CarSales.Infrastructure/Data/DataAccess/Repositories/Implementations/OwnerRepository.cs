using CarSales.Infrastructure.Data.DataAccess.Repositories.Contracts;
using CarSales.Infrastructure.Data.DataAccess.Repository;
using CarSales.Infrastructure.Data.Entities;

namespace CarSales.Infrastructure.Data.DataAccess.Repositories.Implementations
{
    public class OwnerRepository : BaseRepository<Owner>, IOwnerRepository
    {
        public OwnerRepository(CarSalesDbContext context) : base(context)
        {
        }
    }
}
