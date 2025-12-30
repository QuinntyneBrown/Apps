// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConversationStarterApp.Core;

namespace ConversationStarterApp.Api;

/// <summary>
/// Data transfer object for Prompt.
/// </summary>
public record PromptDto
{
    /// <summary>
    /// Gets or sets the prompt ID.
    /// </summary>
    public Guid PromptId { get; init; }

    /// <summary>
    /// Gets or sets the user ID (null for system prompts).
    /// </summary>
    public Guid? UserId { get; init; }

    /// <summary>
    /// Gets or sets the text of the prompt.
    /// </summary>
    public string Text { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the category of the prompt.
    /// </summary>
    public Category Category { get; init; }

    /// <summary>
    /// Gets or sets the depth level of the prompt.
    /// </summary>
    public Depth Depth { get; init; }

    /// <summary>
    /// Gets or sets tags associated with this prompt.
    /// </summary>
    public string? Tags { get; init; }

    /// <summary>
    /// Gets or sets a value indicating whether this is a system-provided prompt.
    /// </summary>
    public bool IsSystemPrompt { get; init; }

    /// <summary>
    /// Gets or sets the number of times this prompt has been used.
    /// </summary>
    public int UsageCount { get; init; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; init; }
}

/// <summary>
/// Extension methods for Prompt.
/// </summary>
public static class PromptExtensions
{
    /// <summary>
    /// Converts a Prompt to a DTO.
    /// </summary>
    /// <param name="prompt">The prompt.</param>
    /// <returns>The DTO.</returns>
    public static PromptDto ToDto(this Prompt prompt)
    {
        return new PromptDto
        {
            PromptId = prompt.PromptId,
            UserId = prompt.UserId,
            Text = prompt.Text,
            Category = prompt.Category,
            Depth = prompt.Depth,
            Tags = prompt.Tags,
            IsSystemPrompt = prompt.IsSystemPrompt,
            UsageCount = prompt.UsageCount,
            CreatedAt = prompt.CreatedAt,
            UpdatedAt = prompt.UpdatedAt,
        };
    }
}
