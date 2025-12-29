namespace FamilyCalendarEventPlanner.Infrastructure.Tests;

public class FamilyCalendarEventPlannerContextTests
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
    public void Context_ImplementsIFamilyCalendarEventPlannerContext()
    {
        Assert.That(_context, Is.InstanceOf<IFamilyCalendarEventPlannerContext>());
    }

    [Test]
    public void Context_HasCalendarEventsDbSet()
    {
        Assert.That(_context.CalendarEvents, Is.Not.Null);
    }

    [Test]
    public void Context_HasEventAttendeesDbSet()
    {
        Assert.That(_context.EventAttendees, Is.Not.Null);
    }

    [Test]
    public void Context_HasFamilyMembersDbSet()
    {
        Assert.That(_context.FamilyMembers, Is.Not.Null);
    }

    [Test]
    public void Context_HasAvailabilityBlocksDbSet()
    {
        Assert.That(_context.AvailabilityBlocks, Is.Not.Null);
    }

    [Test]
    public void Context_HasScheduleConflictsDbSet()
    {
        Assert.That(_context.ScheduleConflicts, Is.Not.Null);
    }

    [Test]
    public void Context_HasEventRemindersDbSet()
    {
        Assert.That(_context.EventReminders, Is.Not.Null);
    }

    [Test]
    public async Task SaveChangesAsync_ReturnsNumberOfAffectedEntities()
    {
        var familyId = Guid.NewGuid();
        var creatorId = Guid.NewGuid();
        var calendarEvent = new CalendarEvent(
            familyId,
            creatorId,
            "Test Event",
            DateTime.UtcNow.AddHours(1),
            DateTime.UtcNow.AddHours(2),
            EventType.Other);

        _context.CalendarEvents.Add(calendarEvent);
        var result = await _context.SaveChangesAsync();

        Assert.That(result, Is.EqualTo(1));
    }
}
