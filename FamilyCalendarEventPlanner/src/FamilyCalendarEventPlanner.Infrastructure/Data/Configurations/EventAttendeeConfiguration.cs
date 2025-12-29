using FamilyCalendarEventPlanner.Core.Model.AttendeeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyCalendarEventPlanner.Infrastructure.Data.Configurations;

public class EventAttendeeConfiguration : IEntityTypeConfiguration<EventAttendee>
{
    public void Configure(EntityTypeBuilder<EventAttendee> builder)
    {
        builder.HasKey(a => a.AttendeeId);

        builder.Property(a => a.AttendeeId)
            .ValueGeneratedNever();

        builder.Property(a => a.Notes)
            .HasMaxLength(500);

        builder.HasIndex(a => a.EventId);
        builder.HasIndex(a => a.FamilyMemberId);
        builder.HasIndex(a => new { a.EventId, a.FamilyMemberId }).IsUnique();
    }
}
