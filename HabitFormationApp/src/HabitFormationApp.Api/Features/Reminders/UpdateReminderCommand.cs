using HabitFormationApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HabitFormationApp.Api.Features.Reminders;

public record UpdateReminderCommand : IRequest<ReminderDto?>
{
    public Guid ReminderId { get; init; }
    public TimeSpan ReminderTime { get; init; }
    public string? Message { get; init; }
}

public class UpdateReminderCommandHandler : IRequestHandler<UpdateReminderCommand, ReminderDto?>
{
    private readonly IHabitFormationAppContext _context;
    private readonly ILogger<UpdateReminderCommandHandler> _logger;

    public UpdateReminderCommandHandler(
        IHabitFormationAppContext context,
        ILogger<UpdateReminderCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ReminderDto?> Handle(UpdateReminderCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating reminder {ReminderId}", request.ReminderId);

        var reminder = await _context.Reminders
            .FirstOrDefaultAsync(r => r.ReminderId == request.ReminderId, cancellationToken);

        if (reminder == null)
        {
            _logger.LogWarning("Reminder {ReminderId} not found", request.ReminderId);
            return null;
        }

        reminder.ReminderTime = request.ReminderTime;
        reminder.Message = request.Message;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated reminder {ReminderId}", request.ReminderId);

        return reminder.ToDto();
    }
}
