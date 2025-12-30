// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ConversationStarterApp.Core;

/// <summary>
/// Represents a conversation starter prompt.
/// </summary>
public class Prompt
{
    /// <summary>
    /// Gets or sets the unique identifier for the prompt.
    /// </summary>
    public Guid PromptId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who created this prompt (null for system prompts).
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Gets or sets the text of the prompt.
    /// </summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category of the prompt.
    /// </summary>
    public Category Category { get; set; }

    /// <summary>
    /// Gets or sets the depth level of the prompt.
    /// </summary>
    public Depth Depth { get; set; }

    /// <summary>
    /// Gets or sets tags associated with this prompt.
    /// </summary>
    public string? Tags { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is a system-provided prompt.
    /// </summary>
    public bool IsSystemPrompt { get; set; }

    /// <summary>
    /// Gets or sets the number of times this prompt has been used.
    /// </summary>
    public int UsageCount { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the collection of favorites for this prompt.
    /// </summary>
    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    /// <summary>
    /// Increments the usage count for this prompt.
    /// </summary>
    public void IncrementUsageCount()
    {
        UsageCount++;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Checks if this prompt is favorited by a specific user.
    /// </summary>
    /// <param name="userId">The user ID to check.</param>
    /// <returns>True if favorited; otherwise, false.</returns>
    public bool IsFavoritedByUser(Guid userId)
    {
        return Favorites.Any(f => f.UserId == userId);
    }
}
