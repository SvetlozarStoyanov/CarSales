using CarSales.Infrastructure.Data.DataAccess.Repository;
using CarSales.Core.Contracts;
using CarSales.Core.Services;
using CarSales.Infrastructure.Data.DataAccess.UnitOfWork;

namespace CarSales.Web.Extensions
{
    public static class CarSalesServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IHtmlSanitizingService, HtmlSanitizingService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IOwnerService, OwnerService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IRoleRequestService, RoleRequestService>();
            services.AddScoped<ISalesmanService, SalesmanService>();
            services.AddScoped<IImporterService, ImporterService>();
            services.AddScoped<IReviewerService, ReviewerService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IOfferService, OfferService>();
            services.AddScoped<INotificationService, NotificationService>();

            return services;
        }
    }
}
