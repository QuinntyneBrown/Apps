// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CouplesGoalTracker.Core;

namespace CouplesGoalTracker.Api.Features.Goals;

/// <summary>
/// Data transfer object for Goal.
/// </summary>
public class GoalDto
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
    public int Priority { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this goal is shared with partner.
    /// </summary>
    public bool IsShared { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the completion percentage.
    /// </summary>
    public double CompletionPercentage { get; set; }

    /// <summary>
    /// Creates a GoalDto from a Goal entity.
    /// </summary>
    /// <param name="goal">The goal entity.</param>
    /// <returns>A GoalDto.</returns>
    public static GoalDto FromGoal(Goal goal)
    {
        return new GoalDto
        {
            GoalId = goal.GoalId,
            UserId = goal.UserId,
            Title = goal.Title,
            Description = goal.Description,
            Category = goal.Category,
            Status = goal.Status,
            TargetDate = goal.TargetDate,
            CompletedDate = goal.CompletedDate,
            Priority = goal.Priority,
            IsShared = goal.IsShared,
            CreatedAt = goal.CreatedAt,
            UpdatedAt = goal.UpdatedAt,
            CompletionPercentage = goal.CalculateCompletionPercentage(),
        };
    }
}
