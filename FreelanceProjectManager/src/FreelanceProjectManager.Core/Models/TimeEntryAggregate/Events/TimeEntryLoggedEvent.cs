// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FreelanceProjectManager.Core;

/// <summary>
/// Event raised when a time entry is logged.
/// </summary>
public record TimeEntryLoggedEvent
{
    /// <summary>
    /// Gets the time entry ID.
    /// </summary>
    public Guid TimeEntryId { get; init; }

    /// <summary>
    /// Gets the project ID.
    /// </summary>
    public Guid ProjectId { get; init; }

    /// <summary>
    /// Gets the work date.
    /// </summary>
    public DateTime WorkDate { get; init; }

    /// <summary>
    /// Gets the hours worked.
    /// </summary>
    public decimal Hours { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
