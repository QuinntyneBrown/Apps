// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace KnowledgeBaseSecondBrain.Core;

/// <summary>
/// Event raised when two notes are linked.
/// </summary>
public record NotesLinkedEvent
{
    /// <summary>
    /// Gets the link ID.
    /// </summary>
    public Guid NoteLinkId { get; init; }

    /// <summary>
    /// Gets the source note ID.
    /// </summary>
    public Guid SourceNoteId { get; init; }

    /// <summary>
    /// Gets the target note ID.
    /// </summary>
    public Guid TargetNoteId { get; init; }

    /// <summary>
    /// Gets the link type.
    /// </summary>
    public string? LinkType { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
