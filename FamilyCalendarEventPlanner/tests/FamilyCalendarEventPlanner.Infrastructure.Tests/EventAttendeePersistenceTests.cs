namespace FamilyCalendarEventPlanner.Infrastructure.Tests;

public class EventAttendeePersistenceTests
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
    public async Task AddEventAttendee_PersistsToDatabase()
    {
        var attendee = CreateDefaultAttendee();

        _context.EventAttendees.Add(attendee);
        await _context.SaveChangesAsync();

        var retrieved = await _context.EventAttendees.FindAsync(attendee.AttendeeId);
        Assert.That(retrieved, Is.Not.Null);
    }

    [Test]
    public async Task EventAttendee_PersistsAllProperties()
    {
        var eventId = Guid.NewGuid();
        var memberId = Guid.NewGuid();
        var attendee = new EventAttendee(eventId, memberId, "Looking forward to it!");

        _context.EventAttendees.Add(attendee);
        await _context.SaveChangesAsync();

        _context.ChangeTracker.Clear();
        var retrieved = await _context.EventAttendees.FindAsync(attendee.AttendeeId);

        Assert.Multiple(() =>
        {
            Assert.That(retrieved, Is.Not.Null);
            Assert.That(retrieved!.EventId, Is.EqualTo(eventId));
            Assert.That(retrieved.FamilyMemberId, Is.EqualTo(memberId));
            Assert.That(retrieved.Notes, Is.EqualTo("Looking forward to it!"));
            Assert.That(retrieved.RSVPStatus, Is.EqualTo(RSVPStatus.Pending));
        });
    }

    [Test]
    public async Task EventAttendee_CanAcceptAndPersist()
    {
        var attendee = CreateDefaultAttendee();
        _context.EventAttendees.Add(attendee);
        await _context.SaveChangesAsync();

        attendee.Accept("Will be there!");
        await _context.SaveChangesAsync();

        _context.ChangeTracker.Clear();
        var retrieved = await _context.EventAttendees.FindAsync(attendee.AttendeeId);

        Assert.Multiple(() =>
        {
            Assert.That(retrieved!.RSVPStatus, Is.EqualTo(RSVPStatus.Accepted));
            Assert.That(retrieved.Notes, Is.EqualTo("Will be there!"));
            Assert.That(retrieved.ResponseTime, Is.Not.Null);
        });
    }

    [Test]
    public async Task EventAttendee_CanDeclineAndPersist()
    {
        var attendee = CreateDefaultAttendee();
        _context.EventAttendees.Add(attendee);
        await _context.SaveChangesAsync();

        attendee.Decline("Cannot make it");
        await _context.SaveChangesAsync();

        _context.ChangeTracker.Clear();
        var retrieved = await _context.EventAttendees.FindAsync(attendee.AttendeeId);

        Assert.That(retrieved!.RSVPStatus, Is.EqualTo(RSVPStatus.Declined));
    }

    [Test]
    public async Task EventAttendee_CanMarkTentativeAndPersist()
    {
        var attendee = CreateDefaultAttendee();
        _context.EventAttendees.Add(attendee);
        await _context.SaveChangesAsync();

        attendee.MarkTentative("Not sure yet");
        await _context.SaveChangesAsync();

        _context.ChangeTracker.Clear();
        var retrieved = await _context.EventAttendees.FindAsync(attendee.AttendeeId);

        Assert.That(retrieved!.RSVPStatus, Is.EqualTo(RSVPStatus.Tentative));
    }

    [Test]
    public async Task EventAttendee_CanQueryByEventId()
    {
        var eventId = Guid.NewGuid();

        var attendee1 = new EventAttendee(eventId, Guid.NewGuid());
        var attendee2 = new EventAttendee(eventId, Guid.NewGuid());
        var attendee3 = new EventAttendee(Guid.NewGuid(), Guid.NewGuid());

        _context.EventAttendees.AddRange(attendee1, attendee2, attendee3);
        await _context.SaveChangesAsync();

        var eventAttendees = await _context.EventAttendees
            .Where(a => a.EventId == eventId)
            .ToListAsync();

        Assert.That(eventAttendees, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task EventAttendee_CanQueryByMemberId()
    {
        var memberId = Guid.NewGuid();

        var attendee1 = new EventAttendee(Guid.NewGuid(), memberId);
        var attendee2 = new EventAttendee(Guid.NewGuid(), memberId);
        var attendee3 = new EventAttendee(Guid.NewGuid(), Guid.NewGuid());

        _context.EventAttendees.AddRange(attendee1, attendee2, attendee3);
        await _context.SaveChangesAsync();

        var memberAttendances = await _context.EventAttendees
            .Where(a => a.FamilyMemberId == memberId)
            .ToListAsync();

        Assert.That(memberAttendances, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task EventAttendee_CanDelete()
    {
        var attendee = CreateDefaultAttendee();
        _context.EventAttendees.Add(attendee);
        await _context.SaveChangesAsync();

        _context.EventAttendees.Remove(attendee);
        await _context.SaveChangesAsync();

        var retrieved = await _context.EventAttendees.FindAsync(attendee.AttendeeId);
        Assert.That(retrieved, Is.Null);
    }

    [Test]
    public async Task EventAttendee_PersistsAllRSVPStatuses()
    {
        var eventId = Guid.NewGuid();

        var pending = new EventAttendee(eventId, Guid.NewGuid());
        var accepted = new EventAttendee(eventId, Guid.NewGuid());
        accepted.Accept();
        var declined = new EventAttendee(eventId, Guid.NewGuid());
        declined.Decline();
        var tentative = new EventAttendee(eventId, Guid.NewGuid());
        tentative.MarkTentative();

        _context.EventAttendees.AddRange(pending, accepted, declined, tentative);
        await _context.SaveChangesAsync();

        _context.ChangeTracker.Clear();

        var pendingRetrieved = await _context.EventAttendees.FindAsync(pending.AttendeeId);
        var acceptedRetrieved = await _context.EventAttendees.FindAsync(accepted.AttendeeId);
        var declinedRetrieved = await _context.EventAttendees.FindAsync(declined.AttendeeId);
        var tentativeRetrieved = await _context.EventAttendees.FindAsync(tentative.AttendeeId);

        Assert.Multiple(() =>
        {
            Assert.That(pendingRetrieved!.RSVPStatus, Is.EqualTo(RSVPStatus.Pending));
            Assert.That(acceptedRetrieved!.RSVPStatus, Is.EqualTo(RSVPStatus.Accepted));
            Assert.That(declinedRetrieved!.RSVPStatus, Is.EqualTo(RSVPStatus.Declined));
            Assert.That(tentativeRetrieved!.RSVPStatus, Is.EqualTo(RSVPStatus.Tentative));
        });
    }

    private EventAttendee CreateDefaultAttendee()
    {
        return new EventAttendee(Guid.NewGuid(), Guid.NewGuid());
    }
}
