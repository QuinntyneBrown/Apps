// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MorningRoutineBuilder.Core;

/// <summary>
/// Represents a streak of consecutive routine completions.
/// </summary>
public class Streak
{
    /// <summary>
    /// Gets or sets the unique identifier for the streak.
    /// </summary>
    public Guid StreakId { get; set; }

    /// <summary>
    /// Gets or sets the routine ID this streak belongs to.
    /// </summary>
    public Guid RoutineId { get; set; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the current streak count in days.
    /// </summary>
    public int CurrentStreak { get; set; }

    /// <summary>
    /// Gets or sets the longest streak ever achieved.
    /// </summary>
    public int LongestStreak { get; set; }

    /// <summary>
    /// Gets or sets the last completion date.
    /// </summary>
    public DateTime? LastCompletionDate { get; set; }

    /// <summary>
    /// Gets or sets the streak start date.
    /// </summary>
    public DateTime? StreakStartDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the streak is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the navigation property to the routine.
    /// </summary>
    public Routine? Routine { get; set; }

    /// <summary>
    /// Increments the streak for a completion.
    /// </summary>
    /// <param name="completionDate">The completion date.</param>
    public void IncrementStreak(DateTime completionDate)
    {
        if (LastCompletionDate == null)
        {
            CurrentStreak = 1;
            StreakStartDate = completionDate;
        }
        else
        {
            var daysSinceLastCompletion = (completionDate.Date - LastCompletionDate.Value.Date).Days;

            if (daysSinceLastCompletion == 1)
            {
                CurrentStreak++;
            }
            else if (daysSinceLastCompletion > 1)
            {
                CurrentStreak = 1;
                StreakStartDate = completionDate;
            }
        }

        LastCompletionDate = completionDate;

        if (CurrentStreak > LongestStreak)
        {
            LongestStreak = CurrentStreak;
        }

        IsActive = true;
    }

    /// <summary>
    /// Resets the current streak.
    /// </summary>
    public void ResetStreak()
    {
        CurrentStreak = 0;
        StreakStartDate = null;
        IsActive = false;
    }

    /// <summary>
    /// Checks if the streak is broken based on today's date.
    /// </summary>
    /// <returns>True if broken; otherwise, false.</returns>
    public bool IsStreakBroken()
    {
        if (LastCompletionDate == null)
        {
            return false;
        }

        var daysSinceLastCompletion = (DateTime.UtcNow.Date - LastCompletionDate.Value.Date).Days;
        return daysSinceLastCompletion > 1;
    }
}
