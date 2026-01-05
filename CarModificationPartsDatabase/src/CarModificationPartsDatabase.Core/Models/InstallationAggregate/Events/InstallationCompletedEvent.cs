// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CarModificationPartsDatabase.Core;

/// <summary>
/// Event raised when an installation is completed.
/// </summary>
public record InstallationCompletedEvent
{
    /// <summary>
    /// Gets the installation ID.
    /// </summary>
    public Guid InstallationId { get; init; }

    /// <summary>
    /// Gets the modification ID.
    /// </summary>
    public Guid ModificationId { get; init; }

    /// <summary>
    /// Gets the total cost.
    /// </summary>
    public decimal TotalCost { get; init; }

    /// <summary>
    /// Gets the satisfaction rating.
    /// </summary>
    public int? SatisfactionRating { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
