using Handicaps.Core;
using Handicaps.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Handicaps.Infrastructure.Data;

public class HandicapsDbContext : DbContext, IHandicapsDbContext
{
    public HandicapsDbContext(DbContextOptions<HandicapsDbContext> options) : base(options) { }
    public DbSet<Handicap> Handicaps { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Handicap>(e => { e.ToTable("Handicaps"); e.HasKey(h => h.HandicapId); e.Property(h => h.HandicapIndex).HasPrecision(4, 1); });
    }
}
