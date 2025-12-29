namespace FamilyCalendarEventPlanner.Infrastructure.Tests;

public class EventReminderPersistenceTests
{
    private FamilyCalendarEventPlannerContext _context = null!;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<FamilyCalendarEventPlannerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new FamilyCalendarEventPlannerContext(options);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    [Test]
    public async Task AddEventReminder_PersistsToDatabase()
    {
        var reminder = CreateDefaultReminder();

        _context.EventReminders.Add(reminder);
        await _context.SaveChangesAsync();

        var retrieved = await _context.EventReminders.FindAsync(reminder.ReminderId);
        Assert.That(retrieved, Is.Not.Null);
    }

    [Test]
    public async Task EventReminder_PersistsAllProperties()
    {
        var eventId = Guid.NewGuid();
        var recipientId = Guid.NewGuid();
        var reminderTime = DateTime.UtcNow.AddHours(1);
        var reminder = new EventReminder(eventId, recipientId, reminderTime, NotificationChannel.Email);

        _context.EventReminders.Add(reminder);
        await _context.SaveChangesAsync();

        _context.ChangeTracker.Clear();
        var retrieved = await _context.EventReminders.FindAsync(reminder.ReminderId);

        Assert.Multiple(() =>
        {
            Assert.That(retrieved, Is.Not.Null);
            Assert.That(retrieved!.EventId, Is.EqualTo(eventId));
            Assert.That(retrieved.RecipientId, Is.EqualTo(recipientId));
            Assert.That(retrieved.ReminderTime, Is.EqualTo(reminderTime));
            Assert.That(retrieved.DeliveryChannel, Is.EqualTo(NotificationChannel.Email));
            Assert.That(retrieved.Sent, Is.False);
        });
    }

    [Test]
    public async Task EventReminder_CanSendAndPersist()
    {
        var reminder = CreateDefaultReminder();
        _context.EventReminders.Add(reminder);
        await _context.SaveChangesAsync();

        reminder.Send();
        await _context.SaveChangesAsync();

        _context.ChangeTracker.Clear();
        var retrieved = await _context.EventReminders.FindAsync(reminder.ReminderId);

        Assert.That(retrieved!.Sent, Is.True);
    }

    [Test]
    public async Task EventReminder_CanRescheduleAndPersist()
    {
        var reminder = CreateDefaultReminder();
        _context.EventReminders.Add(reminder);
        await _context.SaveChangesAsync();

        var newTime = DateTime.UtcNow.AddHours(5);
        reminder.Reschedule(newTime);
        await _context.SaveChangesAsync();

        _context.ChangeTracker.Clear();
        var retrieved = await _context.EventReminders.FindAsync(reminder.ReminderId);

        Assert.That(retrieved!.ReminderTime, Is.EqualTo(newTime));
    }

    [Test]
    public async Task EventReminder_CanChangeChannelAndPersist()
    {
        var reminder = CreateDefaultReminder();
        _context.EventReminders.Add(reminder);
        await _context.SaveChangesAsync();

        reminder.ChangeChannel(NotificationChannel.SMS);
        await _context.SaveChangesAsync();

        _context.ChangeTracker.Clear();
        var retrieved = await _context.EventReminders.FindAsync(reminder.ReminderId);

        Assert.That(retrieved!.DeliveryChannel, Is.EqualTo(NotificationChannel.SMS));
    }

    [Test]
    public async Task EventReminder_PersistsAllDeliveryChannels()
    {
        var eventId = Guid.NewGuid();
        var recipientId = Guid.NewGuid();
        var futureTime = DateTime.UtcNow.AddHours(1);

        var emailReminder = new EventReminder(eventId, recipientId, futureTime, NotificationChannel.Email);
        var pushReminder = new EventReminder(eventId, recipientId, futureTime.AddMinutes(1), NotificationChannel.Push);
        var smsReminder = new EventReminder(eventId, recipientId, futureTime.AddMinutes(2), NotificationChannel.SMS);

        _context.EventReminders.AddRange(emailReminder, pushReminder, smsReminder);
        await _context.SaveChangesAsync();

        _context.ChangeTracker.Clear();

        var emailRetrieved = await _context.EventReminders.FindAsync(emailReminder.ReminderId);
        var pushRetrieved = await _context.EventReminders.FindAsync(pushReminder.ReminderId);
        var smsRetrieved = await _context.EventReminders.FindAsync(smsReminder.ReminderId);

        Assert.Multiple(() =>
        {
            Assert.That(emailRetrieved!.DeliveryChannel, Is.EqualTo(NotificationChannel.Email));
            Assert.That(pushRetrieved!.DeliveryChannel, Is.EqualTo(NotificationChannel.Push));
            Assert.That(smsRetrieved!.DeliveryChannel, Is.EqualTo(NotificationChannel.SMS));
        });
    }

    [Test]
    public async Task EventReminder_CanQueryByEventId()
    {
        var eventId = Guid.NewGuid();
        var futureTime = DateTime.UtcNow.AddHours(1);

        var reminder1 = new EventReminder(eventId, Guid.NewGuid(), futureTime, NotificationChannel.Email);
        var reminder2 = new EventReminder(eventId, Guid.NewGuid(), futureTime.AddMinutes(1), NotificationChannel.Push);
        var reminder3 = new EventReminder(Guid.NewGuid(), Guid.NewGuid(), futureTime.AddMinutes(2), NotificationChannel.Email);

        _context.EventReminders.AddRange(reminder1, reminder2, reminder3);
        await _context.SaveChangesAsync();

        var eventReminders = await _context.EventReminders
            .Where(r => r.EventId == eventId)
            .ToListAsync();

        Assert.That(eventReminders, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task EventReminder_CanQueryByRecipientId()
    {
        var recipientId = Guid.NewGuid();
        var futureTime = DateTime.UtcNow.AddHours(1);

        var reminder1 = new EventReminder(Guid.NewGuid(), recipientId, futureTime, NotificationChannel.Email);
        var reminder2 = new EventReminder(Guid.NewGuid(), recipientId, futureTime.AddMinutes(1), NotificationChannel.Push);
        var reminder3 = new EventReminder(Guid.NewGuid(), Guid.NewGuid(), futureTime.AddMinutes(2), NotificationChannel.Email);

        _context.EventReminders.AddRange(reminder1, reminder2, reminder3);
        await _context.SaveChangesAsync();

        var recipientReminders = await _context.EventReminders
            .Where(r => r.RecipientId == recipientId)
            .ToListAsync();

        Assert.That(recipientReminders, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task EventReminder_CanQueryBySentStatus()
    {
        var futureTime = DateTime.UtcNow.AddHours(1);

        var unsent1 = new EventReminder(Guid.NewGuid(), Guid.NewGuid(), futureTime, NotificationChannel.Email);
        var unsent2 = new EventReminder(Guid.NewGuid(), Guid.NewGuid(), futureTime.AddMinutes(1), NotificationChannel.Push);
        var sent = new EventReminder(Guid.NewGuid(), Guid.NewGuid(), futureTime.AddMinutes(2), NotificationChannel.Email);
        sent.Send();

        _context.EventReminders.AddRange(unsent1, unsent2, sent);
        await _context.SaveChangesAsync();

        var unsentReminders = await _context.EventReminders
            .Where(r => !r.Sent)
            .ToListAsync();

        Assert.That(unsentReminders, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task EventReminder_CanQueryByReminderTime()
    {
        var now = DateTime.UtcNow;
        var targetTime = now.AddHours(2);

        var earlier = new EventReminder(Guid.NewGuid(), Guid.NewGuid(), now.AddHours(1), NotificationChannel.Email);
        var onTarget = new EventReminder(Guid.NewGuid(), Guid.NewGuid(), targetTime, NotificationChannel.Push);
        var later = new EventReminder(Guid.NewGuid(), Guid.NewGuid(), now.AddHours(3), NotificationChannel.SMS);

        _context.EventReminders.AddRange(earlier, onTarget, later);
        await _context.SaveChangesAsync();

        var dueReminders = await _context.EventReminders
            .Where(r => r.ReminderTime <= targetTime)
            .ToListAsync();

        Assert.That(dueReminders, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task EventReminder_CanDelete()
    {
        var reminder = CreateDefaultReminder();
        _context.EventReminders.Add(reminder);
        await _context.SaveChangesAsync();

        _context.EventReminders.Remove(reminder);
        await _context.SaveChangesAsync();

        var retrieved = await _context.EventReminders.FindAsync(reminder.ReminderId);
        Assert.That(retrieved, Is.Null);
    }

    private EventReminder CreateDefaultReminder()
    {
        return new EventReminder(
            Guid.NewGuid(),
            Guid.NewGuid(),
            DateTime.UtcNow.AddHours(1),
            NotificationChannel.Email);
    }
}
