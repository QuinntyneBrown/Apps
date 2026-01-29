using GroceryLists.Core;
using GroceryLists.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace GroceryLists.Infrastructure.Data;

public class GroceryListsDbContext : DbContext, IGroceryListsDbContext
{
    public GroceryListsDbContext(DbContextOptions<GroceryListsDbContext> options) : base(options)
    {
    }

    public DbSet<GroceryList> GroceryLists { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<GroceryList>(entity =>
        {
            entity.ToTable("GroceryLists");
            entity.HasKey(e => e.GroceryListId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.EstimatedCost).HasPrecision(18, 2);
            entity.HasIndex(e => new { e.TenantId, e.UserId });
        });
    }
}
