using HabitFormationApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HabitFormationApp.Api.Features.Streaks;

public record GetStreakByHabitIdQuery : IRequest<StreakDto?>
{
    public Guid HabitId { get; init; }
}

public class GetStreakByHabitIdQueryHandler : IRequestHandler<GetStreakByHabitIdQuery, StreakDto?>
{
    private readonly IHabitFormationAppContext _context;
    private readonly ILogger<GetStreakByHabitIdQueryHandler> _logger;

    public GetStreakByHabitIdQueryHandler(
        IHabitFormationAppContext context,
        ILogger<GetStreakByHabitIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<StreakDto?> Handle(GetStreakByHabitIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting streak for habit {HabitId}", request.HabitId);

        var streak = await _context.Streaks
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.HabitId == request.HabitId, cancellationToken);

        return streak?.ToDto();
    }
}
