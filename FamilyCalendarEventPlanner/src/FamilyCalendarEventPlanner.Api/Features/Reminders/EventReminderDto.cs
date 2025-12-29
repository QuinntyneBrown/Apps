using FamilyCalendarEventPlanner.Core.Model.ReminderAggregate;
using FamilyCalendarEventPlanner.Core.Model.ReminderAggregate.Enums;

namespace FamilyCalendarEventPlanner.Api.Features.Reminders;

public record EventReminderDto
{
    public Guid ReminderId { get; init; }
    public Guid EventId { get; init; }
    public Guid RecipientId { get; init; }
    public DateTime ReminderTime { get; init; }
    public NotificationChannel DeliveryChannel { get; init; }
    public bool Sent { get; init; }
}

public static class EventReminderExtensions
{
    public static EventReminderDto ToDto(this EventReminder reminder)
    {
        return new EventReminderDto
        {
            ReminderId = reminder.ReminderId,
            EventId = reminder.EventId,
            RecipientId = reminder.RecipientId,
            ReminderTime = reminder.ReminderTime,
            DeliveryChannel = reminder.DeliveryChannel,
            Sent = reminder.Sent,
        };
    }
}
