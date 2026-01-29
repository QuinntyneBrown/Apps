using Events.Core;
using Events.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Events.Infrastructure.Data;

public class EventsDbContext : DbContext, IEventsDbContext
{
    public EventsDbContext(DbContextOptions<EventsDbContext> options) : base(options) { }

    public DbSet<Event> Events => Set<Event>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Event>(entity =>
        {
            entity.ToTable("Events", "events");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Location).HasMaxLength(500);
            entity.Property(e => e.Website).HasMaxLength(500);
            entity.Property(e => e.RegistrationFee).HasPrecision(18, 2);
            entity.HasIndex(e => e.TenantId);
            entity.HasIndex(e => e.StartDate);
        });
    }
}
