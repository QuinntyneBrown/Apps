// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SubscriptionAuditTool.Core;

/// <summary>
/// Event raised when a category is created.
/// </summary>
public record CategoryCreatedEvent
{
    /// <summary>
    /// Gets the category ID.
    /// </summary>
    public Guid CategoryId { get; init; }

    /// <summary>
    /// Gets the category name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
