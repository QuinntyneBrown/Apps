// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalProjectPipeline.Core;

/// <summary>
/// Represents a milestone within a project.
/// </summary>
public class Milestone
{
    /// <summary>
    /// Gets or sets the unique identifier for the milestone.
    /// </summary>
    public Guid MilestoneId { get; set; }

    /// <summary>
    /// Gets or sets the project ID this milestone belongs to.
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    /// Gets or sets the milestone name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the milestone description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the target date.
    /// </summary>
    public DateTime? TargetDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the milestone is achieved.
    /// </summary>
    public bool IsAchieved { get; set; }

    /// <summary>
    /// Gets or sets the achievement date.
    /// </summary>
    public DateTime? AchievementDate { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the navigation property to the project.
    /// </summary>
    public Project? Project { get; set; }

    /// <summary>
    /// Gets or sets the collection of tasks for this milestone.
    /// </summary>
    public ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();

    /// <summary>
    /// Marks the milestone as achieved.
    /// </summary>
    public void Achieve()
    {
        IsAchieved = true;
        AchievementDate = DateTime.UtcNow;
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
