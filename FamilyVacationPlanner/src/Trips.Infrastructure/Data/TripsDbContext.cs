using Trips.Core;
using Trips.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Trips.Infrastructure.Data;

public class TripsDbContext : DbContext, ITripsDbContext
{
    public TripsDbContext(DbContextOptions<TripsDbContext> options) : base(options)
    {
    }

    public DbSet<Trip> Trips { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.ToTable("Trips");
            entity.HasKey(e => e.TripId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Destination).HasMaxLength(500);
        });
    }
}
