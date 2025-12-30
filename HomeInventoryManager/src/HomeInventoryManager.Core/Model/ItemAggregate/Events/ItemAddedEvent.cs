// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeInventoryManager.Core;

/// <summary>
/// Event raised when an item is added to the inventory.
/// </summary>
public record ItemAddedEvent
{
    /// <summary>
    /// Gets the item ID.
    /// </summary>
    public Guid ItemId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the item name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the category.
    /// </summary>
    public Category Category { get; init; }

    /// <summary>
    /// Gets the room.
    /// </summary>
    public Room Room { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
