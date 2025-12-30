using FamilyCalendarEventPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Api.Features.CalendarEvents;

public record DeleteEventCommand : IRequest<bool>
{
    public Guid EventId { get; init; }
}

public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, bool>
{
    private readonly IFamilyCalendarEventPlannerContext _context;
    private readonly ILogger<DeleteEventCommandHandler> _logger;

    public DeleteEventCommandHandler(
        IFamilyCalendarEventPlannerContext context,
        ILogger<DeleteEventCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting calendar event {EventId}", request.EventId);

        var calendarEvent = await _context.CalendarEvents
            .FirstOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken);

        if (calendarEvent == null)
        {
            _logger.LogWarning("Calendar event {EventId} not found", request.EventId);
            return false;
        }

        _context.CalendarEvents.Remove(calendarEvent);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted calendar event {EventId}", request.EventId);

        return true;
    }
}
