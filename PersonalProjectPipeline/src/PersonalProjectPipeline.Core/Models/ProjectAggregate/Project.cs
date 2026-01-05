// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalProjectPipeline.Core;

/// <summary>
/// Represents a personal project.
/// </summary>
public class Project
{
    /// <summary>
    /// Gets or sets the unique identifier for the project.
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this project.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the project name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the project description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the project status.
    /// </summary>
    public ProjectStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the project priority.
    /// </summary>
    public ProjectPriority Priority { get; set; }

    /// <summary>
    /// Gets or sets the start date.
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the target completion date.
    /// </summary>
    public DateTime? TargetDate { get; set; }

    /// <summary>
    /// Gets or sets the actual completion date.
    /// </summary>
    public DateTime? CompletionDate { get; set; }

    /// <summary>
    /// Gets or sets optional tags for categorization.
    /// </summary>
    public string? Tags { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the collection of tasks in this project.
    /// </summary>
    public ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();

    /// <summary>
    /// Gets or sets the collection of milestones in this project.
    /// </summary>
    public ICollection<Milestone> Milestones { get; set; } = new List<Milestone>();

    /// <summary>
    /// Starts the project.
    /// </summary>
    public void Start()
    {
        Status = ProjectStatus.InProgress;
        if (StartDate == null)
        {
            StartDate = DateTime.UtcNow;
        }
    }

    /// <summary>
    /// Completes the project.
    /// </summary>
    public void Complete()
    {
        Status = ProjectStatus.Completed;
        CompletionDate = DateTime.UtcNow;
    }

    /// <summary>
    /// Cancels the project.
    /// </summary>
    public void Cancel()
    {
        Status = ProjectStatus.Cancelled;
    }

    /// <summary>
    /// Calculates the progress percentage based on completed tasks.
    /// </summary>
    /// <returns>The progress percentage.</returns>
    public double GetProgressPercentage()
    {
        if (Tasks.Count == 0)
        {
            return 0;
        }

        var completedTasks = Tasks.Count(t => t.IsCompleted);
        return (double)completedTasks / Tasks.Count * 100;
    }
}
