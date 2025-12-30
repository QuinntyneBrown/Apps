// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeBrewingTracker.Core;

/// <summary>
/// Represents a home brewing recipe.
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
    /// Gets or sets the beer style.
    /// </summary>
    public BeerStyle BeerStyle { get; set; }

    /// <summary>
    /// Gets or sets the description of the recipe.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the target original gravity.
    /// </summary>
    public decimal? OriginalGravity { get; set; }

    /// <summary>
    /// Gets or sets the target final gravity.
    /// </summary>
    public decimal? FinalGravity { get; set; }

    /// <summary>
    /// Gets or sets the target ABV percentage.
    /// </summary>
    public decimal? ABV { get; set; }

    /// <summary>
    /// Gets or sets the target IBU (International Bitterness Units).
    /// </summary>
    public int? IBU { get; set; }

    /// <summary>
    /// Gets or sets the batch size in gallons.
    /// </summary>
    public decimal BatchSize { get; set; } = 5.0m;

    /// <summary>
    /// Gets or sets the ingredients list.
    /// </summary>
    public string Ingredients { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the brewing instructions.
    /// </summary>
    public string Instructions { get; set; } = string.Empty;

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
    /// Gets or sets the collection of batches using this recipe.
    /// </summary>
    public ICollection<Batch> Batches { get; set; } = new List<Batch>();

    /// <summary>
    /// Toggles the favorite status of this recipe.
    /// </summary>
    public void ToggleFavorite()
    {
        IsFavorite = !IsFavorite;
    }
}
