// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace NeighborhoodSocialNetwork.Core;

/// <summary>
/// Event raised when a new recommendation is created.
/// </summary>
public record RecommendationCreatedEvent
{
    /// <summary>
    /// Gets the recommendation ID.
    /// </summary>
    public Guid RecommendationId { get; init; }

    /// <summary>
    /// Gets the neighbor ID.
    /// </summary>
    public Guid NeighborId { get; init; }

    /// <summary>
    /// Gets the title.
    /// </summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// Gets the recommendation type.
    /// </summary>
    public RecommendationType RecommendationType { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
