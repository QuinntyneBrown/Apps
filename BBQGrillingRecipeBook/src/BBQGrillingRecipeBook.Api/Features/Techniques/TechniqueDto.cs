// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BBQGrillingRecipeBook.Core;

namespace BBQGrillingRecipeBook.Api.Features.Techniques;

/// <summary>
/// Data transfer object for Technique.
/// </summary>
public class TechniqueDto
{
    /// <summary>
    /// Gets or sets the technique ID.
    /// </summary>
    public Guid TechniqueId { get; set; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the technique name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the technique description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the difficulty level.
    /// </summary>
    public int DifficultyLevel { get; set; }

    /// <summary>
    /// Gets or sets the instructions.
    /// </summary>
    public string Instructions { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the tips.
    /// </summary>
    public string? Tips { get; set; }

    /// <summary>
    /// Gets or sets whether the technique is a favorite.
    /// </summary>
    public bool IsFavorite { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Maps a Technique entity to TechniqueDto.
    /// </summary>
    public static TechniqueDto FromEntity(Technique technique)
    {
        return new TechniqueDto
        {
            TechniqueId = technique.TechniqueId,
            UserId = technique.UserId,
            Name = technique.Name,
            Description = technique.Description,
            Category = technique.Category,
            DifficultyLevel = technique.DifficultyLevel,
            Instructions = technique.Instructions,
            Tips = technique.Tips,
            IsFavorite = technique.IsFavorite,
            CreatedAt = technique.CreatedAt
        };
    }
}
