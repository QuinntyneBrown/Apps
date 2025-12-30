using HabitFormationApp.Core;

namespace HabitFormationApp.Api.Features.Streaks;

public record StreakDto
{
    public Guid StreakId { get; init; }
    public Guid HabitId { get; init; }
    public int CurrentStreak { get; init; }
    public int LongestStreak { get; init; }
    public DateTime? LastCompletedDate { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class StreakExtensions
{
    public static StreakDto ToDto(this Streak streak)
    {
        return new StreakDto
        {
            StreakId = streak.StreakId,
            HabitId = streak.HabitId,
            CurrentStreak = streak.CurrentStreak,
            LongestStreak = streak.LongestStreak,
            LastCompletedDate = streak.LastCompletedDate,
            CreatedAt = streak.CreatedAt,
        };
    }
}
