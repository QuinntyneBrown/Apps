// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalMissionStatementBuilder.Core;

/// <summary>
/// Represents a personal goal aligned with the mission statement.
/// </summary>
public class Goal
{
    /// <summary>
    /// Gets or sets the unique identifier for the goal.
    /// </summary>
    public Guid GoalId { get; set; }

    /// <summary>
    /// Gets or sets the mission statement ID this goal belongs to.
    /// </summary>
    public Guid? MissionStatementId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who created this goal.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the title of the goal.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the goal.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the status of the goal.
    /// </summary>
    public GoalStatus Status { get; set; } = GoalStatus.NotStarted;

    /// <summary>
    /// Gets or sets the target completion date.
    /// </summary>
    public DateTime? TargetDate { get; set; }

    /// <summary>
    /// Gets or sets the actual completion date.
    /// </summary>
    public DateTime? CompletedDate { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the mission statement this goal belongs to.
    /// </summary>
    public MissionStatement? MissionStatement { get; set; }

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
    /// Updates the status.
    /// </summary>
    /// <param name="newStatus">The new status.</param>
    public void UpdateStatus(GoalStatus newStatus)
    {
        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
    }
}
