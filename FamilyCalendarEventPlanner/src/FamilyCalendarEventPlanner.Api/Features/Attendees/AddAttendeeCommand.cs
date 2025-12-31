using FamilyCalendarEventPlanner.Core;
using FamilyCalendarEventPlanner.Core.Model.AttendeeAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Api.Features.Attendees;

public record AddAttendeeCommand : IRequest<EventAttendeeDto>
{
    public Guid EventId { get; init; }
    public Guid FamilyMemberId { get; init; }
    public string? Notes { get; init; }
}

public class AddAttendeeCommandHandler : IRequestHandler<AddAttendeeCommand, EventAttendeeDto>
{
    private readonly IFamilyCalendarEventPlannerContext _context;
    private readonly ITenantContext _tenantContext;
    private readonly ILogger<AddAttendeeCommandHandler> _logger;

    public AddAttendeeCommandHandler(
        IFamilyCalendarEventPlannerContext context,
        ITenantContext tenantContext,
        ILogger<AddAttendeeCommandHandler> logger)
    {
        _context = context;
        _tenantContext = tenantContext;
        _logger = logger;
    }

    public async Task<EventAttendeeDto> Handle(AddAttendeeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Adding attendee {FamilyMemberId} to event {EventId}",
            request.FamilyMemberId,
            request.EventId);

        var attendee = new EventAttendee(_tenantContext.TenantId, request.EventId, request.FamilyMemberId, request.Notes);

        _context.EventAttendees.Add(attendee);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Added attendee {AttendeeId} to event {EventId}",
            attendee.AttendeeId,
            request.EventId);

        return attendee.ToDto();
    }
}
