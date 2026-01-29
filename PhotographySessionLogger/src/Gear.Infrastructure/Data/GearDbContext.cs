using Gear.Core;
using Gear.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Gear.Infrastructure.Data;

public class GearDbContext : DbContext, IGearDbContext
{
    public GearDbContext(DbContextOptions<GearDbContext> options) : base(options)
    {
    }

    public DbSet<GearItem> GearItems { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<GearItem>(entity =>
        {
            entity.ToTable("GearItems");
            entity.HasKey(e => e.GearItemId);
            entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Type).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Brand).HasMaxLength(100);
            entity.Property(e => e.Model).HasMaxLength(100);
            entity.Property(e => e.SerialNumber).HasMaxLength(100);
        });
    }
}
