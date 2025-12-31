using FamilyCalendarEventPlanner.Core;
using FamilyCalendarEventPlanner.Core.Model.EventAggregate;
using FamilyCalendarEventPlanner.Core.Model.EventAggregate.Enums;
using FamilyCalendarEventPlanner.Core.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Api.Features.CalendarEvents;

public record CreateEventCommand : IRequest<CalendarEventDto>
{
    public Guid FamilyId { get; init; }
    public Guid CreatorId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public string? Location { get; init; }
    public EventType EventType { get; init; }
    public RecurrencePatternDto? RecurrencePattern { get; init; }
}

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, CalendarEventDto>
{
    private readonly IFamilyCalendarEventPlannerContext _context;
    private readonly ITenantContext _tenantContext;
    private readonly ILogger<CreateEventCommandHandler> _logger;

    public CreateEventCommandHandler(
        IFamilyCalendarEventPlannerContext context,
        ITenantContext tenantContext,
        ILogger<CreateEventCommandHandler> logger)
    {
        _context = context;
        _tenantContext = tenantContext;
        _logger = logger;
    }

    public async Task<CalendarEventDto> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating calendar event for family {FamilyId}, title: {Title}",
            request.FamilyId,
            request.Title);

        RecurrencePattern? recurrencePattern = null;
        if (request.RecurrencePattern != null)
        {
            recurrencePattern = new RecurrencePattern(
                request.RecurrencePattern.Frequency,
                request.RecurrencePattern.Interval,
                request.RecurrencePattern.EndDate,
                request.RecurrencePattern.DaysOfWeek);
        }

        var calendarEvent = new CalendarEvent(
            _tenantContext.TenantId,
            request.FamilyId,
            request.CreatorId,
            request.Title,
            request.StartTime,
            request.EndTime,
            request.EventType,
            request.Description,
            request.Location,
            recurrencePattern);

        _context.CalendarEvents.Add(calendarEvent);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created calendar event {EventId} for family {FamilyId}",
            calendarEvent.EventId,
            request.FamilyId);

        return calendarEvent.ToDto();
    }
}
