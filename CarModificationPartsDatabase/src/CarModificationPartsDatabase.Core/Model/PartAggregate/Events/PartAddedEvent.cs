// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CarModificationPartsDatabase.Core;

/// <summary>
/// Event raised when a new part is added to the database.
/// </summary>
public record PartAddedEvent
{
    /// <summary>
    /// Gets the part ID.
    /// </summary>
    public Guid PartId { get; init; }

    /// <summary>
    /// Gets the part name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the manufacturer.
    /// </summary>
    public string Manufacturer { get; init; } = string.Empty;

    /// <summary>
    /// Gets the part number.
    /// </summary>
    public string? PartNumber { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
