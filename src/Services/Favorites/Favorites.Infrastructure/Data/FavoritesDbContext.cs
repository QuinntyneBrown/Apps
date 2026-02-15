using Favorites.Core;
using Favorites.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Favorites.Infrastructure.Data;

public class FavoritesDbContext : DbContext, IFavoritesDbContext
{
    public FavoritesDbContext(DbContextOptions<FavoritesDbContext> options) : base(options)
    {
    }

    public DbSet<Favorite> Favorites { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.ToTable("Favorites");
            entity.HasKey(e => e.FavoriteId);
            entity.HasIndex(e => new { e.UserId, e.PromptId }).IsUnique();
        });
    }
}
