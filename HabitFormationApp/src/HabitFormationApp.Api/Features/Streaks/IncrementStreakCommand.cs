using HabitFormationApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HabitFormationApp.Api.Features.Streaks;

public record IncrementStreakCommand : IRequest<StreakDto?>
{
    public Guid HabitId { get; init; }
}

public class IncrementStreakCommandHandler : IRequestHandler<IncrementStreakCommand, StreakDto?>
{
    private readonly IHabitFormationAppContext _context;
    private readonly ILogger<IncrementStreakCommandHandler> _logger;

    public IncrementStreakCommandHandler(
        IHabitFormationAppContext context,
        ILogger<IncrementStreakCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<StreakDto?> Handle(IncrementStreakCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Incrementing streak for habit {HabitId}", request.HabitId);

        var streak = await _context.Streaks
            .FirstOrDefaultAsync(s => s.HabitId == request.HabitId, cancellationToken);

        if (streak == null)
        {
            _logger.LogWarning("Streak for habit {HabitId} not found", request.HabitId);
            return null;
        }

        streak.IncrementStreak();
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Incremented streak for habit {HabitId}, current streak: {CurrentStreak}",
            request.HabitId,
            streak.CurrentStreak);

        return streak.ToDto();
    }
}
