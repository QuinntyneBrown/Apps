// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalNetworkCRM.Core;

/// <summary>
/// Represents a follow-up task or reminder for a contact.
/// </summary>
public class FollowUp
{
    /// <summary>
    /// Gets or sets the unique identifier for the follow-up.
    /// </summary>
    public Guid FollowUpId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this follow-up.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the contact ID.
    /// </summary>
    public Guid ContactId { get; set; }

    /// <summary>
    /// Gets or sets the description of the follow-up action.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the due date for the follow-up.
    /// </summary>
    public DateTime DueDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the follow-up is completed.
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// Gets or sets the completion date.
    /// </summary>
    public DateTime? CompletedDate { get; set; }

    /// <summary>
    /// Gets or sets the priority (High, Medium, Low).
    /// </summary>
    public string Priority { get; set; } = "Medium";

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
    /// Gets or sets the navigation property to the contact.
    /// </summary>
    public Contact? Contact { get; set; }

    /// <summary>
    /// Marks the follow-up as completed.
    /// </summary>
    public void Complete()
    {
        IsCompleted = true;
        CompletedDate = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Reschedules the follow-up to a new date.
    /// </summary>
    /// <param name="newDueDate">The new due date.</param>
    public void Reschedule(DateTime newDueDate)
    {
        DueDate = newDueDate;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Checks if the follow-up is overdue.
    /// </summary>
    /// <returns>True if overdue; otherwise, false.</returns>
    public bool IsOverdue()
    {
        return !IsCompleted && DueDate < DateTime.UtcNow;
    }
}
