// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RecipeManagerMealPlanner.Core;

/// <summary>
/// Event raised when an ingredient is added to a recipe.
/// </summary>
public record IngredientAddedEvent
{
    /// <summary>
    /// Gets the ingredient ID.
    /// </summary>
    public Guid IngredientId { get; init; }

    /// <summary>
    /// Gets the recipe ID.
    /// </summary>
    public Guid RecipeId { get; init; }

    /// <summary>
    /// Gets the ingredient name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
