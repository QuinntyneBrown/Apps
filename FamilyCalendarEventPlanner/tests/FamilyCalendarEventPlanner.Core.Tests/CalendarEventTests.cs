namespace FamilyCalendarEventPlanner.Core.Tests;

public class CalendarEventTests
{
    private readonly Guid _familyId = Guid.NewGuid();
    private readonly Guid _creatorId = Guid.NewGuid();

    [Test]
    public void Constructor_ValidParameters_CreatesEvent()
    {
        var startTime = DateTime.UtcNow.AddHours(1);
        var endTime = DateTime.UtcNow.AddHours(2);

        var calendarEvent = new CalendarEvent(
            _familyId,
            _creatorId,
            "Family Dinner",
            startTime,
            endTime,
            EventType.FamilyDinner);

        Assert.Multiple(() =>
        {
            Assert.That(calendarEvent.EventId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(calendarEvent.FamilyId, Is.EqualTo(_familyId));
            Assert.That(calendarEvent.CreatorId, Is.EqualTo(_creatorId));
            Assert.That(calendarEvent.Title, Is.EqualTo("Family Dinner"));
            Assert.That(calendarEvent.StartTime, Is.EqualTo(startTime));
            Assert.That(calendarEvent.EndTime, Is.EqualTo(endTime));
            Assert.That(calendarEvent.EventType, Is.EqualTo(EventType.FamilyDinner));
            Assert.That(calendarEvent.Status, Is.EqualTo(EventStatus.Scheduled));
        });
    }

    [Test]
    public void Constructor_EmptyTitle_ThrowsArgumentException()
    {
        var startTime = DateTime.UtcNow.AddHours(1);
        var endTime = DateTime.UtcNow.AddHours(2);

        Assert.Throws<ArgumentException>(() =>
            new CalendarEvent(_familyId, _creatorId, "", startTime, endTime, EventType.Other));
    }

    [Test]
    public void Constructor_WhitespaceTitle_ThrowsArgumentException()
    {
        var startTime = DateTime.UtcNow.AddHours(1);
        var endTime = DateTime.UtcNow.AddHours(2);

        Assert.Throws<ArgumentException>(() =>
            new CalendarEvent(_familyId, _creatorId, "   ", startTime, endTime, EventType.Other));
    }

    [Test]
    public void Constructor_EndTimeBeforeStartTime_ThrowsArgumentException()
    {
        var startTime = DateTime.UtcNow.AddHours(2);
        var endTime = DateTime.UtcNow.AddHours(1);

        Assert.Throws<ArgumentException>(() =>
            new CalendarEvent(_familyId, _creatorId, "Test Event", startTime, endTime, EventType.Other));
    }

    [Test]
    public void Constructor_EndTimeEqualsStartTime_ThrowsArgumentException()
    {
        var time = DateTime.UtcNow.AddHours(1);

        Assert.Throws<ArgumentException>(() =>
            new CalendarEvent(_familyId, _creatorId, "Test Event", time, time, EventType.Other));
    }

    [Test]
    public void Constructor_WithOptionalParameters_SetsAllProperties()
    {
        var startTime = DateTime.UtcNow.AddHours(1);
        var endTime = DateTime.UtcNow.AddHours(2);
        var recurrence = RecurrencePattern.Weekly();

        var calendarEvent = new CalendarEvent(
            _familyId,
            _creatorId,
            "Weekly Meeting",
            startTime,
            endTime,
            EventType.Appointment,
            description: "Team sync",
            location: "Conference Room",
            recurrencePattern: recurrence);

        Assert.Multiple(() =>
        {
            Assert.That(calendarEvent.Description, Is.EqualTo("Team sync"));
            Assert.That(calendarEvent.Location, Is.EqualTo("Conference Room"));
            Assert.That(calendarEvent.RecurrencePattern.Frequency, Is.EqualTo(RecurrenceFrequency.Weekly));
        });
    }

    [Test]
    public void Modify_ValidTitle_UpdatesTitle()
    {
        var calendarEvent = CreateDefaultEvent();

        calendarEvent.Modify(title: "Updated Title");

        Assert.That(calendarEvent.Title, Is.EqualTo("Updated Title"));
    }

    [Test]
    public void Modify_EmptyTitle_ThrowsArgumentException()
    {
        var calendarEvent = CreateDefaultEvent();

        Assert.Throws<ArgumentException>(() => calendarEvent.Modify(title: ""));
    }

    [Test]
    public void Modify_ValidTimes_UpdatesTimes()
    {
        var calendarEvent = CreateDefaultEvent();
        var newStartTime = DateTime.UtcNow.AddDays(1);
        var newEndTime = DateTime.UtcNow.AddDays(1).AddHours(1);

        calendarEvent.Modify(startTime: newStartTime, endTime: newEndTime);

        Assert.Multiple(() =>
        {
            Assert.That(calendarEvent.StartTime, Is.EqualTo(newStartTime));
            Assert.That(calendarEvent.EndTime, Is.EqualTo(newEndTime));
        });
    }

    [Test]
    public void Modify_InvalidTimeRange_ThrowsArgumentException()
    {
        var calendarEvent = CreateDefaultEvent();

        Assert.Throws<ArgumentException>(() =>
            calendarEvent.Modify(startTime: DateTime.UtcNow.AddHours(3), endTime: DateTime.UtcNow.AddHours(2)));
    }

    [Test]
    public void Modify_CancelledEvent_ThrowsInvalidOperationException()
    {
        var calendarEvent = CreateDefaultEvent();
        calendarEvent.Cancel();

        Assert.Throws<InvalidOperationException>(() => calendarEvent.Modify(title: "New Title"));
    }

    [Test]
    public void Cancel_ScheduledEvent_SetsStatusToCancelled()
    {
        var calendarEvent = CreateDefaultEvent();

        calendarEvent.Cancel();

        Assert.That(calendarEvent.Status, Is.EqualTo(EventStatus.Cancelled));
    }

    [Test]
    public void Cancel_AlreadyCancelledEvent_ThrowsInvalidOperationException()
    {
        var calendarEvent = CreateDefaultEvent();
        calendarEvent.Cancel();

        Assert.Throws<InvalidOperationException>(() => calendarEvent.Cancel());
    }

    [Test]
    public void Complete_ScheduledEvent_SetsStatusToCompleted()
    {
        var calendarEvent = CreateDefaultEvent();

        calendarEvent.Complete();

        Assert.That(calendarEvent.Status, Is.EqualTo(EventStatus.Completed));
    }

    [Test]
    public void Complete_CancelledEvent_ThrowsInvalidOperationException()
    {
        var calendarEvent = CreateDefaultEvent();
        calendarEvent.Cancel();

        Assert.Throws<InvalidOperationException>(() => calendarEvent.Complete());
    }

    [Test]
    public void Complete_AlreadyCompletedEvent_ThrowsInvalidOperationException()
    {
        var calendarEvent = CreateDefaultEvent();
        calendarEvent.Complete();

        Assert.Throws<InvalidOperationException>(() => calendarEvent.Complete());
    }

    private CalendarEvent CreateDefaultEvent()
    {
        return new CalendarEvent(
            _familyId,
            _creatorId,
            "Test Event",
            DateTime.UtcNow.AddHours(1),
            DateTime.UtcNow.AddHours(2),
            EventType.Other);
    }
}
