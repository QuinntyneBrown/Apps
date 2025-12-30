// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeMaintenanceSchedule.Core;

/// <summary>
/// Represents a maintenance task for a home.
/// </summary>
public class MaintenanceTask
{
    /// <summary>
    /// Gets or sets the unique identifier for the maintenance task.
    /// </summary>
    public Guid MaintenanceTaskId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this task.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the name of the task.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the task.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the type of maintenance.
    /// </summary>
    public MaintenanceType MaintenanceType { get; set; }

    /// <summary>
    /// Gets or sets the status of the task.
    /// </summary>
    public TaskStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the due date for the task.
    /// </summary>
    public DateTime? DueDate { get; set; }

    /// <summary>
    /// Gets or sets the completion date of the task.
    /// </summary>
    public DateTime? CompletedDate { get; set; }

    /// <summary>
    /// Gets or sets the frequency in days for recurring tasks.
    /// </summary>
    public int? RecurrenceFrequencyDays { get; set; }

    /// <summary>
    /// Gets or sets the estimated cost.
    /// </summary>
    public decimal? EstimatedCost { get; set; }

    /// <summary>
    /// Gets or sets the actual cost.
    /// </summary>
    public decimal? ActualCost { get; set; }

    /// <summary>
    /// Gets or sets the priority level (1-5, with 1 being highest).
    /// </summary>
    public int Priority { get; set; } = 3;

    /// <summary>
    /// Gets or sets the location or area of the home.
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Gets or sets the contractor ID if assigned.
    /// </summary>
    public Guid? ContractorId { get; set; }

    /// <summary>
    /// Gets or sets the contractor associated with this task.
    /// </summary>
    public Contractor? Contractor { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the collection of service logs for this task.
    /// </summary>
    public ICollection<ServiceLog> ServiceLogs { get; set; } = new List<ServiceLog>();

    /// <summary>
    /// Marks the task as completed.
    /// </summary>
    public void Complete()
    {
        Status = TaskStatus.Completed;
        CompletedDate = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Checks if the task is overdue.
    /// </summary>
    /// <returns>True if the task is overdue; otherwise, false.</returns>
    public bool IsOverdue()
    {
        return DueDate.HasValue && DueDate.Value < DateTime.UtcNow && Status != TaskStatus.Completed;
    }

    /// <summary>
    /// Schedules the next occurrence for recurring tasks.
    /// </summary>
    /// <returns>The next due date.</returns>
    public DateTime? ScheduleNextOccurrence()
    {
        if (RecurrenceFrequencyDays.HasValue && CompletedDate.HasValue)
        {
            DueDate = CompletedDate.Value.AddDays(RecurrenceFrequencyDays.Value);
            Status = TaskStatus.Scheduled;
            UpdatedAt = DateTime.UtcNow;
            return DueDate;
        }

        return null;
    }
}
