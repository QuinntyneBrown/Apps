// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalReadingList.Core;

/// <summary>
/// Event raised when reading a resource is completed.
/// </summary>
public record ReadingCompletedEvent
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
    /// Gets the completion date.
    /// </summary>
    public DateTime CompletionDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
