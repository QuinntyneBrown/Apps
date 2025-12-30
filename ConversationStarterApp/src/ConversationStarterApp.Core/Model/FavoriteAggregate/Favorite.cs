// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ConversationStarterApp.Core;

/// <summary>
/// Represents a favorited prompt.
/// </summary>
public class Favorite
{
    /// <summary>
    /// Gets or sets the unique identifier for the favorite.
    /// </summary>
    public Guid FavoriteId { get; set; }

    /// <summary>
    /// Gets or sets the prompt ID that was favorited.
    /// </summary>
    public Guid PromptId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who favorited the prompt.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets optional notes about why this prompt is favorited.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the prompt this favorite belongs to.
    /// </summary>
    public Prompt? Prompt { get; set; }
}
