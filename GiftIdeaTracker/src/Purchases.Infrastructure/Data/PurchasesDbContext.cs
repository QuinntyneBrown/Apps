using Purchases.Core;
using Purchases.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Purchases.Infrastructure.Data;

public class PurchasesDbContext : DbContext, IPurchasesDbContext
{
    public PurchasesDbContext(DbContextOptions<PurchasesDbContext> options) : base(options)
    {
    }

    public DbSet<Purchase> Purchases { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.ToTable("Purchases");
            entity.HasKey(e => e.PurchaseId);
            entity.Property(e => e.Amount).HasPrecision(18, 2);
            entity.HasIndex(e => new { e.TenantId, e.IdeaId });
        });
    }
}
