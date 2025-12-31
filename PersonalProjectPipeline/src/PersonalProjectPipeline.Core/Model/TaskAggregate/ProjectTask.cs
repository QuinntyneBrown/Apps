// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalProjectPipeline.Core;

/// <summary>
/// Represents a task within a project.
/// </summary>
public class ProjectTask
{
    /// <summary>
    /// Gets or sets the unique identifier for the task.
    /// </summary>
    public Guid ProjectTaskId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the project ID this task belongs to.
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    /// Gets or sets the milestone ID this task belongs to.
    /// </summary>
    public Guid? MilestoneId { get; set; }

    /// <summary>
    /// Gets or sets the task title.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the task description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the due date.
    /// </summary>
    public DateTime? DueDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the task is completed.
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// Gets or sets the completion date.
    /// </summary>
    public DateTime? CompletionDate { get; set; }

    /// <summary>
    /// Gets or sets the estimated hours.
    /// </summary>
    public double? EstimatedHours { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the navigation property to the project.
    /// </summary>
    public Project? Project { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the milestone.
    /// </summary>
    public Milestone? Milestone { get; set; }

    /// <summary>
    /// Marks the task as completed.
    /// </summary>
    public void Complete()
    {
        IsCompleted = true;
        CompletionDate = DateTime.UtcNow;
    }
}
