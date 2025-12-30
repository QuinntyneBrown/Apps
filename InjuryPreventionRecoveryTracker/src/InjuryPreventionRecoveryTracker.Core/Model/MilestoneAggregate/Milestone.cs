// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace InjuryPreventionRecoveryTracker.Core;

/// <summary>
/// Represents a recovery milestone for an injury.
/// </summary>
public class Milestone
{
    /// <summary>
    /// Gets or sets the unique identifier for the milestone.
    /// </summary>
    public Guid MilestoneId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this milestone.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the injury ID this milestone belongs to.
    /// </summary>
    public Guid InjuryId { get; set; }

    /// <summary>
    /// Gets or sets the milestone name or title.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the milestone.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the target date to achieve this milestone.
    /// </summary>
    public DateTime? TargetDate { get; set; }

    /// <summary>
    /// Gets or sets the actual achievement date.
    /// </summary>
    public DateTime? AchievedDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the milestone is achieved.
    /// </summary>
    public bool IsAchieved { get; set; }

    /// <summary>
    /// Gets or sets optional notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the associated injury.
    /// </summary>
    public Injury? Injury { get; set; }

    /// <summary>
    /// Marks the milestone as achieved.
    /// </summary>
    public void MarkAsAchieved()
    {
        IsAchieved = true;
        AchievedDate = DateTime.UtcNow;
    }

    /// <summary>
    /// Checks if the milestone is overdue.
    /// </summary>
    /// <returns>True if overdue; otherwise, false.</returns>
    public bool IsOverdue()
    {
        return !IsAchieved && TargetDate.HasValue && TargetDate.Value < DateTime.UtcNow;
    }
}
