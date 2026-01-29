using Vehicles.Core;
using Vehicles.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Vehicles.Infrastructure.Data;

public class VehiclesDbContext : DbContext, IVehiclesDbContext
{
    public VehiclesDbContext(DbContextOptions<VehiclesDbContext> options) : base(options)
    {
    }

    public DbSet<Vehicle> Vehicles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.ToTable("Vehicles");
            entity.HasKey(e => e.VehicleId);
            entity.Property(e => e.Make).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Model).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Vin).HasMaxLength(17);
        });
    }
}
