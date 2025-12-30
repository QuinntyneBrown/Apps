// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace LetterToFutureSelf.Core;

/// <summary>
/// Represents a letter to future self.
/// </summary>
public class Letter
{
    /// <summary>
    /// Gets or sets the unique identifier for the letter.
    /// </summary>
    public Guid LetterId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who wrote this letter.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the subject of the letter.
    /// </summary>
    public string Subject { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the content of the letter.
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date the letter was written.
    /// </summary>
    public DateTime WrittenDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the scheduled delivery date.
    /// </summary>
    public DateTime ScheduledDeliveryDate { get; set; }

    /// <summary>
    /// Gets or sets the actual delivery date.
    /// </summary>
    public DateTime? ActualDeliveryDate { get; set; }

    /// <summary>
    /// Gets or sets the delivery status.
    /// </summary>
    public DeliveryStatus DeliveryStatus { get; set; } = DeliveryStatus.Pending;

    /// <summary>
    /// Gets or sets a value indicating whether the letter has been read.
    /// </summary>
    public bool HasBeenRead { get; set; }

    /// <summary>
    /// Gets or sets the date the letter was read.
    /// </summary>
    public DateTime? ReadDate { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the collection of delivery schedules for this letter.
    /// </summary>
    public ICollection<DeliverySchedule> DeliverySchedules { get; set; } = new List<DeliverySchedule>();

    /// <summary>
    /// Marks the letter as delivered.
    /// </summary>
    public void MarkAsDelivered()
    {
        DeliveryStatus = DeliveryStatus.Delivered;
        ActualDeliveryDate = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Marks the letter as read.
    /// </summary>
    public void MarkAsRead()
    {
        HasBeenRead = true;
        ReadDate = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Checks if the letter is due for delivery.
    /// </summary>
    /// <returns>True if due for delivery; otherwise, false.</returns>
    public bool IsDueForDelivery()
    {
        return DeliveryStatus == DeliveryStatus.Pending && ScheduledDeliveryDate <= DateTime.UtcNow;
    }
}
