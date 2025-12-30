// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeBrewingTracker.Core;

namespace HomeBrewingTracker.Api.Features.Recipes;

/// <summary>
/// Data transfer object for Recipe.
/// </summary>
public class RecipeDto
{
    /// <summary>
    /// Gets or sets the recipe ID.
    /// </summary>
    public Guid RecipeId { get; set; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the recipe name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the beer style.
    /// </summary>
    public BeerStyle BeerStyle { get; set; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the original gravity.
    /// </summary>
    public decimal? OriginalGravity { get; set; }

    /// <summary>
    /// Gets or sets the final gravity.
    /// </summary>
    public decimal? FinalGravity { get; set; }

    /// <summary>
    /// Gets or sets the ABV.
    /// </summary>
    public decimal? ABV { get; set; }

    /// <summary>
    /// Gets or sets the IBU.
    /// </summary>
    public int? IBU { get; set; }

    /// <summary>
    /// Gets or sets the batch size.
    /// </summary>
    public decimal BatchSize { get; set; }

    /// <summary>
    /// Gets or sets the ingredients.
    /// </summary>
    public string Ingredients { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the instructions.
    /// </summary>
    public string Instructions { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets whether this is a favorite.
    /// </summary>
    public bool IsFavorite { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
