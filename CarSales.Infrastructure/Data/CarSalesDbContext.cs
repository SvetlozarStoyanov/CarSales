using CarSales.Infrastructure.Data.Configuration;
using CarSales.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarSales.Infrastructure.Data
{
    public class CarSalesDbContext : IdentityDbContext<User>
    {
        public CarSalesDbContext(DbContextOptions<CarSalesDbContext> options)
            : base(options)
        {
            this.Database.Migrate();
        }
        public DbSet<Salesman> Salesmen { get; set; } = null!;
        public DbSet<Owner> Owners { get; set; } = null!;
        public DbSet<Importer> Importers { get; set; } = null!;
        public DbSet<Reviewer> Reviewers { get; set; } = null!;
        public DbSet<Vehicle> Vehicles { get; set; } = null!;
        public DbSet<Sale> Sales { get; set; } = null!;
        public DbSet<RoleRequest> RoleRequests { get; set; } = null!;
        public DbSet<Offer> Offers { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Vehicle>()
                .HasOne(b => b.Owner)
                .WithMany(v => v.Vehicles)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Vehicle>()
                .HasOne(b => b.Salesman)
                .WithMany(v => v.Vehicles)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Vehicle>()
                .HasOne(b => b.Importer)
                .WithMany(v => v.Vehicles)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Owner>()
                .HasMany(b => b.Vehicles)
                .WithOne(v => v.Owner)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Salesman>()
                .HasMany(b => b.Vehicles)
                .WithOne(v => v.Salesman)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Importer>()
                .HasMany(b => b.Vehicles)
                .WithOne(v => v.Importer)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Sale>()
                .HasOne(s => s.Salesman)
                .WithMany(sm => sm.Sales)
                .HasForeignKey(s => s.SalesmanId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Sale>()
                .HasOne(s => s.Owner)
                .WithMany(sm => sm.Sales)
                .HasForeignKey(s => s.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Sale>()
                .HasOne(s => s.Vehicle)
                .WithMany(v => v.Sales)
                .HasForeignKey(s => s.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Offer>()
                .HasOne(o => o.Offeror)
                .WithMany(o => o.Offers)
                .HasForeignKey(o => o.OfferorId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Offer>()
                .HasOne(o => o.Salesman)
                .WithMany(o => o.Offers)
                .HasForeignKey(o => o.SalesmanId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Offer>()
                .HasOne(o => o.Vehicle)
                .WithMany(o => o.Offers)
                .HasForeignKey(o => o.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
            builder.ApplyConfiguration(new SalesmanConfiguration());
            builder.ApplyConfiguration(new OwnerConfiguration());
            builder.ApplyConfiguration(new ImporterConfiguration());
            builder.ApplyConfiguration(new ReviewerConfiguration());
            builder.ApplyConfiguration(new ReviewConfiguration());
            builder.ApplyConfiguration(new VehicleConfiguration());
            builder.ApplyConfiguration(new SaleConfiguration());

            base.OnModelCreating(builder);
        }
    }
}