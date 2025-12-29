using FamilyCalendarEventPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Api.Features.Reminders;

public record GetRemindersQuery : IRequest<IEnumerable<EventReminderDto>>
{
    public Guid? EventId { get; init; }
    public Guid? RecipientId { get; init; }
}

public class GetRemindersQueryHandler : IRequestHandler<GetRemindersQuery, IEnumerable<EventReminderDto>>
{
    private readonly IFamilyCalendarEventPlannerContext _context;
    private readonly ILogger<GetRemindersQueryHandler> _logger;

    public GetRemindersQueryHandler(
        IFamilyCalendarEventPlannerContext context,
        ILogger<GetRemindersQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<EventReminderDto>> Handle(GetRemindersQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting reminders for event {EventId}, recipient {RecipientId}",
            request.EventId,
            request.RecipientId);

        var query = _context.EventReminders.AsNoTracking();

        if (request.EventId.HasValue)
        {
            query = query.Where(r => r.EventId == request.EventId.Value);
        }

        if (request.RecipientId.HasValue)
        {
            query = query.Where(r => r.RecipientId == request.RecipientId.Value);
        }

        var reminders = await query
            .OrderBy(r => r.ReminderTime)
            .ToListAsync(cancellationToken);

        return reminders.Select(r => r.ToDto());
    }
}
