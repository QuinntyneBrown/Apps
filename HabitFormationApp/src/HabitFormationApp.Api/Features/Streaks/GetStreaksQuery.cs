using HabitFormationApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HabitFormationApp.Api.Features.Streaks;

public record GetStreaksQuery : IRequest<IEnumerable<StreakDto>>
{
}

public class GetStreaksQueryHandler : IRequestHandler<GetStreaksQuery, IEnumerable<StreakDto>>
{
    private readonly IHabitFormationAppContext _context;
    private readonly ILogger<GetStreaksQueryHandler> _logger;

    public GetStreaksQueryHandler(
        IHabitFormationAppContext context,
        ILogger<GetStreaksQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<StreakDto>> Handle(GetStreaksQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all streaks");

        var streaks = await _context.Streaks
            .AsNoTracking()
            .OrderByDescending(s => s.CurrentStreak)
            .ToListAsync(cancellationToken);

        return streaks.Select(s => s.ToDto());
    }
}
