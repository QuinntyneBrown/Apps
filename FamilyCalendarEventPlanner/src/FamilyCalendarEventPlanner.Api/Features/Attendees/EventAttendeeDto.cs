using FamilyCalendarEventPlanner.Core.Model.AttendeeAggregate;
using FamilyCalendarEventPlanner.Core.Model.AttendeeAggregate.Enums;

namespace FamilyCalendarEventPlanner.Api.Features.Attendees;

public record EventAttendeeDto
{
    public Guid AttendeeId { get; init; }
    public Guid EventId { get; init; }
    public Guid FamilyMemberId { get; init; }
    public RSVPStatus RsvpStatus { get; init; }
    public DateTime? ResponseTime { get; init; }
    public string Notes { get; init; } = string.Empty;
}

public static class EventAttendeeExtensions
{
    public static EventAttendeeDto ToDto(this EventAttendee attendee)
    {
        return new EventAttendeeDto
        {
            AttendeeId = attendee.AttendeeId,
            EventId = attendee.EventId,
            FamilyMemberId = attendee.FamilyMemberId,
            RsvpStatus = attendee.RSVPStatus,
            ResponseTime = attendee.ResponseTime,
            Notes = attendee.Notes,
        };
    }
}
