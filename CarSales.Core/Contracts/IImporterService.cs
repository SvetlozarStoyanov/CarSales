using CarSales.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Core.Contracts
{
    public interface IImporterService
    {
        /// <summary>
        /// Creates a new <see cref="Importer"/> with given <paramref name="userId"/>,
        /// If <see cref="Salesman"/> already exists ot sets <see cref="Importer.IsActive"/> to true
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task CreateOrRenewImporterAsync(string userId);


        /// <summary>
        /// Sets <see cref="Importer.IsActive"/> to false
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task RetireImporterAsync(string userId);
    }
}
