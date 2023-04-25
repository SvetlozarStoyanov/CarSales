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
        }
        public DbSet<Salesman> Salesmen { get; set; } = null!;
        public DbSet<Owner> Owners { get; set; } = null!;
        public DbSet<Vehicle> Vehicles { get; set; } = null!;
        public DbSet<Sale> Sales { get; set; } = null!;
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

            builder.Entity<Owner>()
                .HasMany(b => b.Vehicles)
                .WithOne(v => v.Owner)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Salesman>()
                .HasMany(b => b.Vehicles)
                .WithOne(v => v.Salesman)
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
                .WithMany(sm => sm.Sales)
                .HasForeignKey(s => s.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
            builder.ApplyConfiguration(new SalesmanConfiguration());
            builder.ApplyConfiguration(new OwnerConfiguration());
            builder.ApplyConfiguration(new VehicleConfiguration());
            builder.ApplyConfiguration(new SaleConfiguration());

            base.OnModelCreating(builder);
        }
    }
}