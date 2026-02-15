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
            entity.HasIndex(e => new { e.TenantId, e.UserId });
            entity.Property(e => e.BookTitle).HasMaxLength(500);
            entity.Property(e => e.Author).HasMaxLength(300);
            entity.Property(e => e.Isbn).HasMaxLength(20);
        });
    }
}
