// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace NutritionLabelScanner.Core;

/// <summary>
/// Event raised when nutrition information is analyzed.
/// </summary>
public record NutritionAnalyzedEvent
{
    /// <summary>
    /// Gets the nutrition info ID.
    /// </summary>
    public Guid NutritionInfoId { get; init; }

    /// <summary>
    /// Gets the product ID.
    /// </summary>
    public Guid ProductId { get; init; }

    /// <summary>
    /// Gets the calories.
    /// </summary>
    public int Calories { get; init; }

    /// <summary>
    /// Gets the protein in grams.
    /// </summary>
    public decimal Protein { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
