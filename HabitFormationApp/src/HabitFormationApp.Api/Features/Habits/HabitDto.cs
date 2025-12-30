using HabitFormationApp.Core;

namespace HabitFormationApp.Api.Features.Habits;

public record HabitDto
{
    public Guid HabitId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public HabitFrequency Frequency { get; init; }
    public int TargetDaysPerWeek { get; init; }
    public DateTime StartDate { get; init; }
    public bool IsActive { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
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
            Frequency = habit.Frequency,
            TargetDaysPerWeek = habit.TargetDaysPerWeek,
            StartDate = habit.StartDate,
            IsActive = habit.IsActive,
            Notes = habit.Notes,
            CreatedAt = habit.CreatedAt,
        };
    }
}
