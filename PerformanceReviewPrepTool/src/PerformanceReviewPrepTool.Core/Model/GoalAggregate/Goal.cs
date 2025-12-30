// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PerformanceReviewPrepTool.Core;

/// <summary>
/// Represents a goal for a review period.
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
    /// Gets or sets the review period ID.
    /// </summary>
    public Guid ReviewPeriodId { get; set; }

    /// <summary>
    /// Gets or sets the goal title.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the detailed description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the goal status.
    /// </summary>
    public GoalStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the target completion date.
    /// </summary>
    public DateTime? TargetDate { get; set; }

    /// <summary>
    /// Gets or sets the actual completion date.
    /// </summary>
    public DateTime? CompletedDate { get; set; }

    /// <summary>
    /// Gets or sets the progress percentage (0-100).
    /// </summary>
    public int ProgressPercentage { get; set; }

    /// <summary>
    /// Gets or sets the success metrics or criteria.
    /// </summary>
    public string? SuccessMetrics { get; set; }

    /// <summary>
    /// Gets or sets optional notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the review period.
    /// </summary>
    public ReviewPeriod? ReviewPeriod { get; set; }

    /// <summary>
    /// Updates the progress of the goal.
    /// </summary>
    /// <param name="percentage">The new progress percentage.</param>
    public void UpdateProgress(int percentage)
    {
        ProgressPercentage = Math.Clamp(percentage, 0, 100);
        if (ProgressPercentage == 100 && Status != GoalStatus.Completed)
        {
            Complete();
        }
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Marks the goal as completed.
    /// </summary>
    public void Complete()
    {
        Status = GoalStatus.Completed;
        CompletedDate = DateTime.UtcNow;
        ProgressPercentage = 100;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Updates the status of the goal.
    /// </summary>
    /// <param name="newStatus">The new status.</param>
    public void UpdateStatus(GoalStatus newStatus)
    {
        Status = newStatus;
        if (newStatus == GoalStatus.Completed)
        {
            CompletedDate = DateTime.UtcNow;
            ProgressPercentage = 100;
        }
        UpdatedAt = DateTime.UtcNow;
    }
}
