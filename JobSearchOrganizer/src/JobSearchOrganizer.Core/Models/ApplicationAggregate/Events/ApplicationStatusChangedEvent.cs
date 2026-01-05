// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace JobSearchOrganizer.Core;

/// <summary>
/// Event raised when an application status changes.
/// </summary>
public record ApplicationStatusChangedEvent
{
    /// <summary>
    /// Gets the application ID.
    /// </summary>
    public Guid ApplicationId { get; init; }

    /// <summary>
    /// Gets the old status.
    /// </summary>
    public ApplicationStatus OldStatus { get; init; }

    /// <summary>
    /// Gets the new status.
    /// </summary>
    public ApplicationStatus NewStatus { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
