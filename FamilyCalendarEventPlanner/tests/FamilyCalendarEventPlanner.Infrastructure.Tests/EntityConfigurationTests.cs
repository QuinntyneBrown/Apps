namespace FamilyCalendarEventPlanner.Infrastructure.Tests;

public class EntityConfigurationTests
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
    public void CalendarEventConfiguration_HasCorrectKeyConfiguration()
    {
        var entityType = _context.Model.FindEntityType(typeof(CalendarEvent));

        Assert.That(entityType, Is.Not.Null);
        Assert.That(entityType!.FindPrimaryKey()?.Properties[0].Name, Is.EqualTo("EventId"));
    }

    [Test]
    public void EventAttendeeConfiguration_HasCorrectKeyConfiguration()
    {
        var entityType = _context.Model.FindEntityType(typeof(EventAttendee));

        Assert.That(entityType, Is.Not.Null);
        Assert.That(entityType!.FindPrimaryKey()?.Properties[0].Name, Is.EqualTo("AttendeeId"));
    }

    [Test]
    public void FamilyMemberConfiguration_HasCorrectKeyConfiguration()
    {
        var entityType = _context.Model.FindEntityType(typeof(FamilyMember));

        Assert.That(entityType, Is.Not.Null);
        Assert.That(entityType!.FindPrimaryKey()?.Properties[0].Name, Is.EqualTo("MemberId"));
    }

    [Test]
    public void AvailabilityBlockConfiguration_HasCorrectKeyConfiguration()
    {
        var entityType = _context.Model.FindEntityType(typeof(AvailabilityBlock));

        Assert.That(entityType, Is.Not.Null);
        Assert.That(entityType!.FindPrimaryKey()?.Properties[0].Name, Is.EqualTo("BlockId"));
    }

    [Test]
    public void ScheduleConflictConfiguration_HasCorrectKeyConfiguration()
    {
        var entityType = _context.Model.FindEntityType(typeof(ScheduleConflict));

        Assert.That(entityType, Is.Not.Null);
        Assert.That(entityType!.FindPrimaryKey()?.Properties[0].Name, Is.EqualTo("ConflictId"));
    }

    [Test]
    public void EventReminderConfiguration_HasCorrectKeyConfiguration()
    {
        var entityType = _context.Model.FindEntityType(typeof(EventReminder));

        Assert.That(entityType, Is.Not.Null);
        Assert.That(entityType!.FindPrimaryKey()?.Properties[0].Name, Is.EqualTo("ReminderId"));
    }

    [Test]
    public void CalendarEvent_RecurrencePatternIsOwnedType()
    {
        var entityType = _context.Model.FindEntityType(typeof(CalendarEvent));
        var recurrenceNavigation = entityType?.FindNavigation("RecurrencePattern");

        Assert.That(recurrenceNavigation?.TargetEntityType.IsOwned(), Is.True);
    }

    [Test]
    public void CalendarEvent_HasFamilyIdIndex()
    {
        var entityType = _context.Model.FindEntityType(typeof(CalendarEvent));
        var index = entityType?.GetIndexes()
            .FirstOrDefault(i => i.Properties.Any(p => p.Name == "FamilyId"));

        Assert.That(index, Is.Not.Null);
    }

    [Test]
    public void FamilyMember_HasEmailIndex()
    {
        var entityType = _context.Model.FindEntityType(typeof(FamilyMember));
        var index = entityType?.GetIndexes()
            .FirstOrDefault(i => i.Properties.Any(p => p.Name == "Email"));

        Assert.That(index, Is.Not.Null);
    }

    [Test]
    public void EventAttendee_HasEventIdIndex()
    {
        var entityType = _context.Model.FindEntityType(typeof(EventAttendee));
        var index = entityType?.GetIndexes()
            .FirstOrDefault(i => i.Properties.Any(p => p.Name == "EventId"));

        Assert.That(index, Is.Not.Null);
    }

    [Test]
    public void AvailabilityBlock_HasMemberIdIndex()
    {
        var entityType = _context.Model.FindEntityType(typeof(AvailabilityBlock));
        var index = entityType?.GetIndexes()
            .FirstOrDefault(i => i.Properties.Any(p => p.Name == "MemberId"));

        Assert.That(index, Is.Not.Null);
    }

    [Test]
    public void ScheduleConflict_HasIsResolvedIndex()
    {
        var entityType = _context.Model.FindEntityType(typeof(ScheduleConflict));
        var index = entityType?.GetIndexes()
            .FirstOrDefault(i => i.Properties.Any(p => p.Name == "IsResolved"));

        Assert.That(index, Is.Not.Null);
    }

    [Test]
    public void EventReminder_HasEventIdIndex()
    {
        var entityType = _context.Model.FindEntityType(typeof(EventReminder));
        var index = entityType?.GetIndexes()
            .FirstOrDefault(i => i.Properties.Any(p => p.Name == "EventId"));

        Assert.That(index, Is.Not.Null);
    }

    [Test]
    public void EventReminder_HasSentIndex()
    {
        var entityType = _context.Model.FindEntityType(typeof(EventReminder));
        var index = entityType?.GetIndexes()
            .FirstOrDefault(i => i.Properties.Any(p => p.Name == "Sent"));

        Assert.That(index, Is.Not.Null);
    }

    [Test]
    public void CalendarEvent_TitlePropertyIsRequired()
    {
        var entityType = _context.Model.FindEntityType(typeof(CalendarEvent));
        var property = entityType?.FindProperty("Title");

        Assert.That(property?.IsNullable, Is.False);
    }

    [Test]
    public void FamilyMember_NamePropertyIsRequired()
    {
        var entityType = _context.Model.FindEntityType(typeof(FamilyMember));
        var property = entityType?.FindProperty("Name");

        Assert.That(property?.IsNullable, Is.False);
    }

    [Test]
    public void FamilyMember_EmailPropertyIsRequired()
    {
        var entityType = _context.Model.FindEntityType(typeof(FamilyMember));
        var property = entityType?.FindProperty("Email");

        Assert.That(property?.IsNullable, Is.False);
    }

    [Test]
    public void FamilyMember_ColorPropertyIsRequired()
    {
        var entityType = _context.Model.FindEntityType(typeof(FamilyMember));
        var property = entityType?.FindProperty("Color");

        Assert.That(property?.IsNullable, Is.False);
    }
}
