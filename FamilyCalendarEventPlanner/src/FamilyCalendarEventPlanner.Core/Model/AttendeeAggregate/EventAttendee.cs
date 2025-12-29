namespace FamilyCalendarEventPlanner.Core;

public class EventAttendee
{
    public Guid AttendeeId { get; private set; }
    public Guid EventId { get; private set; }
    public Guid FamilyMemberId { get; private set; }
    public RSVPStatus RSVPStatus { get; private set; }
    public DateTime? ResponseTime { get; private set; }
    public string Notes { get; private set; } = string.Empty;

    private EventAttendee()
    {
    }

    public EventAttendee(Guid eventId, Guid familyMemberId, string? notes = null)
    {
        AttendeeId = Guid.NewGuid();
        EventId = eventId;
        FamilyMemberId = familyMemberId;
        RSVPStatus = RSVPStatus.Pending;
        ResponseTime = null;
        Notes = notes ?? string.Empty;
    }

    public void Respond(RSVPStatus status, string? notes = null)
    {
        RSVPStatus = status;
        ResponseTime = DateTime.UtcNow;

        if (notes != null)
        {
            Notes = notes;
        }
    }

    public void Accept(string? notes = null)
    {
        Respond(RSVPStatus.Accepted, notes);
    }

    public void Decline(string? notes = null)
    {
        Respond(RSVPStatus.Declined, notes);
    }

    public void MarkTentative(string? notes = null)
    {
        Respond(RSVPStatus.Tentative, notes);
    }
}
