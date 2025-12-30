using SleepQualityTracker.Core;

namespace SleepQualityTracker.Api.Features.Habits;

public record HabitDto
{
    public Guid HabitId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string HabitType { get; init; } = string.Empty;
    public bool IsPositive { get; init; }
    public TimeSpan? TypicalTime { get; init; }
    public int ImpactLevel { get; init; }
    public bool IsActive { get; init; }
    public DateTime CreatedAt { get; init; }
    public bool IsHighImpact { get; init; }
}

public static class HabitExtensions
{
    public static HabitDto ToDto(this Habit habit)
    {
        return new HabitDto
        {
            HabitId = habit.HabitId,
            UserId = habit.UserId,
            Name = habit.Name,
            Description = habit.Description,
            HabitType = habit.HabitType,
            IsPositive = habit.IsPositive,
            TypicalTime = habit.TypicalTime,
            ImpactLevel = habit.ImpactLevel,
            IsActive = habit.IsActive,
            CreatedAt = habit.CreatedAt,
            IsHighImpact = habit.IsHighImpact(),
        };
    }
}
