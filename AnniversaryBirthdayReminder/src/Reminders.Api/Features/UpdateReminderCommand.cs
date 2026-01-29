using Reminders.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Reminders.Api.Features;

public record UpdateReminderCommand(
    Guid ReminderId,
    Guid? CelebrationId,
    string Title,
    string Message,
    DateTime ScheduledDate,
    bool IsActive) : IRequest<ReminderDto?>;

public class UpdateReminderCommandHandler : IRequestHandler<UpdateReminderCommand, ReminderDto?>
{
    private readonly IRemindersDbContext _context;
    private readonly ILogger<UpdateReminderCommandHandler> _logger;

    public UpdateReminderCommandHandler(IRemindersDbContext context, ILogger<UpdateReminderCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ReminderDto?> Handle(UpdateReminderCommand request, CancellationToken cancellationToken)
    {
        var reminder = await _context.Reminders
            .FirstOrDefaultAsync(r => r.ReminderId == request.ReminderId, cancellationToken);

        if (reminder == null) return null;

        reminder.CelebrationId = request.CelebrationId;
        reminder.Title = request.Title;
        reminder.Message = request.Message;
        reminder.ScheduledDate = request.ScheduledDate;
        reminder.IsActive = request.IsActive;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Reminder updated: {ReminderId}", reminder.ReminderId);

        return reminder.ToDto();
    }
}
