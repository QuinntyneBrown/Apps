// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BBQGrillingRecipeBook.Core;

/// <summary>
/// Represents a BBQ grilling recipe.
/// </summary>
public class Recipe
{
    /// <summary>
    /// Gets or sets the unique identifier for the recipe.
    /// </summary>
    public Guid RecipeId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this recipe.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the name of the recipe.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the recipe.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the meat type for this recipe.
    /// </summary>
    public MeatType MeatType { get; set; }

    /// <summary>
    /// Gets or sets the cooking method.
    /// </summary>
    public CookingMethod CookingMethod { get; set; }

    /// <summary>
    /// Gets or sets the preparation time in minutes.
    /// </summary>
    public int PrepTimeMinutes { get; set; }

    /// <summary>
    /// Gets or sets the cooking time in minutes.
    /// </summary>
    public int CookTimeMinutes { get; set; }

    /// <summary>
    /// Gets or sets the ingredients list.
    /// </summary>
    public string Ingredients { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the cooking instructions.
    /// </summary>
    public string Instructions { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the target temperature in Fahrenheit.
    /// </summary>
    public int? TargetTemperature { get; set; }

    /// <summary>
    /// Gets or sets the number of servings.
    /// </summary>
    public int Servings { get; set; } = 4;

    /// <summary>
    /// Gets or sets optional notes about the recipe.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the recipe is a favorite.
    /// </summary>
    public bool IsFavorite { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the collection of cook sessions using this recipe.
    /// </summary>
    public ICollection<CookSession> CookSessions { get; set; } = new List<CookSession>();

    /// <summary>
    /// Calculates the total time required for the recipe.
    /// </summary>
    /// <returns>The total time in minutes.</returns>
    public int GetTotalTimeMinutes()
    {
        return PrepTimeMinutes + CookTimeMinutes;
    }

    /// <summary>
    /// Toggles the favorite status of this recipe.
    /// </summary>
    public void ToggleFavorite()
    {
        IsFavorite = !IsFavorite;
    }
}
