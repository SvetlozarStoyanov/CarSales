using CarSales.Infrastructure.Data.DataAccess.Repositories.Contracts;
using CarSales.Infrastructure.Data.DataAccess.Repository;
using CarSales.Infrastructure.Data.Entities;

namespace CarSales.Infrastructure.Data.DataAccess.Repositories.Implementations
{
    public class SaleRepository : BaseRepository<Sale>, ISaleRepository
    {
        public SaleRepository(CarSalesDbContext context) : base(context)
        {
        }
    }
}
