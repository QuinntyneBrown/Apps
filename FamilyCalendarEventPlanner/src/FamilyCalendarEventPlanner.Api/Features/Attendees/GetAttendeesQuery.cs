using FamilyCalendarEventPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Api.Features.Attendees;

public record GetAttendeesQuery : IRequest<IEnumerable<EventAttendeeDto>>
{
    public Guid EventId { get; init; }
}

public class GetAttendeesQueryHandler : IRequestHandler<GetAttendeesQuery, IEnumerable<EventAttendeeDto>>
{
    private readonly IFamilyCalendarEventPlannerContext _context;
    private readonly ILogger<GetAttendeesQueryHandler> _logger;

    public GetAttendeesQueryHandler(
        IFamilyCalendarEventPlannerContext context,
        ILogger<GetAttendeesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<EventAttendeeDto>> Handle(GetAttendeesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting attendees for event {EventId}", request.EventId);

        var attendees = await _context.EventAttendees
            .AsNoTracking()
            .Where(a => a.EventId == request.EventId)
            .ToListAsync(cancellationToken);

        return attendees.Select(a => a.ToDto());
    }
}
