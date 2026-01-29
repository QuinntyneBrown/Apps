using Appliances.Core;
using Appliances.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Appliances.Infrastructure.Data;

public class AppliancesDbContext : DbContext, IAppliancesDbContext
{
    public AppliancesDbContext(DbContextOptions<AppliancesDbContext> options) : base(options)
    {
    }

    public DbSet<Appliance> Appliances { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Appliance>(entity =>
        {
            entity.ToTable("Appliances");
            entity.HasKey(e => e.ApplianceId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Brand).HasMaxLength(100);
            entity.Property(e => e.ModelNumber).HasMaxLength(100);
            entity.Property(e => e.SerialNumber).HasMaxLength(100);
            entity.Property(e => e.PurchasePrice).HasPrecision(18, 2);
            entity.HasIndex(e => new { e.TenantId, e.UserId });
        });
    }
}
