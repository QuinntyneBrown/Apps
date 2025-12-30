// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConversationStarterApp.Core;

namespace ConversationStarterApp.Api;

/// <summary>
/// Data transfer object for Favorite.
/// </summary>
public record FavoriteDto
{
    /// <summary>
    /// Gets or sets the favorite ID.
    /// </summary>
    public Guid FavoriteId { get; init; }

    /// <summary>
    /// Gets or sets the prompt ID.
    /// </summary>
    public Guid PromptId { get; init; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets or sets optional notes about why this prompt is favorited.
    /// </summary>
    public string? Notes { get; init; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// Gets or sets the prompt details.
    /// </summary>
    public PromptDto? Prompt { get; init; }
}

/// <summary>
/// Extension methods for Favorite.
/// </summary>
public static class FavoriteExtensions
{
    /// <summary>
    /// Converts a Favorite to a DTO.
    /// </summary>
    /// <param name="favorite">The favorite.</param>
    /// <returns>The DTO.</returns>
    public static FavoriteDto ToDto(this Favorite favorite)
    {
        return new FavoriteDto
        {
            FavoriteId = favorite.FavoriteId,
            PromptId = favorite.PromptId,
            UserId = favorite.UserId,
            Notes = favorite.Notes,
            CreatedAt = favorite.CreatedAt,
            Prompt = favorite.Prompt?.ToDto(),
        };
    }
}
