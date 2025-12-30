using HabitFormationApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HabitFormationApp.Api.Features.Reminders;

public record GetReminderByIdQuery : IRequest<ReminderDto?>
{
    public Guid ReminderId { get; init; }
}

public class GetReminderByIdQueryHandler : IRequestHandler<GetReminderByIdQuery, ReminderDto?>
{
    private readonly IHabitFormationAppContext _context;
    private readonly ILogger<GetReminderByIdQueryHandler> _logger;

    public GetReminderByIdQueryHandler(
        IHabitFormationAppContext context,
        ILogger<GetReminderByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ReminderDto?> Handle(GetReminderByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting reminder {ReminderId}", request.ReminderId);

        var reminder = await _context.Reminders
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.ReminderId == request.ReminderId, cancellationToken);

        return reminder?.ToDto();
    }
}
