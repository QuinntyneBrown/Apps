// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MorningRoutineBuilder.Core;

/// <summary>
/// Represents a task within a morning routine.
/// </summary>
public class RoutineTask
{
    /// <summary>
    /// Gets or sets the unique identifier for the task.
    /// </summary>
    public Guid RoutineTaskId { get; set; }

    /// <summary>
    /// Gets or sets the routine ID this task belongs to.
    /// </summary>
    public Guid RoutineId { get; set; }

    /// <summary>
    /// Gets or sets the task name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the task type.
    /// </summary>
    public TaskType TaskType { get; set; }

    /// <summary>
    /// Gets or sets the task description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the estimated duration in minutes.
    /// </summary>
    public int EstimatedDurationMinutes { get; set; }

    /// <summary>
    /// Gets or sets the order/sequence of the task in the routine.
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the task is optional.
    /// </summary>
    public bool IsOptional { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the navigation property to the routine.
    /// </summary>
    public Routine? Routine { get; set; }

    /// <summary>
    /// Checks if the task is required.
    /// </summary>
    /// <returns>True if required; otherwise, false.</returns>
    public bool IsRequired()
    {
        return !IsOptional;
    }
}
