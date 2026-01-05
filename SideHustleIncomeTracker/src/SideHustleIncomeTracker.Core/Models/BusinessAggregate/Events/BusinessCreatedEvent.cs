// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SideHustleIncomeTracker.Core;

/// <summary>
/// Event raised when a business is created.
/// </summary>
public record BusinessCreatedEvent
{
    /// <summary>
    /// Gets the business ID.
    /// </summary>
    public Guid BusinessId { get; init; }

    /// <summary>
    /// Gets the business name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
