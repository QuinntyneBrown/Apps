using FamilyCalendarEventPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Api.Features.Reminders;

public record DeleteReminderCommand : IRequest<bool>
{
    public Guid ReminderId { get; init; }
}

public class DeleteReminderCommandHandler : IRequestHandler<DeleteReminderCommand, bool>
{
    private readonly IFamilyCalendarEventPlannerContext _context;
    private readonly ILogger<DeleteReminderCommandHandler> _logger;

    public DeleteReminderCommandHandler(
        IFamilyCalendarEventPlannerContext context,
        ILogger<DeleteReminderCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteReminderCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting reminder {ReminderId}", request.ReminderId);

        var reminder = await _context.EventReminders
            .FirstOrDefaultAsync(r => r.ReminderId == request.ReminderId, cancellationToken);

        if (reminder == null)
        {
            _logger.LogWarning("Reminder {ReminderId} not found", request.ReminderId);
            return false;
        }

        _context.EventReminders.Remove(reminder);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted reminder {ReminderId}", request.ReminderId);

        return true;
    }
}
