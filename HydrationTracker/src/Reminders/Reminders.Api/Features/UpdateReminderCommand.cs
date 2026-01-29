using Reminders.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Reminders.Api.Features;

public record UpdateReminderCommand(
    Guid ReminderId,
    TimeSpan Time,
    string Message,
    bool IsActive) : IRequest<ReminderDto?>;

public class UpdateReminderCommandHandler : IRequestHandler<UpdateReminderCommand, ReminderDto?>
{
    private readonly IRemindersDbContext _context;

    public UpdateReminderCommandHandler(IRemindersDbContext context)
    {
        _context = context;
    }

    public async Task<ReminderDto?> Handle(UpdateReminderCommand request, CancellationToken cancellationToken)
    {
        var reminder = await _context.Reminders
            .FirstOrDefaultAsync(r => r.ReminderId == request.ReminderId, cancellationToken);

        if (reminder == null) return null;

        reminder.Time = request.Time;
        reminder.Message = request.Message;
        reminder.IsActive = request.IsActive;

        await _context.SaveChangesAsync(cancellationToken);

        return reminder.ToDto();
    }
}
