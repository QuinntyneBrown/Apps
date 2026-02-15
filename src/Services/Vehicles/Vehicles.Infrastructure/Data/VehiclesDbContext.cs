using Microsoft.EntityFrameworkCore;
using Vehicles.Core;
using Vehicles.Core.Models;

namespace Vehicles.Infrastructure.Data;

public class VehiclesDbContext : DbContext, IVehiclesDbContext
{
    public VehiclesDbContext(DbContextOptions<VehiclesDbContext> options) : base(options) { }

    public DbSet<Vehicle> Vehicles => Set<Vehicle>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.VehicleId);
            entity.Property(e => e.Make).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Model).HasMaxLength(100).IsRequired();
            entity.Property(e => e.VIN).HasMaxLength(17);
            entity.Property(e => e.LicensePlate).HasMaxLength(20);
            entity.Property(e => e.Color).HasMaxLength(50);
            entity.Property(e => e.CurrentMileage).HasPrecision(12, 2);
            entity.Property(e => e.OwnerName).HasMaxLength(200);
            entity.Property(e => e.Notes).HasMaxLength(1000);
            entity.HasIndex(e => e.TenantId);
        });
    }
}
