using FamilyCalendarEventPlanner.Core.Models.AttendeeAggregate;
using FamilyCalendarEventPlanner.Core.Models.AttendeeAggregate.Enums;

namespace FamilyCalendarEventPlanner.Core.Tests;

public class EventAttendeeTests
{
    private readonly Guid _eventId = Guid.NewGuid();
    private readonly Guid _familyMemberId = Guid.NewGuid();

    [Test]
    public void Constructor_ValidParameters_CreatesAttendee()
    {
        var attendee = new EventAttendee(TestHelpers.DefaultTenantId, _eventId, _familyMemberId);

        Assert.Multiple(() =>
        {
            Assert.That(attendee.AttendeeId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(attendee.EventId, Is.EqualTo(_eventId));
            Assert.That(attendee.FamilyMemberId, Is.EqualTo(_familyMemberId));
            Assert.That(attendee.RSVPStatus, Is.EqualTo(RSVPStatus.Pending));
            Assert.That(attendee.ResponseTime, Is.Null);
        });
    }

    [Test]
    public void Constructor_WithNotes_SetsNotes()
    {
        var attendee = new EventAttendee(TestHelpers.DefaultTenantId, _eventId, _familyMemberId, "Will bring dessert");

        Assert.That(attendee.Notes, Is.EqualTo("Will bring dessert"));
    }

    [Test]
    public void Respond_AcceptedStatus_UpdatesStatusAndResponseTime()
    {
        var attendee = new EventAttendee(TestHelpers.DefaultTenantId, _eventId, _familyMemberId);
        var beforeResponse = DateTime.UtcNow;

        attendee.Respond(RSVPStatus.Accepted);

        Assert.Multiple(() =>
        {
            Assert.That(attendee.RSVPStatus, Is.EqualTo(RSVPStatus.Accepted));
            Assert.That(attendee.ResponseTime, Is.Not.Null);
            Assert.That(attendee.ResponseTime, Is.GreaterThanOrEqualTo(beforeResponse));
        });
    }

    [Test]
    public void Respond_WithNotes_UpdatesNotes()
    {
        var attendee = new EventAttendee(TestHelpers.DefaultTenantId, _eventId, _familyMemberId);

        attendee.Respond(RSVPStatus.Accepted, "Looking forward to it!");

        Assert.That(attendee.Notes, Is.EqualTo("Looking forward to it!"));
    }

    [Test]
    public void Accept_SetsStatusToAccepted()
    {
        var attendee = new EventAttendee(TestHelpers.DefaultTenantId, _eventId, _familyMemberId);

        attendee.Accept();

        Assert.That(attendee.RSVPStatus, Is.EqualTo(RSVPStatus.Accepted));
    }

    [Test]
    public void Accept_WithNotes_SetsNotesAndStatus()
    {
        var attendee = new EventAttendee(TestHelpers.DefaultTenantId, _eventId, _familyMemberId);

        attendee.Accept("I'll be there!");

        Assert.Multiple(() =>
        {
            Assert.That(attendee.RSVPStatus, Is.EqualTo(RSVPStatus.Accepted));
            Assert.That(attendee.Notes, Is.EqualTo("I'll be there!"));
        });
    }

    [Test]
    public void Decline_SetsStatusToDeclined()
    {
        var attendee = new EventAttendee(TestHelpers.DefaultTenantId, _eventId, _familyMemberId);

        attendee.Decline();

        Assert.That(attendee.RSVPStatus, Is.EqualTo(RSVPStatus.Declined));
    }

    [Test]
    public void Decline_WithNotes_SetsNotesAndStatus()
    {
        var attendee = new EventAttendee(TestHelpers.DefaultTenantId, _eventId, _familyMemberId);

        attendee.Decline("Can't make it, sorry!");

        Assert.Multiple(() =>
        {
            Assert.That(attendee.RSVPStatus, Is.EqualTo(RSVPStatus.Declined));
            Assert.That(attendee.Notes, Is.EqualTo("Can't make it, sorry!"));
        });
    }

    [Test]
    public void MarkTentative_SetsStatusToTentative()
    {
        var attendee = new EventAttendee(TestHelpers.DefaultTenantId, _eventId, _familyMemberId);

        attendee.MarkTentative();

        Assert.That(attendee.RSVPStatus, Is.EqualTo(RSVPStatus.Tentative));
    }

    [Test]
    public void MarkTentative_WithNotes_SetsNotesAndStatus()
    {
        var attendee = new EventAttendee(TestHelpers.DefaultTenantId, _eventId, _familyMemberId);

        attendee.MarkTentative("Not sure yet, will confirm later");

        Assert.Multiple(() =>
        {
            Assert.That(attendee.RSVPStatus, Is.EqualTo(RSVPStatus.Tentative));
            Assert.That(attendee.Notes, Is.EqualTo("Not sure yet, will confirm later"));
        });
    }

    [Test]
    public void Respond_MultipleResponses_UpdatesEachTime()
    {
        var attendee = new EventAttendee(TestHelpers.DefaultTenantId, _eventId, _familyMemberId);

        attendee.Accept();
        var firstResponseTime = attendee.ResponseTime!.Value;

        System.Threading.Thread.Sleep(10);
        attendee.Decline("Changed my mind");

        Assert.Multiple(() =>
        {
            Assert.That(attendee.RSVPStatus, Is.EqualTo(RSVPStatus.Declined));
            Assert.That(attendee.ResponseTime, Is.GreaterThan(firstResponseTime));
        });
    }
}
