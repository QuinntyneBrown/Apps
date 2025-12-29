using FamilyCalendarEventPlanner.Core;
using FamilyCalendarEventPlanner.Core.Model.AttendeeAggregate.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Api.Features.Attendees;

public record RespondToEventCommand : IRequest<EventAttendeeDto?>
{
    public Guid AttendeeId { get; init; }
    public RSVPStatus RsvpStatus { get; init; }
    public string? Notes { get; init; }
}

public class RespondToEventCommandHandler : IRequestHandler<RespondToEventCommand, EventAttendeeDto?>
{
    private readonly IFamilyCalendarEventPlannerContext _context;
    private readonly ILogger<RespondToEventCommandHandler> _logger;

    public RespondToEventCommandHandler(
        IFamilyCalendarEventPlannerContext context,
        ILogger<RespondToEventCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<EventAttendeeDto?> Handle(RespondToEventCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Updating RSVP for attendee {AttendeeId} to {RsvpStatus}",
            request.AttendeeId,
            request.RsvpStatus);

        var attendee = await _context.EventAttendees
            .FirstOrDefaultAsync(a => a.AttendeeId == request.AttendeeId, cancellationToken);

        if (attendee == null)
        {
            _logger.LogWarning("Attendee {AttendeeId} not found", request.AttendeeId);
            return null;
        }

        attendee.Respond(request.RsvpStatus, request.Notes);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Updated RSVP for attendee {AttendeeId} to {RsvpStatus}",
            request.AttendeeId,
            request.RsvpStatus);

        return attendee.ToDto();
    }
}
