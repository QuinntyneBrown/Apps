// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ConversationStarterApp.Core;

/// <summary>
/// Event raised when a prompt is favorited.
/// </summary>
public record FavoriteAddedEvent
{
    /// <summary>
    /// Gets the favorite ID.
    /// </summary>
    public Guid FavoriteId { get; init; }

    /// <summary>
    /// Gets the prompt ID.
    /// </summary>
    public Guid PromptId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
