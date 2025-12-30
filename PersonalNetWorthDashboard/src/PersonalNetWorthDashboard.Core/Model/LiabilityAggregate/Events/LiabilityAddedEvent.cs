// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalNetWorthDashboard.Core;

/// <summary>
/// Event raised when a new liability is added.
/// </summary>
public record LiabilityAddedEvent
{
    /// <summary>
    /// Gets the liability ID.
    /// </summary>
    public Guid LiabilityId { get; init; }

    /// <summary>
    /// Gets the liability name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the liability type.
    /// </summary>
    public LiabilityType LiabilityType { get; init; }

    /// <summary>
    /// Gets the initial balance.
    /// </summary>
    public decimal CurrentBalance { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
