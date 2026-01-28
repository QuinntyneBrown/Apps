using Reminders.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Reminders.Api.Features;

public record DeleteReminderCommand(Guid ReminderId) : IRequest<bool>;

public class DeleteReminderCommandHandler : IRequestHandler<DeleteReminderCommand, bool>
{
    private readonly IRemindersDbContext _context;

    public DeleteReminderCommandHandler(IRemindersDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteReminderCommand request, CancellationToken cancellationToken)
    {
        var reminder = await _context.Reminders
            .FirstOrDefaultAsync(r => r.ReminderId == request.ReminderId, cancellationToken);

        if (reminder == null) return false;

        _context.Reminders.Remove(reminder);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
