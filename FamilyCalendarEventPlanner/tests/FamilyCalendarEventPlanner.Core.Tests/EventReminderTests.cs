using FamilyCalendarEventPlanner.Core.Model.ReminderAggregate;
using FamilyCalendarEventPlanner.Core.Model.ReminderAggregate.Enums;

namespace FamilyCalendarEventPlanner.Core.Tests;

public class EventReminderTests
{
    private readonly Guid _eventId = Guid.NewGuid();
    private readonly Guid _recipientId = Guid.NewGuid();

    [Test]
    public void Constructor_ValidParameters_CreatesReminder()
    {
        var reminderTime = DateTime.UtcNow.AddHours(1);

        var reminder = new EventReminder(TestHelpers.DefaultTenantId, _eventId, _recipientId, reminderTime, NotificationChannel.Email);

        Assert.Multiple(() =>
        {
            Assert.That(reminder.ReminderId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(reminder.EventId, Is.EqualTo(_eventId));
            Assert.That(reminder.RecipientId, Is.EqualTo(_recipientId));
            Assert.That(reminder.ReminderTime, Is.EqualTo(reminderTime));
            Assert.That(reminder.DeliveryChannel, Is.EqualTo(NotificationChannel.Email));
            Assert.That(reminder.Sent, Is.False);
        });
    }

    [Test]
    public void Constructor_PastReminderTime_ThrowsArgumentException()
    {
        var pastTime = DateTime.UtcNow.AddHours(-1);

        Assert.Throws<ArgumentException>(() =>
            new EventReminder(TestHelpers.DefaultTenantId, _eventId, _recipientId, pastTime, NotificationChannel.Email));
    }

    [Test]
    public void Constructor_EmailChannel_SetsCorrectChannel()
    {
        var reminder = CreateDefaultReminder(NotificationChannel.Email);

        Assert.That(reminder.DeliveryChannel, Is.EqualTo(NotificationChannel.Email));
    }

    [Test]
    public void Constructor_PushChannel_SetsCorrectChannel()
    {
        var reminder = CreateDefaultReminder(NotificationChannel.Push);

        Assert.That(reminder.DeliveryChannel, Is.EqualTo(NotificationChannel.Push));
    }

    [Test]
    public void Constructor_SMSChannel_SetsCorrectChannel()
    {
        var reminder = CreateDefaultReminder(NotificationChannel.SMS);

        Assert.That(reminder.DeliveryChannel, Is.EqualTo(NotificationChannel.SMS));
    }

    [Test]
    public void Send_UnsentReminder_SetsSentToTrue()
    {
        var reminder = CreateDefaultReminder();

        reminder.Send();

        Assert.That(reminder.Sent, Is.True);
    }

    [Test]
    public void Send_AlreadySentReminder_ThrowsInvalidOperationException()
    {
        var reminder = CreateDefaultReminder();
        reminder.Send();

        Assert.Throws<InvalidOperationException>(() => reminder.Send());
    }

    [Test]
    public void Reschedule_ValidTime_UpdatesReminderTime()
    {
        var reminder = CreateDefaultReminder();
        var newTime = DateTime.UtcNow.AddHours(2);

        reminder.Reschedule(newTime);

        Assert.That(reminder.ReminderTime, Is.EqualTo(newTime));
    }

    [Test]
    public void Reschedule_PastTime_ThrowsArgumentException()
    {
        var reminder = CreateDefaultReminder();
        var pastTime = DateTime.UtcNow.AddHours(-1);

        Assert.Throws<ArgumentException>(() => reminder.Reschedule(pastTime));
    }

    [Test]
    public void Reschedule_AlreadySentReminder_ThrowsInvalidOperationException()
    {
        var reminder = CreateDefaultReminder();
        reminder.Send();
        var newTime = DateTime.UtcNow.AddHours(2);

        Assert.Throws<InvalidOperationException>(() => reminder.Reschedule(newTime));
    }

    [Test]
    public void ChangeChannel_ValidChannel_UpdatesChannel()
    {
        var reminder = CreateDefaultReminder(NotificationChannel.Email);

        reminder.ChangeChannel(NotificationChannel.SMS);

        Assert.That(reminder.DeliveryChannel, Is.EqualTo(NotificationChannel.SMS));
    }

    [Test]
    public void ChangeChannel_AlreadySentReminder_ThrowsInvalidOperationException()
    {
        var reminder = CreateDefaultReminder();
        reminder.Send();

        Assert.Throws<InvalidOperationException>(() => reminder.ChangeChannel(NotificationChannel.Push));
    }

    [Test]
    public void Reschedule_MultipleReschedules_UpdatesToLatestTime()
    {
        var reminder = CreateDefaultReminder();
        var firstTime = DateTime.UtcNow.AddHours(2);
        var secondTime = DateTime.UtcNow.AddHours(3);

        reminder.Reschedule(firstTime);
        reminder.Reschedule(secondTime);

        Assert.That(reminder.ReminderTime, Is.EqualTo(secondTime));
    }

    [Test]
    public void ChangeChannel_MultipleChanges_UpdatesToLatestChannel()
    {
        var reminder = CreateDefaultReminder(NotificationChannel.Email);

        reminder.ChangeChannel(NotificationChannel.Push);
        reminder.ChangeChannel(NotificationChannel.SMS);

        Assert.That(reminder.DeliveryChannel, Is.EqualTo(NotificationChannel.SMS));
    }

    private EventReminder CreateDefaultReminder(NotificationChannel channel = NotificationChannel.Email)
    {
        return new EventReminder(
            TestHelpers.DefaultTenantId,
            _eventId,
            _recipientId,
            DateTime.UtcNow.AddHours(1),
            channel);
    }
}
