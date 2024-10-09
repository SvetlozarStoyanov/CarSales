using CarSales.Infrastructure.Data.DataAccess.Repositories.Contracts;
using CarSales.Infrastructure.Data.DataAccess.Repositories.Implementations;

namespace CarSales.Infrastructure.Data.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CarSalesDbContext context;

        #region Repository fields

        private IImporterRepository importerRepository;
        private INotificationRepository notificationRepository;
        private IOfferRepository offerRepository;
        private IOwnerRepository ownerRepository;
        private IReviewerRepository reviewerRepository;
        private IReviewRepository reviewRepository;
        private IRoleRequestRepository roleRequestRepository;
        private ISaleRepository saleRepository;
        private ISalesmanRepository salesmanRepository;
        private IUserRepository userRepository;
        private IVehicleRepository vehicleRepository;

        #endregion

        public UnitOfWork(CarSalesDbContext dbContext)
        {
            this.context = dbContext;
        }

        #region Repositories

        public IImporterRepository ImporterRepository => importerRepository ??= new ImporterRepository(context);
        public INotificationRepository NotificationRepository => notificationRepository ??= new NotificationRepository(context);
        public IOfferRepository OfferRepository => offerRepository ??= new OfferRepository(context);
        public IOwnerRepository OwnerRepository => ownerRepository ??= new OwnerRepository(context);
        public IReviewerRepository ReviewerRepository => reviewerRepository ??= new ReviewerRepository(context);
        public IReviewRepository ReviewRepository => reviewRepository ??= new ReviewRepository(context);
        public IRoleRequestRepository RoleRequestRepository => roleRequestRepository ??= new RoleRequestRepository(context);
        public ISaleRepository SaleRepository => saleRepository ??= new SaleRepository(context);
        public ISalesmanRepository SalesmanRepository => salesmanRepository ??= new SalesmanRepository(context);
        public IUserRepository UserRepository => userRepository ??= new UserRepository(context);
        public IVehicleRepository VehicleRepository => vehicleRepository ??= new VehicleRepository(context);

        #endregion

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        // IDisposable implementation
        public void Dispose()
        {
            context?.Dispose();
        }
    }
}
