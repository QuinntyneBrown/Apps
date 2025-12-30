// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ChoreAssignmentTracker.Core;

/// <summary>
/// Event raised when a chore is created.
/// </summary>
public record ChoreCreatedEvent
{
    /// <summary>
    /// Gets the chore ID.
    /// </summary>
    public Guid ChoreId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the chore name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the frequency.
    /// </summary>
    public ChoreFrequency Frequency { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
