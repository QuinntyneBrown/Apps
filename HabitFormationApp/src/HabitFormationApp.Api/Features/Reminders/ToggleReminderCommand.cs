using HabitFormationApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HabitFormationApp.Api.Features.Reminders;

public record ToggleReminderCommand : IRequest<ReminderDto?>
{
    public Guid ReminderId { get; init; }
}

public class ToggleReminderCommandHandler : IRequestHandler<ToggleReminderCommand, ReminderDto?>
{
    private readonly IHabitFormationAppContext _context;
    private readonly ILogger<ToggleReminderCommandHandler> _logger;

    public ToggleReminderCommandHandler(
        IHabitFormationAppContext context,
        ILogger<ToggleReminderCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ReminderDto?> Handle(ToggleReminderCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Toggling reminder {ReminderId}", request.ReminderId);

        var reminder = await _context.Reminders
            .FirstOrDefaultAsync(r => r.ReminderId == request.ReminderId, cancellationToken);

        if (reminder == null)
        {
            _logger.LogWarning("Reminder {ReminderId} not found", request.ReminderId);
            return null;
        }

        reminder.Toggle();
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Toggled reminder {ReminderId} to {IsEnabled}",
            request.ReminderId,
            reminder.IsEnabled);

        return reminder.ToDto();
    }
}
