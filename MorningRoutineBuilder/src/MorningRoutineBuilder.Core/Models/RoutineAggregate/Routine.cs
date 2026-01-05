// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MorningRoutineBuilder.Core;

/// <summary>
/// Represents a morning routine.
/// </summary>
public class Routine
{
    /// <summary>
    /// Gets or sets the unique identifier for the routine.
    /// </summary>
    public Guid RoutineId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this routine.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the routine name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the routine description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the target start time.
    /// </summary>
    public TimeSpan TargetStartTime { get; set; }

    /// <summary>
    /// Gets or sets the estimated total duration in minutes.
    /// </summary>
    public int EstimatedDurationMinutes { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the routine is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the collection of tasks in this routine.
    /// </summary>
    public ICollection<RoutineTask> Tasks { get; set; } = new List<RoutineTask>();

    /// <summary>
    /// Gets or sets the collection of completion logs for this routine.
    /// </summary>
    public ICollection<CompletionLog> CompletionLogs { get; set; } = new List<CompletionLog>();

    /// <summary>
    /// Calculates the total tasks in the routine.
    /// </summary>
    /// <returns>The total number of tasks.</returns>
    public int GetTotalTasks()
    {
        return Tasks.Count;
    }

    /// <summary>
    /// Activates the routine.
    /// </summary>
    public void Activate()
    {
        IsActive = true;
    }

    /// <summary>
    /// Deactivates the routine.
    /// </summary>
    public void Deactivate()
    {
        IsActive = false;
    }
}
