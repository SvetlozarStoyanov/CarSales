using CarSales.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Core.Contracts
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(string userId);
    }
}
