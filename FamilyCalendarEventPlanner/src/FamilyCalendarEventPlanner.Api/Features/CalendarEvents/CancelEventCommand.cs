using FamilyCalendarEventPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Api.Features.CalendarEvents;

public record CancelEventCommand : IRequest<CalendarEventDto?>
{
    public Guid EventId { get; init; }
}

public class CancelEventCommandHandler : IRequestHandler<CancelEventCommand, CalendarEventDto?>
{
    private readonly IFamilyCalendarEventPlannerContext _context;
    private readonly ILogger<CancelEventCommandHandler> _logger;

    public CancelEventCommandHandler(
        IFamilyCalendarEventPlannerContext context,
        ILogger<CancelEventCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CalendarEventDto?> Handle(CancelEventCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Cancelling calendar event {EventId}", request.EventId);

        var calendarEvent = await _context.CalendarEvents
            .FirstOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken);

        if (calendarEvent == null)
        {
            _logger.LogWarning("Calendar event {EventId} not found", request.EventId);
            return null;
        }

        calendarEvent.Cancel();
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Cancelled calendar event {EventId}", request.EventId);

        return calendarEvent.ToDto();
    }
}
