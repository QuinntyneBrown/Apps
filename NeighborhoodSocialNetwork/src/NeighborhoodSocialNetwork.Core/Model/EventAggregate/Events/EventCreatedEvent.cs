// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace NeighborhoodSocialNetwork.Core;

/// <summary>
/// Event raised when a new neighborhood event is created.
/// </summary>
public record EventCreatedEvent
{
    /// <summary>
    /// Gets the event ID.
    /// </summary>
    public Guid EventId { get; init; }

    /// <summary>
    /// Gets the neighbor ID who created the event.
    /// </summary>
    public Guid CreatedByNeighborId { get; init; }

    /// <summary>
    /// Gets the title.
    /// </summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// Gets the event date and time.
    /// </summary>
    public DateTime EventDateTime { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
