namespace FamilyCalendarEventPlanner.Infrastructure.Tests;

public class CalendarEventPersistenceTests
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
    public async Task AddCalendarEvent_PersistsToDatabase()
    {
        var calendarEvent = CreateDefaultEvent();

        _context.CalendarEvents.Add(calendarEvent);
        await _context.SaveChangesAsync();

        var retrieved = await _context.CalendarEvents.FindAsync(calendarEvent.EventId);
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Test Event"));
    }

    [Test]
    public async Task CalendarEvent_PersistsAllProperties()
    {
        var familyId = Guid.NewGuid();
        var creatorId = Guid.NewGuid();
        var startTime = DateTime.UtcNow.AddHours(1);
        var endTime = DateTime.UtcNow.AddHours(2);

        var calendarEvent = new CalendarEvent(
            familyId,
            creatorId,
            "Family Dinner",
            startTime,
            endTime,
            EventType.FamilyDinner,
            description: "Monthly family dinner",
            location: "Home");

        _context.CalendarEvents.Add(calendarEvent);
        await _context.SaveChangesAsync();

        _context.ChangeTracker.Clear();
        var retrieved = await _context.CalendarEvents.FindAsync(calendarEvent.EventId);

        Assert.Multiple(() =>
        {
            Assert.That(retrieved, Is.Not.Null);
            Assert.That(retrieved!.FamilyId, Is.EqualTo(familyId));
            Assert.That(retrieved.CreatorId, Is.EqualTo(creatorId));
            Assert.That(retrieved.Title, Is.EqualTo("Family Dinner"));
            Assert.That(retrieved.Description, Is.EqualTo("Monthly family dinner"));
            Assert.That(retrieved.Location, Is.EqualTo("Home"));
            Assert.That(retrieved.EventType, Is.EqualTo(EventType.FamilyDinner));
            Assert.That(retrieved.Status, Is.EqualTo(EventStatus.Scheduled));
        });
    }

    [Test]
    public async Task CalendarEvent_PersistsRecurrencePattern()
    {
        var recurrence = RecurrencePattern.Weekly(
            interval: 2,
            endDate: DateTime.UtcNow.AddMonths(3),
            daysOfWeek: new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Wednesday });

        var calendarEvent = new CalendarEvent(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Weekly Meeting",
            DateTime.UtcNow.AddHours(1),
            DateTime.UtcNow.AddHours(2),
            EventType.Appointment,
            recurrencePattern: recurrence);

        _context.CalendarEvents.Add(calendarEvent);
        await _context.SaveChangesAsync();

        _context.ChangeTracker.Clear();
        var retrieved = await _context.CalendarEvents.FindAsync(calendarEvent.EventId);

        Assert.Multiple(() =>
        {
            Assert.That(retrieved, Is.Not.Null);
            Assert.That(retrieved!.RecurrencePattern.Frequency, Is.EqualTo(RecurrenceFrequency.Weekly));
            Assert.That(retrieved.RecurrencePattern.Interval, Is.EqualTo(2));
            Assert.That(retrieved.RecurrencePattern.DaysOfWeek, Contains.Item(DayOfWeek.Monday));
            Assert.That(retrieved.RecurrencePattern.DaysOfWeek, Contains.Item(DayOfWeek.Wednesday));
        });
    }

    [Test]
    public async Task CalendarEvent_UpdatesStatus()
    {
        var calendarEvent = CreateDefaultEvent();
        _context.CalendarEvents.Add(calendarEvent);
        await _context.SaveChangesAsync();

        calendarEvent.Cancel();
        await _context.SaveChangesAsync();

        _context.ChangeTracker.Clear();
        var retrieved = await _context.CalendarEvents.FindAsync(calendarEvent.EventId);

        Assert.That(retrieved!.Status, Is.EqualTo(EventStatus.Cancelled));
    }

    [Test]
    public async Task CalendarEvent_CanBeModifiedAndPersisted()
    {
        var calendarEvent = CreateDefaultEvent();
        _context.CalendarEvents.Add(calendarEvent);
        await _context.SaveChangesAsync();

        calendarEvent.Modify(title: "Updated Title", description: "Updated Description");
        await _context.SaveChangesAsync();

        _context.ChangeTracker.Clear();
        var retrieved = await _context.CalendarEvents.FindAsync(calendarEvent.EventId);

        Assert.Multiple(() =>
        {
            Assert.That(retrieved!.Title, Is.EqualTo("Updated Title"));
            Assert.That(retrieved.Description, Is.EqualTo("Updated Description"));
        });
    }

    [Test]
    public async Task CalendarEvent_CanQueryByFamilyId()
    {
        var familyId = Guid.NewGuid();

        var event1 = new CalendarEvent(familyId, Guid.NewGuid(), "Event 1",
            DateTime.UtcNow.AddHours(1), DateTime.UtcNow.AddHours(2), EventType.Other);
        var event2 = new CalendarEvent(familyId, Guid.NewGuid(), "Event 2",
            DateTime.UtcNow.AddHours(3), DateTime.UtcNow.AddHours(4), EventType.Other);
        var event3 = new CalendarEvent(Guid.NewGuid(), Guid.NewGuid(), "Event 3",
            DateTime.UtcNow.AddHours(5), DateTime.UtcNow.AddHours(6), EventType.Other);

        _context.CalendarEvents.AddRange(event1, event2, event3);
        await _context.SaveChangesAsync();

        var familyEvents = await _context.CalendarEvents
            .Where(e => e.FamilyId == familyId)
            .ToListAsync();

        Assert.That(familyEvents, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task CalendarEvent_CanDelete()
    {
        var calendarEvent = CreateDefaultEvent();
        _context.CalendarEvents.Add(calendarEvent);
        await _context.SaveChangesAsync();

        _context.CalendarEvents.Remove(calendarEvent);
        await _context.SaveChangesAsync();

        var retrieved = await _context.CalendarEvents.FindAsync(calendarEvent.EventId);
        Assert.That(retrieved, Is.Null);
    }

    private CalendarEvent CreateDefaultEvent()
    {
        return new CalendarEvent(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Test Event",
            DateTime.UtcNow.AddHours(1),
            DateTime.UtcNow.AddHours(2),
            EventType.Other);
    }
}
