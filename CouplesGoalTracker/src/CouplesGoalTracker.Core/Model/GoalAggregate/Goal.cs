// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CouplesGoalTracker.Core;

/// <summary>
/// Represents a relationship goal for couples.
/// </summary>
public class Goal
{
    /// <summary>
    /// Gets or sets the unique identifier for the goal.
    /// </summary>
    public Guid GoalId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who created this goal.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the title of the goal.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the goal.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category of the goal.
    /// </summary>
    public GoalCategory Category { get; set; }

    /// <summary>
    /// Gets or sets the status of the goal.
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
    /// Gets or sets the priority level (1-5, with 5 being highest).
    /// </summary>
    public int Priority { get; set; } = 3;

    /// <summary>
    /// Gets or sets a value indicating whether this goal is shared with partner.
    /// </summary>
    public bool IsShared { get; set; } = true;

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the collection of milestones for this goal.
    /// </summary>
    public ICollection<Milestone> Milestones { get; set; } = new List<Milestone>();

    /// <summary>
    /// Gets or sets the collection of progress entries for this goal.
    /// </summary>
    public ICollection<Progress> Progresses { get; set; } = new List<Progress>();

    /// <summary>
    /// Marks the goal as completed.
    /// </summary>
    public void MarkAsCompleted()
    {
        Status = GoalStatus.Completed;
        CompletedDate = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Marks the goal as in progress.
    /// </summary>
    public void MarkAsInProgress()
    {
        Status = GoalStatus.InProgress;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Calculates the completion percentage based on milestones.
    /// </summary>
    /// <returns>The completion percentage (0-100).</returns>
    public double CalculateCompletionPercentage()
    {
        if (Milestones == null || !Milestones.Any())
        {
            return 0;
        }

        var completedCount = Milestones.Count(m => m.IsCompleted);
        return (double)completedCount / Milestones.Count * 100;
    }
}
