using Warranties.Core;
using Warranties.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Warranties.Infrastructure.Data;

public class WarrantiesDbContext : DbContext, IWarrantiesDbContext
{
    public WarrantiesDbContext(DbContextOptions<WarrantiesDbContext> options) : base(options)
    {
    }

    public DbSet<Warranty> Warranties { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Warranty>(entity =>
        {
            entity.ToTable("Warranties");
            entity.HasKey(e => e.WarrantyId);
            entity.Property(e => e.Provider).HasMaxLength(200);
            entity.Property(e => e.CoverageDetails).HasMaxLength(2000);
            entity.Property(e => e.DocumentUrl).HasMaxLength(500);
            entity.HasIndex(e => new { e.TenantId, e.ApplianceId });
        });
    }
}
