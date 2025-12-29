using FamilyCalendarEventPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Api.Features.CalendarEvents;

public record CompleteEventCommand : IRequest<CalendarEventDto?>
{
    public Guid EventId { get; init; }
}

public class CompleteEventCommandHandler : IRequestHandler<CompleteEventCommand, CalendarEventDto?>
{
    private readonly IFamilyCalendarEventPlannerContext _context;
    private readonly ILogger<CompleteEventCommandHandler> _logger;

    public CompleteEventCommandHandler(
        IFamilyCalendarEventPlannerContext context,
        ILogger<CompleteEventCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CalendarEventDto?> Handle(CompleteEventCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Completing calendar event {EventId}", request.EventId);

        var calendarEvent = await _context.CalendarEvents
            .FirstOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken);

        if (calendarEvent == null)
        {
            _logger.LogWarning("Calendar event {EventId} not found", request.EventId);
            return null;
        }

        calendarEvent.Complete();
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Completed calendar event {EventId}", request.EventId);

        return calendarEvent.ToDto();
    }
}
