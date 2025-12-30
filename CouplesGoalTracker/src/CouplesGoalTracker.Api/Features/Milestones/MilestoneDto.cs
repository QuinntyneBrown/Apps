// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CouplesGoalTracker.Core;

namespace CouplesGoalTracker.Api.Features.Milestones;

/// <summary>
/// Data transfer object for Milestone.
/// </summary>
public class MilestoneDto
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
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this milestone is overdue.
    /// </summary>
    public bool IsOverdue { get; set; }

    /// <summary>
    /// Creates a MilestoneDto from a Milestone entity.
    /// </summary>
    /// <param name="milestone">The milestone entity.</param>
    /// <returns>A MilestoneDto.</returns>
    public static MilestoneDto FromMilestone(Milestone milestone)
    {
        return new MilestoneDto
        {
            MilestoneId = milestone.MilestoneId,
            GoalId = milestone.GoalId,
            UserId = milestone.UserId,
            Title = milestone.Title,
            Description = milestone.Description,
            TargetDate = milestone.TargetDate,
            CompletedDate = milestone.CompletedDate,
            IsCompleted = milestone.IsCompleted,
            SortOrder = milestone.SortOrder,
            CreatedAt = milestone.CreatedAt,
            UpdatedAt = milestone.UpdatedAt,
            IsOverdue = milestone.IsOverdue(),
        };
    }
}
