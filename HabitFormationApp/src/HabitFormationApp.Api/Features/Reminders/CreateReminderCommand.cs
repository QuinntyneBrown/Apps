using HabitFormationApp.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HabitFormationApp.Api.Features.Reminders;

public record CreateReminderCommand : IRequest<ReminderDto>
{
    public Guid UserId { get; init; }
    public Guid HabitId { get; init; }
    public TimeSpan ReminderTime { get; init; }
    public string? Message { get; init; }
}

public class CreateReminderCommandHandler : IRequestHandler<CreateReminderCommand, ReminderDto>
{
    private readonly IHabitFormationAppContext _context;
    private readonly ILogger<CreateReminderCommandHandler> _logger;

    public CreateReminderCommandHandler(
        IHabitFormationAppContext context,
        ILogger<CreateReminderCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ReminderDto> Handle(CreateReminderCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating reminder for habit {HabitId} at {ReminderTime}",
            request.HabitId,
            request.ReminderTime);

        var reminder = new Reminder
        {
            ReminderId = Guid.NewGuid(),
            UserId = request.UserId,
            HabitId = request.HabitId,
            ReminderTime = request.ReminderTime,
            Message = request.Message,
            IsEnabled = true,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Reminders.Add(reminder);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created reminder {ReminderId} for habit {HabitId}",
            reminder.ReminderId,
            request.HabitId);

        return reminder.ToDto();
    }
}
