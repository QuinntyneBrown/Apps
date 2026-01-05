// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ConversationStarterApp.Core;

/// <summary>
/// Event raised when a new prompt is created.
/// </summary>
public record PromptCreatedEvent
{
    /// <summary>
    /// Gets the prompt ID.
    /// </summary>
    public Guid PromptId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid? UserId { get; init; }

    /// <summary>
    /// Gets the text.
    /// </summary>
    public string Text { get; init; } = string.Empty;

    /// <summary>
    /// Gets the category.
    /// </summary>
    public Category Category { get; init; }

    /// <summary>
    /// Gets the depth.
    /// </summary>
    public Depth Depth { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
