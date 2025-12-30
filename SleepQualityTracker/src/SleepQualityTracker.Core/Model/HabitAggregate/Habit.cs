// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SleepQualityTracker.Core;

/// <summary>
/// Represents a sleep-related habit or behavior.
/// </summary>
public class Habit
{
    /// <summary>
    /// Gets or sets the unique identifier for the habit.
    /// </summary>
    public Guid HabitId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this habit.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the name of the habit.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the habit.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the habit type (e.g., Caffeine, Exercise, Screen Time).
    /// </summary>
    public string HabitType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the habit is positive or negative for sleep.
    /// </summary>
    public bool IsPositive { get; set; }

    /// <summary>
    /// Gets or sets the time of day the habit typically occurs.
    /// </summary>
    public TimeSpan? TypicalTime { get; set; }

    /// <summary>
    /// Gets or sets the impact level on sleep (1-5).
    /// </summary>
    public int ImpactLevel { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the habit is actively tracked.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Checks if the habit has a high impact on sleep (level 4 or 5).
    /// </summary>
    /// <returns>True if high impact; otherwise, false.</returns>
    public bool IsHighImpact()
    {
        return ImpactLevel >= 4;
    }

    /// <summary>
    /// Toggles the active status of the habit.
    /// </summary>
    public void ToggleActive()
    {
        IsActive = !IsActive;
    }
}
