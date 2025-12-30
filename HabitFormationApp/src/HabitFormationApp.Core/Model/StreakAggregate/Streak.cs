// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HabitFormationApp.Core;

public class Streak
{
    public Guid StreakId { get; set; }
    public Guid HabitId { get; set; }
    public int CurrentStreak { get; set; }
    public int LongestStreak { get; set; }
    public DateTime? LastCompletedDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Habit? Habit { get; set; }
    
    public void IncrementStreak()
    {
        CurrentStreak++;
        if (CurrentStreak > LongestStreak)
        {
            LongestStreak = CurrentStreak;
        }
        LastCompletedDate = DateTime.UtcNow;
    }
}
