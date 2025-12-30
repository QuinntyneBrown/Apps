// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace LetterToFutureSelf.Core;

/// <summary>
/// Represents a delivery schedule for a letter.
/// </summary>
public class DeliverySchedule
{
    /// <summary>
    /// Gets or sets the unique identifier for the delivery schedule.
    /// </summary>
    public Guid DeliveryScheduleId { get; set; }

    /// <summary>
    /// Gets or sets the letter ID this schedule belongs to.
    /// </summary>
    public Guid LetterId { get; set; }

    /// <summary>
    /// Gets or sets the scheduled delivery date and time.
    /// </summary>
    public DateTime ScheduledDateTime { get; set; }

    /// <summary>
    /// Gets or sets the delivery method (e.g., "Email", "SMS", "In-App").
    /// </summary>
    public string DeliveryMethod { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the recipient email or contact.
    /// </summary>
    public string? RecipientContact { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this schedule is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the letter this schedule belongs to.
    /// </summary>
    public Letter? Letter { get; set; }

    /// <summary>
    /// Deactivates the delivery schedule.
    /// </summary>
    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }
}
