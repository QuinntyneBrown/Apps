using HabitFormationApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HabitFormationApp.Api.Features.Reminders;

public record GetRemindersQuery : IRequest<IEnumerable<ReminderDto>>
{
    public Guid? UserId { get; init; }
    public Guid? HabitId { get; init; }
    public bool? IsEnabled { get; init; }
}

public class GetRemindersQueryHandler : IRequestHandler<GetRemindersQuery, IEnumerable<ReminderDto>>
{
    private readonly IHabitFormationAppContext _context;
    private readonly ILogger<GetRemindersQueryHandler> _logger;

    public GetRemindersQueryHandler(
        IHabitFormationAppContext context,
        ILogger<GetRemindersQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ReminderDto>> Handle(GetRemindersQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting reminders for user {UserId}", request.UserId);

        var query = _context.Reminders.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(r => r.UserId == request.UserId.Value);
        }

        if (request.HabitId.HasValue)
        {
            query = query.Where(r => r.HabitId == request.HabitId.Value);
        }

        if (request.IsEnabled.HasValue)
        {
            query = query.Where(r => r.IsEnabled == request.IsEnabled.Value);
        }

        var reminders = await query
            .OrderBy(r => r.ReminderTime)
            .ToListAsync(cancellationToken);

        return reminders.Select(r => r.ToDto());
    }
}
