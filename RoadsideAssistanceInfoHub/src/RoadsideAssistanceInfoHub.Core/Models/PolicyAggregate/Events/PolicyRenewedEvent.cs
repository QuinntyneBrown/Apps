// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RoadsideAssistanceInfoHub.Core;

/// <summary>
/// Event raised when a policy is renewed.
/// </summary>
public record PolicyRenewedEvent
{
    /// <summary>
    /// Gets the policy ID.
    /// </summary>
    public Guid PolicyId { get; init; }

    /// <summary>
    /// Gets the vehicle ID.
    /// </summary>
    public Guid VehicleId { get; init; }

    /// <summary>
    /// Gets the new end date.
    /// </summary>
    public DateTime NewEndDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
