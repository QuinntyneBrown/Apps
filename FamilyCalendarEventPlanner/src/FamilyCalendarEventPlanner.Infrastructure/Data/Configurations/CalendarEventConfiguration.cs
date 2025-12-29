using FamilyCalendarEventPlanner.Core.Model.EventAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyCalendarEventPlanner.Infrastructure.Data.Configurations;

public class CalendarEventConfiguration : IEntityTypeConfiguration<CalendarEvent>
{
    public void Configure(EntityTypeBuilder<CalendarEvent> builder)
    {
        builder.HasKey(e => e.EventId);

        builder.Property(e => e.EventId)
            .ValueGeneratedNever();

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Description)
            .HasMaxLength(2000);

        builder.Property(e => e.Location)
            .HasMaxLength(500);

        builder.OwnsOne(e => e.RecurrencePattern, recurrence =>
        {
            recurrence.Property(r => r.Frequency);
            recurrence.Property(r => r.Interval);
            recurrence.Property(r => r.EndDate);
            recurrence.Property(r => r.DaysOfWeek)
                .HasConversion(
                    v => string.Join(',', v.Select(d => (int)d)),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => (DayOfWeek)int.Parse(s))
                        .ToList());
        });

        builder.HasIndex(e => e.FamilyId);
        builder.HasIndex(e => e.CreatorId);
        builder.HasIndex(e => e.StartTime);
        builder.HasIndex(e => e.Status);
    }
}
