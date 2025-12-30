using SleepQualityTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SleepQualityTracker.Api.Features.Habits;

public record CreateHabitCommand : IRequest<HabitDto>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string HabitType { get; init; } = string.Empty;
    public bool IsPositive { get; init; }
    public TimeSpan? TypicalTime { get; init; }
    public int ImpactLevel { get; init; }
    public bool IsActive { get; init; } = true;
}

public class CreateHabitCommandHandler : IRequestHandler<CreateHabitCommand, HabitDto>
{
    private readonly ISleepQualityTrackerContext _context;
    private readonly ILogger<CreateHabitCommandHandler> _logger;

    public CreateHabitCommandHandler(
        ISleepQualityTrackerContext context,
        ILogger<CreateHabitCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<HabitDto> Handle(CreateHabitCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating habit for user {UserId}, type: {HabitType}",
            request.UserId,
            request.HabitType);

        var habit = new Habit
        {
            HabitId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            Description = request.Description,
            HabitType = request.HabitType,
            IsPositive = request.IsPositive,
            TypicalTime = request.TypicalTime,
            ImpactLevel = request.ImpactLevel,
            IsActive = request.IsActive,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Habits.Add(habit);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created habit {HabitId} for user {UserId}",
            habit.HabitId,
            request.UserId);

        return habit.ToDto();
    }
}
