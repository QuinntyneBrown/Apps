using Events.Core;
using Events.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Events.Infrastructure.Data;

public class EventsDbContext : DbContext, IEventsDbContext
{
    public EventsDbContext(DbContextOptions<EventsDbContext> options) : base(options)
    {
    }

    public DbSet<CalendarEvent> CalendarEvents { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CalendarEvent>(entity =>
        {
            entity.ToTable("CalendarEvents");
            entity.HasKey(e => e.CalendarEventId);
            entity.Property(e => e.Title).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.Location).HasMaxLength(500);
            entity.Property(e => e.RecurrencePattern).HasMaxLength(500);
            entity.HasIndex(e => new { e.TenantId, e.StartTime });
            entity.HasIndex(e => new { e.TenantId, e.CreatedByUserId });
        });
    }
}
