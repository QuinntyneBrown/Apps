using FamilyCalendarEventPlanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyCalendarEventPlanner.Infrastructure;

public class CalendarEventConfiguration : IEntityTypeConfiguration<CalendarEvent>
{
    public void Configure(EntityTypeBuilder<CalendarEvent> builder)
    {
        builder.HasKey(e => e.EventId);

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Description)
            .HasMaxLength(2000);

        builder.Property(e => e.Location)
            .HasMaxLength(500);

        builder.Property(e => e.EventType)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(e => e.Status)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.OwnsOne(e => e.RecurrencePattern, recurrence =>
        {
            recurrence.Property(r => r.Frequency)
                .HasConversion<string>()
                .HasMaxLength(50);

            recurrence.Property(r => r.Interval);

            recurrence.Property(r => r.EndDate);

            recurrence.Property(r => r.DaysOfWeek)
                .HasConversion(
                    v => string.Join(",", v.Select(d => (int)d)),
                    v => string.IsNullOrEmpty(v)
                        ? new List<DayOfWeek>()
                        : v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                            .Select(d => (DayOfWeek)int.Parse(d))
                            .ToList())
                .HasMaxLength(50);
        });

        builder.HasIndex(e => e.FamilyId);
        builder.HasIndex(e => e.CreatorId);
        builder.HasIndex(e => e.StartTime);
        builder.HasIndex(e => e.EndTime);
    }
}
