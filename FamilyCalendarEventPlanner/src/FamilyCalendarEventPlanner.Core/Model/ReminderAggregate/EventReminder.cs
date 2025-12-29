using FamilyCalendarEventPlanner.Core.Model.ReminderAggregate.Enums;

namespace FamilyCalendarEventPlanner.Core.Model.ReminderAggregate;

public class EventReminder
{
    public Guid ReminderId { get; private set; }
    public Guid EventId { get; private set; }
    public Guid RecipientId { get; private set; }
    public DateTime ReminderTime { get; private set; }
    public NotificationChannel DeliveryChannel { get; private set; }
    public bool Sent { get; private set; }

    private EventReminder()
    {
    }

    public EventReminder(Guid eventId, Guid recipientId, DateTime reminderTime, NotificationChannel deliveryChannel)
    {
        if (reminderTime < DateTime.UtcNow)
        {
            throw new ArgumentException("Reminder time cannot be in the past.", nameof(reminderTime));
        }

        ReminderId = Guid.NewGuid();
        EventId = eventId;
        RecipientId = recipientId;
        ReminderTime = reminderTime;
        DeliveryChannel = deliveryChannel;
        Sent = false;
    }

    public void Send()
    {
        if (Sent)
        {
            throw new InvalidOperationException("Reminder has already been sent.");
        }

        Sent = true;
    }

    public void Reschedule(DateTime newReminderTime)
    {
        if (Sent)
        {
            throw new InvalidOperationException("Cannot reschedule a reminder that has already been sent.");
        }

        if (newReminderTime < DateTime.UtcNow)
        {
            throw new ArgumentException("Reminder time cannot be in the past.", nameof(newReminderTime));
        }

        ReminderTime = newReminderTime;
    }

    public void ChangeChannel(NotificationChannel newChannel)
    {
        if (Sent)
        {
            throw new InvalidOperationException("Cannot change channel of a reminder that has already been sent.");
        }

        DeliveryChannel = newChannel;
    }
}
