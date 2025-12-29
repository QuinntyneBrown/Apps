using FamilyCalendarEventPlanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyCalendarEventPlanner.Infrastructure;

public class EventAttendeeConfiguration : IEntityTypeConfiguration<EventAttendee>
{
    public void Configure(EntityTypeBuilder<EventAttendee> builder)
    {
        builder.HasKey(e => e.AttendeeId);

        builder.Property(e => e.RSVPStatus)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(e => e.Notes)
            .HasMaxLength(1000);

        builder.HasIndex(e => e.EventId);
        builder.HasIndex(e => e.FamilyMemberId);
        builder.HasIndex(e => new { e.EventId, e.FamilyMemberId }).IsUnique();
    }
}
