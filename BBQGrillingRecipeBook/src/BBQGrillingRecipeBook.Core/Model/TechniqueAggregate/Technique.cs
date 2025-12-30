// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BBQGrillingRecipeBook.Core;

/// <summary>
/// Represents a BBQ grilling technique or tip.
/// </summary>
public class Technique
{
    /// <summary>
    /// Gets or sets the unique identifier for the technique.
    /// </summary>
    public Guid TechniqueId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who created this technique.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the name of the technique.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the technique.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category of the technique.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the difficulty level (1-5, where 5 is most difficult).
    /// </summary>
    public int DifficultyLevel { get; set; } = 1;

    /// <summary>
    /// Gets or sets the step-by-step instructions.
    /// </summary>
    public string Instructions { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets tips and tricks related to this technique.
    /// </summary>
    public string? Tips { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is a favorite technique.
    /// </summary>
    public bool IsFavorite { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Toggles the favorite status of this technique.
    /// </summary>
    public void ToggleFavorite()
    {
        IsFavorite = !IsFavorite;
    }
}
