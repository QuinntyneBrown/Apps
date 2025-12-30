// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MealPrepPlanner.Core;

/// <summary>
/// Event raised when nutrition information is calculated.
/// </summary>
public record NutritionCalculatedEvent
{
    /// <summary>
    /// Gets the nutrition ID.
    /// </summary>
    public Guid NutritionId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

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
