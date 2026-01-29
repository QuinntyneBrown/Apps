using BucketListItems.Core;
using BucketListItems.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BucketListItems.Infrastructure.Data;

public class BucketListItemsDbContext : DbContext, IBucketListItemsDbContext
{
    public BucketListItemsDbContext(DbContextOptions<BucketListItemsDbContext> options) : base(options)
    {
    }

    public DbSet<BucketListItem> BucketListItems { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BucketListItem>(entity =>
        {
            entity.ToTable("BucketListItems");
            entity.HasKey(e => e.BucketListItemId);
            entity.HasIndex(e => new { e.TenantId, e.UserId });
            entity.Property(e => e.Title).HasMaxLength(500);
            entity.Property(e => e.Category).HasMaxLength(100);
        });
    }
}
