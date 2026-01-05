// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MarriageEnrichmentJournal.Core;

/// <summary>
/// Event raised when a new reflection is created.
/// </summary>
public record ReflectionCreatedEvent
{
    /// <summary>
    /// Gets the reflection ID.
    /// </summary>
    public Guid ReflectionId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the text.
    /// </summary>
    public string Text { get; init; } = string.Empty;

    /// <summary>
    /// Gets the topic.
    /// </summary>
    public string? Topic { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
