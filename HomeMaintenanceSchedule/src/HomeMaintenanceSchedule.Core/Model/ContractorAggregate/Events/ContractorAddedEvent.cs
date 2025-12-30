// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeMaintenanceSchedule.Core;

/// <summary>
/// Event raised when a contractor is added.
/// </summary>
public record ContractorAddedEvent
{
    /// <summary>
    /// Gets the contractor ID.
    /// </summary>
    public Guid ContractorId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the contractor name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the specialty.
    /// </summary>
    public string? Specialty { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
