using Itineraries.Core;
using Itineraries.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Itineraries.Infrastructure.Data;

public class ItinerariesDbContext : DbContext, IItinerariesDbContext
{
    public ItinerariesDbContext(DbContextOptions<ItinerariesDbContext> options) : base(options)
    {
    }

    public DbSet<Itinerary> Itineraries { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Itinerary>(entity =>
        {
            entity.ToTable("Itineraries");
            entity.HasKey(e => e.ItineraryId);
            entity.Property(e => e.Activity).HasMaxLength(500);
            entity.Property(e => e.Location).HasMaxLength(500);
        });
    }
}
