using CarSales.Infrastructure.Data.DataAccess.Repositories.Contracts;

namespace CarSales.Infrastructure.Data.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        #region Repositories

        public IImporterRepository ImporterRepository { get; }
        public INotificationRepository NotificationRepository { get; }
        public IOfferRepository OfferRepository { get; }
        public IOwnerRepository OwnerRepository { get; }
        public IReviewerRepository ReviewerRepository { get; }
        public IReviewRepository ReviewRepository { get; }
        public IRoleRequestRepository RoleRequestRepository { get; }
        public ISaleRepository SaleRepository { get; }
        public ISalesmanRepository SalesmanRepository { get; }
        public IUserRepository UserRepository { get; }
        public IVehicleRepository VehicleRepository { get; }

        #endregion

        public Task<int> SaveChangesAsync();
    }
}
