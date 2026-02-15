using Scheduling.Core;
using Scheduling.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Scheduling.Infrastructure.Data;

public class SchedulingDbContext : DbContext, ISchedulingDbContext
{
    public SchedulingDbContext(DbContextOptions<SchedulingDbContext> options) : base(options)
    {
    }

    public DbSet<Availability> Availabilities { get; set; } = null!;
    public DbSet<Reminder> Reminders { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Availability>(entity =>
        {
            entity.ToTable("Availabilities");
            entity.HasKey(e => e.AvailabilityId);
            entity.Property(e => e.DayOfWeek).HasConversion<string>().HasMaxLength(20);
            entity.HasIndex(e => new { e.TenantId, e.UserId, e.DayOfWeek });
        });

        modelBuilder.Entity<Reminder>(entity =>
        {
            entity.ToTable("Reminders");
            entity.HasKey(e => e.ReminderId);
            entity.Property(e => e.Message).HasMaxLength(500).IsRequired();
            entity.HasIndex(e => new { e.TenantId, e.CalendarEventId });
            entity.HasIndex(e => new { e.TenantId, e.ReminderTime, e.IsSent });
        });
    }
}
