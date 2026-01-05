// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeInventoryManager.Core;

/// <summary>
/// Event raised when a value estimate is created.
/// </summary>
public record ValueEstimateCreatedEvent
{
    /// <summary>
    /// Gets the value estimate ID.
    /// </summary>
    public Guid ValueEstimateId { get; init; }

    /// <summary>
    /// Gets the item ID.
    /// </summary>
    public Guid ItemId { get; init; }

    /// <summary>
    /// Gets the estimated value.
    /// </summary>
    public decimal EstimatedValue { get; init; }

    /// <summary>
    /// Gets the estimation date.
    /// </summary>
    public DateTime EstimationDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
