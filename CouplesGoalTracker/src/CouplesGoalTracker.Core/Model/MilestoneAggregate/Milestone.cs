// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CouplesGoalTracker.Core;

/// <summary>
/// Represents a milestone for a goal.
/// </summary>
public class Milestone
{
    /// <summary>
    /// Gets or sets the unique identifier for the milestone.
    /// </summary>
    public Guid MilestoneId { get; set; }

    /// <summary>
    /// Gets or sets the goal ID this milestone belongs to.
    /// </summary>
    public Guid GoalId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who created this milestone.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the title of the milestone.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the milestone.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the target completion date.
    /// </summary>
    public DateTime? TargetDate { get; set; }

    /// <summary>
    /// Gets or sets the actual completion date.
    /// </summary>
    public DateTime? CompletedDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this milestone is completed.
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// Gets or sets the sort order of this milestone.
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the goal this milestone belongs to.
    /// </summary>
    public Goal? Goal { get; set; }

    /// <summary>
    /// Marks the milestone as completed.
    /// </summary>
    public void MarkAsCompleted()
    {
        IsCompleted = true;
        CompletedDate = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Checks if the milestone is overdue.
    /// </summary>
    /// <returns>True if the milestone is overdue; otherwise, false.</returns>
    public bool IsOverdue()
    {
        return !IsCompleted && TargetDate.HasValue && TargetDate.Value < DateTime.UtcNow;
    }
}
