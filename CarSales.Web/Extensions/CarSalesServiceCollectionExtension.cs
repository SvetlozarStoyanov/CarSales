﻿using AirsoftMatchMaker.Infrastructure.Data.Common.Repository;
using CarSales.Core.Contracts;
using CarSales.Core.Services;

namespace CarSales.Web.Extensions
{
    public static class CarSalesServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IOwnerService, OwnerService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
