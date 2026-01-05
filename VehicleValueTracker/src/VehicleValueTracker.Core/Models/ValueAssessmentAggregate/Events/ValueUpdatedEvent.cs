// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VehicleValueTracker.Core;

/// <summary>
/// Event raised when a vehicle's estimated value is updated.
/// </summary>
public record ValueUpdatedEvent
{
    /// <summary>
    /// Gets the value assessment ID.
    /// </summary>
    public Guid ValueAssessmentId { get; init; }

    /// <summary>
    /// Gets the vehicle ID.
    /// </summary>
    public Guid VehicleId { get; init; }

    /// <summary>
    /// Gets the previous estimated value.
    /// </summary>
    public decimal PreviousValue { get; init; }

    /// <summary>
    /// Gets the new estimated value.
    /// </summary>
    public decimal NewValue { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
