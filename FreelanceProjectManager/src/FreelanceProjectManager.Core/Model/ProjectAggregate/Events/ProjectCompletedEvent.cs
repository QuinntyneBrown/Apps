// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FreelanceProjectManager.Core;

/// <summary>
/// Event raised when a project is completed.
/// </summary>
public record ProjectCompletedEvent
{
    /// <summary>
    /// Gets the project ID.
    /// </summary>
    public Guid ProjectId { get; init; }

    /// <summary>
    /// Gets the completion date.
    /// </summary>
    public DateTime CompletionDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
