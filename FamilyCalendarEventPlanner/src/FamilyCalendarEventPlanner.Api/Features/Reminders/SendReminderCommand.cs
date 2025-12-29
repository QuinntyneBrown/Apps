using FamilyCalendarEventPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Api.Features.Reminders;

public record SendReminderCommand : IRequest<EventReminderDto?>
{
    public Guid ReminderId { get; init; }
}

public class SendReminderCommandHandler : IRequestHandler<SendReminderCommand, EventReminderDto?>
{
    private readonly IFamilyCalendarEventPlannerContext _context;
    private readonly ILogger<SendReminderCommandHandler> _logger;

    public SendReminderCommandHandler(
        IFamilyCalendarEventPlannerContext context,
        ILogger<SendReminderCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<EventReminderDto?> Handle(SendReminderCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Marking reminder {ReminderId} as sent", request.ReminderId);

        var reminder = await _context.EventReminders
            .FirstOrDefaultAsync(r => r.ReminderId == request.ReminderId, cancellationToken);

        if (reminder == null)
        {
            _logger.LogWarning("Reminder {ReminderId} not found", request.ReminderId);
            return null;
        }

        reminder.Send();
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Marked reminder {ReminderId} as sent", request.ReminderId);

        return reminder.ToDto();
    }
}
