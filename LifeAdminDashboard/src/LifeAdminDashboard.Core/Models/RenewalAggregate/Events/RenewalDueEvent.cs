// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace LifeAdminDashboard.Core;

/// <summary>
/// Event raised when a renewal is due.
/// </summary>
public record RenewalDueEvent
{
    /// <summary>
    /// Gets the renewal ID.
    /// </summary>
    public Guid RenewalId { get; init; }

    /// <summary>
    /// Gets the renewal name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the renewal date.
    /// </summary>
    public DateTime RenewalDate { get; init; }

    /// <summary>
    /// Gets the cost.
    /// </summary>
    public decimal? Cost { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
