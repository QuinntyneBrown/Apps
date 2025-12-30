// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SalaryCompensationTracker.Core;

/// <summary>
/// Event raised when a benefit is added.
/// </summary>
public record BenefitAddedEvent
{
    /// <summary>
    /// Gets the benefit ID.
    /// </summary>
    public Guid BenefitId { get; init; }

    /// <summary>
    /// Gets the benefit name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the category.
    /// </summary>
    public string Category { get; init; } = string.Empty;

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
