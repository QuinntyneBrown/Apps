// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HabitFormationApp.Core;

public class Habit
{
    public Guid HabitId { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public HabitFrequency Frequency { get; set; }
    public int TargetDaysPerWeek { get; set; } = 7;
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Streak> Streaks { get; set; } = new List<Streak>();
    
    public void ToggleActive()
    {
        IsActive = !IsActive;
    }
}
