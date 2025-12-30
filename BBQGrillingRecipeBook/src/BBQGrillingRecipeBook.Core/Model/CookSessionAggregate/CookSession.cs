// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BBQGrillingRecipeBook.Core;

/// <summary>
/// Represents a cooking session using a recipe.
/// </summary>
public class CookSession
{
    /// <summary>
    /// Gets or sets the unique identifier for the cook session.
    /// </summary>
    public Guid CookSessionId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who performed this session.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the recipe ID used in this session.
    /// </summary>
    public Guid RecipeId { get; set; }

    /// <summary>
    /// Gets or sets the recipe used in this session.
    /// </summary>
    public Recipe? Recipe { get; set; }

    /// <summary>
    /// Gets or sets the date of the cooking session.
    /// </summary>
    public DateTime CookDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the actual cooking time in minutes.
    /// </summary>
    public int ActualCookTimeMinutes { get; set; }

    /// <summary>
    /// Gets or sets the temperature used.
    /// </summary>
    public int? TemperatureUsed { get; set; }

    /// <summary>
    /// Gets or sets the rating for this session (1-5 stars).
    /// </summary>
    public int? Rating { get; set; }

    /// <summary>
    /// Gets or sets notes about this session.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets any modifications made to the recipe.
    /// </summary>
    public string? Modifications { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this session was successful.
    /// </summary>
    public bool WasSuccessful { get; set; } = true;

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Validates the rating is within valid range.
    /// </summary>
    /// <returns>True if rating is valid; otherwise, false.</returns>
    public bool IsRatingValid()
    {
        return !Rating.HasValue || (Rating >= 1 && Rating <= 5);
    }
}
