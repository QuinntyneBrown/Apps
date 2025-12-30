// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace NutritionLabelScanner.Core;

/// <summary>
/// Event raised when a product comparison is created.
/// </summary>
public record ComparisonCreatedEvent
{
    /// <summary>
    /// Gets the comparison ID.
    /// </summary>
    public Guid ComparisonId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the comparison name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
