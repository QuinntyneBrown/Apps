using Reminders.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Reminders.Api.Features;

public record GetRemindersQuery : IRequest<IEnumerable<ReminderDto>>;

public class GetRemindersQueryHandler : IRequestHandler<GetRemindersQuery, IEnumerable<ReminderDto>>
{
    private readonly IRemindersDbContext _context;

    public GetRemindersQueryHandler(IRemindersDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ReminderDto>> Handle(GetRemindersQuery request, CancellationToken cancellationToken)
    {
        var reminders = await _context.Reminders
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return reminders.Select(r => r.ToDto());
    }
}
