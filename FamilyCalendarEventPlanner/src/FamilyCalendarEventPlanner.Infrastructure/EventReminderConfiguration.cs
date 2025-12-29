using FamilyCalendarEventPlanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyCalendarEventPlanner.Infrastructure;

public class EventReminderConfiguration : IEntityTypeConfiguration<EventReminder>
{
    public void Configure(EntityTypeBuilder<EventReminder> builder)
    {
        builder.HasKey(e => e.ReminderId);

        builder.Property(e => e.DeliveryChannel)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.HasIndex(e => e.EventId);
        builder.HasIndex(e => e.RecipientId);
        builder.HasIndex(e => e.ReminderTime);
        builder.HasIndex(e => e.Sent);
    }
}
