// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalReadingList.Core;

/// <summary>
/// Event raised when reading a resource is started.
/// </summary>
public record ReadingStartedEvent
{
    /// <summary>
    /// Gets the reading progress ID.
    /// </summary>
    public Guid ReadingProgressId { get; init; }

    /// <summary>
    /// Gets the resource ID.
    /// </summary>
    public Guid ResourceId { get; init; }

    /// <summary>
    /// Gets the start date.
    /// </summary>
    public DateTime StartDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
