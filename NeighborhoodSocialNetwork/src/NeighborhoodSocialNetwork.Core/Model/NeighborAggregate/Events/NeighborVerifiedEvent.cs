// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace NeighborhoodSocialNetwork.Core;

/// <summary>
/// Event raised when a neighbor is verified.
/// </summary>
public record NeighborVerifiedEvent
{
    /// <summary>
    /// Gets the neighbor ID.
    /// </summary>
    public Guid NeighborId { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
