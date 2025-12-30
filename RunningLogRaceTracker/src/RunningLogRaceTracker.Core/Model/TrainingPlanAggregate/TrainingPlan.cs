// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RunningLogRaceTracker.Core;

/// <summary>
/// Represents a training plan for race preparation.
/// </summary>
public class TrainingPlan
{
    /// <summary>
    /// Gets or sets the unique identifier for the training plan.
    /// </summary>
    public Guid TrainingPlanId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this training plan.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the name of the training plan.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the target race ID.
    /// </summary>
    public Guid? RaceId { get; set; }

    /// <summary>
    /// Gets or sets the start date of the training plan.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date of the training plan.
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Gets or sets the weekly mileage goal.
    /// </summary>
    public decimal? WeeklyMileageGoal { get; set; }

    /// <summary>
    /// Gets or sets the training plan details (JSON format with weekly schedules).
    /// </summary>
    public string? PlanDetails { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the plan is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets optional notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the associated race.
    /// </summary>
    public Race? Race { get; set; }

    /// <summary>
    /// Gets the duration of the training plan in weeks.
    /// </summary>
    /// <returns>The duration in weeks.</returns>
    public int GetDurationInWeeks()
    {
        return (int)((EndDate - StartDate).TotalDays / 7);
    }

    /// <summary>
    /// Checks if the training plan is currently active (current date is within plan dates).
    /// </summary>
    /// <returns>True if currently in progress; otherwise, false.</returns>
    public bool IsInProgress()
    {
        var today = DateTime.UtcNow.Date;
        return IsActive && today >= StartDate.Date && today <= EndDate.Date;
    }
}
