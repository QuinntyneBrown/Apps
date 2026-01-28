using Reminders.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Reminders.Api.Features;

public record GetReminderByIdQuery(Guid ReminderId) : IRequest<ReminderDto?>;

public class GetReminderByIdQueryHandler : IRequestHandler<GetReminderByIdQuery, ReminderDto?>
{
    private readonly IRemindersDbContext _context;

    public GetReminderByIdQueryHandler(IRemindersDbContext context)
    {
        _context = context;
    }

    public async Task<ReminderDto?> Handle(GetReminderByIdQuery request, CancellationToken cancellationToken)
    {
        var reminder = await _context.Reminders
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.ReminderId == request.ReminderId, cancellationToken);

        return reminder?.ToDto();
    }
}
