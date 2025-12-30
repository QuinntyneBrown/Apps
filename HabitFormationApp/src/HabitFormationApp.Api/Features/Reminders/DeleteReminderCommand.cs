using HabitFormationApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HabitFormationApp.Api.Features.Reminders;

public record DeleteReminderCommand : IRequest<bool>
{
    public Guid ReminderId { get; init; }
}

public class DeleteReminderCommandHandler : IRequestHandler<DeleteReminderCommand, bool>
{
    private readonly IHabitFormationAppContext _context;
    private readonly ILogger<DeleteReminderCommandHandler> _logger;

    public DeleteReminderCommandHandler(
        IHabitFormationAppContext context,
        ILogger<DeleteReminderCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteReminderCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting reminder {ReminderId}", request.ReminderId);

        var reminder = await _context.Reminders
            .FirstOrDefaultAsync(r => r.ReminderId == request.ReminderId, cancellationToken);

        if (reminder == null)
        {
            _logger.LogWarning("Reminder {ReminderId} not found", request.ReminderId);
            return false;
        }

        _context.Reminders.Remove(reminder);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted reminder {ReminderId}", request.ReminderId);

        return true;
    }
}
