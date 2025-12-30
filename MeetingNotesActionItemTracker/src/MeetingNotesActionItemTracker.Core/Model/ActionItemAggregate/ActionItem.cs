// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MeetingNotesActionItemTracker.Core;

/// <summary>
/// Represents an action item from a meeting.
/// </summary>
public class ActionItem
{
    /// <summary>
    /// Gets or sets the unique identifier for the action item.
    /// </summary>
    public Guid ActionItemId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this action item.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the meeting ID.
    /// </summary>
    public Guid MeetingId { get; set; }

    /// <summary>
    /// Gets or sets the description of the action item.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the person responsible.
    /// </summary>
    public string? ResponsiblePerson { get; set; }

    /// <summary>
    /// Gets or sets the due date.
    /// </summary>
    public DateTime? DueDate { get; set; }

    /// <summary>
    /// Gets or sets the priority.
    /// </summary>
    public Priority Priority { get; set; }

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    public ActionItemStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the completion date.
    /// </summary>
    public DateTime? CompletedDate { get; set; }

    /// <summary>
    /// Gets or sets optional notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the meeting.
    /// </summary>
    public Meeting? Meeting { get; set; }

    /// <summary>
    /// Marks the action item as completed.
    /// </summary>
    public void Complete()
    {
        Status = ActionItemStatus.Completed;
        CompletedDate = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Updates the status of the action item.
    /// </summary>
    /// <param name="newStatus">The new status.</param>
    public void UpdateStatus(ActionItemStatus newStatus)
    {
        Status = newStatus;
        if (newStatus == ActionItemStatus.Completed)
        {
            CompletedDate = DateTime.UtcNow;
        }
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Checks if the action item is overdue.
    /// </summary>
    /// <returns>True if overdue; otherwise, false.</returns>
    public bool IsOverdue()
    {
        return DueDate.HasValue && DueDate.Value < DateTime.UtcNow && Status != ActionItemStatus.Completed;
    }
}
