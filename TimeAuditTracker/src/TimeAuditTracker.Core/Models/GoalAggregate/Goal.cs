// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TimeAuditTracker.Core;

/// <summary>
/// Represents a time allocation goal for an activity category.
/// </summary>
public class Goal
{
    /// <summary>
    /// Gets or sets the unique identifier for the goal.
    /// </summary>
    public Guid GoalId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this goal.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the activity category for this goal.
    /// </summary>
    public ActivityCategory Category { get; set; }

    /// <summary>
    /// Gets or sets the target hours per week.
    /// </summary>
    public double TargetHoursPerWeek { get; set; }

    /// <summary>
    /// Gets or sets the minimum hours per week (threshold).
    /// </summary>
    public double? MinimumHoursPerWeek { get; set; }

    /// <summary>
    /// Gets or sets the description of the goal.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the goal is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the start date of the goal.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date of the goal.
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Calculates the target hours per day.
    /// </summary>
    /// <returns>The target hours per day.</returns>
    public double GetTargetHoursPerDay()
    {
        return TargetHoursPerWeek / 7.0;
    }

    /// <summary>
    /// Checks if the actual hours meet the goal.
    /// </summary>
    /// <param name="actualHours">The actual hours spent.</param>
    /// <returns>True if the goal is met; otherwise, false.</returns>
    public bool IsGoalMet(double actualHours)
    {
        return actualHours >= TargetHoursPerWeek;
    }

    /// <summary>
    /// Calculates the progress percentage towards the goal.
    /// </summary>
    /// <param name="actualHours">The actual hours spent.</param>
    /// <returns>The progress percentage.</returns>
    public double GetProgressPercentage(double actualHours)
    {
        if (TargetHoursPerWeek == 0)
        {
            return 0;
        }

        return (actualHours / TargetHoursPerWeek) * 100;
    }

    /// <summary>
    /// Deactivates the goal.
    /// </summary>
    public void Deactivate()
    {
        IsActive = false;
        EndDate = DateTime.UtcNow;
    }
}
