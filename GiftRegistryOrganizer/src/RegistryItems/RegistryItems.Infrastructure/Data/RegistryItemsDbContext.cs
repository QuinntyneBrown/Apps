using RegistryItems.Core;
using RegistryItems.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace RegistryItems.Infrastructure.Data;

public class RegistryItemsDbContext : DbContext, IRegistryItemsDbContext
{
    public RegistryItemsDbContext(DbContextOptions<RegistryItemsDbContext> options) : base(options)
    {
    }

    public DbSet<RegistryItem> RegistryItems { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<RegistryItem>(entity =>
        {
            entity.ToTable("RegistryItems");
            entity.HasKey(e => e.RegistryItemId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Price).HasPrecision(18, 2);
            entity.HasIndex(e => new { e.TenantId, e.RegistryId });
        });
    }
}
