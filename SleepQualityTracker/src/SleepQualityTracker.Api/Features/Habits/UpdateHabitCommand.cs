using SleepQualityTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SleepQualityTracker.Api.Features.Habits;

public record UpdateHabitCommand : IRequest<HabitDto?>
{
    public Guid HabitId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string HabitType { get; init; } = string.Empty;
    public bool IsPositive { get; init; }
    public TimeSpan? TypicalTime { get; init; }
    public int ImpactLevel { get; init; }
    public bool IsActive { get; init; }
}

public class UpdateHabitCommandHandler : IRequestHandler<UpdateHabitCommand, HabitDto?>
{
    private readonly ISleepQualityTrackerContext _context;
    private readonly ILogger<UpdateHabitCommandHandler> _logger;

    public UpdateHabitCommandHandler(
        ISleepQualityTrackerContext context,
        ILogger<UpdateHabitCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<HabitDto?> Handle(UpdateHabitCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating habit {HabitId}", request.HabitId);

        var habit = await _context.Habits
            .FirstOrDefaultAsync(h => h.HabitId == request.HabitId, cancellationToken);

        if (habit == null)
        {
            _logger.LogWarning("Habit {HabitId} not found", request.HabitId);
            return null;
        }

        habit.Name = request.Name;
        habit.Description = request.Description;
        habit.HabitType = request.HabitType;
        habit.IsPositive = request.IsPositive;
        habit.TypicalTime = request.TypicalTime;
        habit.ImpactLevel = request.ImpactLevel;
        habit.IsActive = request.IsActive;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated habit {HabitId}", request.HabitId);

        return habit.ToDto();
    }
}
