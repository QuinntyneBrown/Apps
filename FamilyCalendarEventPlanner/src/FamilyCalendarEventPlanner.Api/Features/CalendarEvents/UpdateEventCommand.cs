using FamilyCalendarEventPlanner.Core;
using FamilyCalendarEventPlanner.Core.Model.EventAggregate;
using FamilyCalendarEventPlanner.Core.Model.EventAggregate.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Api.Features.CalendarEvents;

public record UpdateEventCommand : IRequest<CalendarEventDto?>
{
    public Guid EventId { get; init; }
    public string? Title { get; init; }
    public string? Description { get; init; }
    public DateTime? StartTime { get; init; }
    public DateTime? EndTime { get; init; }
    public string? Location { get; init; }
    public EventType? EventType { get; init; }
    public RecurrencePatternDto? RecurrencePattern { get; init; }
}

public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, CalendarEventDto?>
{
    private readonly IFamilyCalendarEventPlannerContext _context;
    private readonly ILogger<UpdateEventCommandHandler> _logger;

    public UpdateEventCommandHandler(
        IFamilyCalendarEventPlannerContext context,
        ILogger<UpdateEventCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CalendarEventDto?> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating calendar event {EventId}", request.EventId);

        var calendarEvent = await _context.CalendarEvents
            .FirstOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken);

        if (calendarEvent == null)
        {
            _logger.LogWarning("Calendar event {EventId} not found", request.EventId);
            return null;
        }

        RecurrencePattern? recurrencePattern = null;
        if (request.RecurrencePattern != null)
        {
            recurrencePattern = new RecurrencePattern(
                request.RecurrencePattern.Frequency,
                request.RecurrencePattern.Interval,
                request.RecurrencePattern.EndDate,
                request.RecurrencePattern.DaysOfWeek);
        }

        calendarEvent.Modify(
            request.Title,
            request.Description,
            request.StartTime,
            request.EndTime,
            request.Location,
            request.EventType,
            recurrencePattern);

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated calendar event {EventId}", request.EventId);

        return calendarEvent.ToDto();
    }
}
