using FamilyCalendarEventPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Api.Features.CalendarEvents;

public record GetEventsQuery : IRequest<IEnumerable<CalendarEventDto>>
{
    public Guid? FamilyId { get; init; }
}

public class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, IEnumerable<CalendarEventDto>>
{
    private readonly IFamilyCalendarEventPlannerContext _context;
    private readonly ILogger<GetEventsQueryHandler> _logger;

    public GetEventsQueryHandler(
        IFamilyCalendarEventPlannerContext context,
        ILogger<GetEventsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<CalendarEventDto>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting calendar events for family {FamilyId}", request.FamilyId);

        var query = _context.CalendarEvents.AsNoTracking();

        if (request.FamilyId.HasValue)
        {
            query = query.Where(e => e.FamilyId == request.FamilyId.Value);
        }

        var events = await query
            .OrderBy(e => e.StartTime)
            .ToListAsync(cancellationToken);

        return events.Select(e => e.ToDto());
    }
}
