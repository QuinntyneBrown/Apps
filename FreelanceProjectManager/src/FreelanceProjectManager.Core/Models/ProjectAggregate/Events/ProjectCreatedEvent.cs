// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FreelanceProjectManager.Core;

/// <summary>
/// Event raised when a new project is created.
/// </summary>
public record ProjectCreatedEvent
{
    /// <summary>
    /// Gets the project ID.
    /// </summary>
    public Guid ProjectId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the client ID.
    /// </summary>
    public Guid ClientId { get; init; }

    /// <summary>
    /// Gets the project name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
