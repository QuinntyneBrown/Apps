using FamilyCalendarEventPlanner.Core.Models.ReminderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyCalendarEventPlanner.Infrastructure.Data.Configurations;

public class EventReminderConfiguration : IEntityTypeConfiguration<EventReminder>
{
    public void Configure(EntityTypeBuilder<EventReminder> builder)
    {
        builder.HasKey(r => r.ReminderId);

        builder.Property(r => r.ReminderId)
            .ValueGeneratedNever();

        builder.HasIndex(r => r.EventId);
        builder.HasIndex(r => r.RecipientId);
        builder.HasIndex(r => r.ReminderTime);
        builder.HasIndex(r => r.Sent);
    }
}
