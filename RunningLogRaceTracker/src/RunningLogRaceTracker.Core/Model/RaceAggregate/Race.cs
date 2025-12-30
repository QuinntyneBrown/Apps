// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RunningLogRaceTracker.Core;

/// <summary>
/// Represents a race event.
/// </summary>
public class Race
{
    /// <summary>
    /// Gets or sets the unique identifier for the race.
    /// </summary>
    public Guid RaceId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who participated in this race.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the name of the race.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the race type.
    /// </summary>
    public RaceType RaceType { get; set; }

    /// <summary>
    /// Gets or sets the race date.
    /// </summary>
    public DateTime RaceDate { get; set; }

    /// <summary>
    /// Gets or sets the location of the race.
    /// </summary>
    public string Location { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the distance in kilometers.
    /// </summary>
    public decimal Distance { get; set; }

    /// <summary>
    /// Gets or sets the finish time in minutes.
    /// </summary>
    public int? FinishTimeMinutes { get; set; }

    /// <summary>
    /// Gets or sets the goal time in minutes.
    /// </summary>
    public int? GoalTimeMinutes { get; set; }

    /// <summary>
    /// Gets or sets the placement or rank.
    /// </summary>
    public int? Placement { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the race is completed.
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// Gets or sets optional notes about the race.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Checks if the goal time was achieved.
    /// </summary>
    /// <returns>True if goal was achieved; otherwise, false.</returns>
    public bool AchievedGoal()
    {
        if (!IsCompleted || !FinishTimeMinutes.HasValue || !GoalTimeMinutes.HasValue)
            return false;

        return FinishTimeMinutes.Value <= GoalTimeMinutes.Value;
    }

    /// <summary>
    /// Checks if the race is upcoming (in the future).
    /// </summary>
    /// <returns>True if upcoming; otherwise, false.</returns>
    public bool IsUpcoming()
    {
        return RaceDate > DateTime.UtcNow;
    }
}
