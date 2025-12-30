// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MedicationReminderSystem.Core;

/// <summary>
/// Event raised when a medication refill is recorded.
/// </summary>
public record RefillRecordedEvent
{
    /// <summary>
    /// Gets the refill ID.
    /// </summary>
    public Guid RefillId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the medication ID.
    /// </summary>
    public Guid MedicationId { get; init; }

    /// <summary>
    /// Gets the refill date.
    /// </summary>
    public DateTime RefillDate { get; init; }

    /// <summary>
    /// Gets the quantity refilled.
    /// </summary>
    public int Quantity { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
