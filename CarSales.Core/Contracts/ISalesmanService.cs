using CarSales.Infrastructure.Data.Entities;
namespace CarSales.Core.Contracts
{
    public interface ISalesmanService
    {
        /// <summary>
        /// Creates a new <see cref="Salesman"/> with given <paramref name="userId"/>,
        /// If <see cref="Salesman"/> already exists ot sets <see cref="Salesman.IsActive"/> to true
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task CreateOrRenewSalesmanAsync(string userId);

        /// <summary>
        /// Sets <see cref="Salesman.IsActive"/> to false
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task RetireSalesmanAsync(string userId);

        /// <summary>
        /// Returns <see cref="User.Id"/> of <see cref="Salesman"/>
        /// with <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="string"/></returns>
        Task<string> GetSalesmanUserIdAsync(int id);
    }
}
