using FamilyCalendarEventPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Api.Features.CalendarEvents;

public record GetEventByIdQuery : IRequest<CalendarEventDto?>
{
    public Guid EventId { get; init; }
}

public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, CalendarEventDto?>
{
    private readonly IFamilyCalendarEventPlannerContext _context;
    private readonly ILogger<GetEventByIdQueryHandler> _logger;

    public GetEventByIdQueryHandler(
        IFamilyCalendarEventPlannerContext context,
        ILogger<GetEventByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CalendarEventDto?> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting calendar event {EventId}", request.EventId);

        var calendarEvent = await _context.CalendarEvents
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken);

        return calendarEvent?.ToDto();
    }
}
