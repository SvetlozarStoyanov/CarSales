using CarSales.Infrastructure.Data.Entities;

namespace CarSales.Core.Contracts
{
    public interface IOwnerService
    {
        /// <summary>
        /// Creates an <see cref="Owner"/> and adds it to the database
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task CreateOwnerAsync(string userId);
        
    }
}
