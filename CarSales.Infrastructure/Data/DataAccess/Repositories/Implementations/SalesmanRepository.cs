using CarSales.Infrastructure.Data.DataAccess.Repositories.Contracts;
using CarSales.Infrastructure.Data.DataAccess.Repository;
using CarSales.Infrastructure.Data.Entities;

namespace CarSales.Infrastructure.Data.DataAccess.Repositories.Implementations
{
    public class SalesmanRepository : BaseRepository<Salesman>, ISalesmanRepository
    {
        public SalesmanRepository(CarSalesDbContext context) : base(context)
        {
        }
    }
}
