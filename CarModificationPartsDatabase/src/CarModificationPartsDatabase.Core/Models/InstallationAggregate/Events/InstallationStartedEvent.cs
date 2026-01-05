// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CarModificationPartsDatabase.Core;

/// <summary>
/// Event raised when a new installation is started.
/// </summary>
public record InstallationStartedEvent
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
    /// Gets the vehicle information.
    /// </summary>
    public string VehicleInfo { get; init; } = string.Empty;

    /// <summary>
    /// Gets the installation date.
    /// </summary>
    public DateTime InstallationDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
