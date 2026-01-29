using Wishlists.Core;
using Wishlists.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Wishlists.Infrastructure.Data;

public class WishlistsDbContext : DbContext, IWishlistsDbContext
{
    public WishlistsDbContext(DbContextOptions<WishlistsDbContext> options) : base(options)
    {
    }

    public DbSet<WishlistItem> WishlistItems { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<WishlistItem>(entity =>
        {
            entity.ToTable("WishlistItems");
            entity.HasKey(e => e.WishlistItemId);
            entity.Property(e => e.GameTitle).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Platform).HasMaxLength(100).IsRequired();
            entity.Property(e => e.TargetPrice).HasPrecision(18, 2);
        });
    }
}
