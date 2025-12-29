using FamilyCalendarEventPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Api.Features.Reminders;

public record RescheduleReminderCommand : IRequest<EventReminderDto?>
{
    public Guid ReminderId { get; init; }
    public DateTime ReminderTime { get; init; }
}

public class RescheduleReminderCommandHandler : IRequestHandler<RescheduleReminderCommand, EventReminderDto?>
{
    private readonly IFamilyCalendarEventPlannerContext _context;
    private readonly ILogger<RescheduleReminderCommandHandler> _logger;

    public RescheduleReminderCommandHandler(
        IFamilyCalendarEventPlannerContext context,
        ILogger<RescheduleReminderCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<EventReminderDto?> Handle(RescheduleReminderCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Rescheduling reminder {ReminderId} to {ReminderTime}",
            request.ReminderId,
            request.ReminderTime);

        var reminder = await _context.EventReminders
            .FirstOrDefaultAsync(r => r.ReminderId == request.ReminderId, cancellationToken);

        if (reminder == null)
        {
            _logger.LogWarning("Reminder {ReminderId} not found", request.ReminderId);
            return null;
        }

        reminder.Reschedule(request.ReminderTime);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Rescheduled reminder {ReminderId} to {ReminderTime}",
            request.ReminderId,
            request.ReminderTime);

        return reminder.ToDto();
    }
}
