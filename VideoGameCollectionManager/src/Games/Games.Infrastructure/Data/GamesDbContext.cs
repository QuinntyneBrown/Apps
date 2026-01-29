using Games.Core;
using Games.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Games.Infrastructure.Data;

public class GamesDbContext : DbContext, IGamesDbContext
{
    public GamesDbContext(DbContextOptions<GamesDbContext> options) : base(options)
    {
    }

    public DbSet<Game> Games { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Game>(entity =>
        {
            entity.ToTable("Games");
            entity.HasKey(e => e.GameId);
            entity.Property(e => e.Title).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Platform).HasMaxLength(100).IsRequired();
            entity.Property(e => e.PurchasePrice).HasPrecision(18, 2);
        });
    }
}
