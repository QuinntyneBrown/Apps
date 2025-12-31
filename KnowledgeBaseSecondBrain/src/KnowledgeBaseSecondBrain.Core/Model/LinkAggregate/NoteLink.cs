// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace KnowledgeBaseSecondBrain.Core;

/// <summary>
/// Represents a link between two notes.
/// </summary>
public class NoteLink
{
    /// <summary>
    /// Gets or sets the unique identifier for the link.
    /// </summary>
    public Guid NoteLinkId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the source note ID.
    /// </summary>
    public Guid SourceNoteId { get; set; }

    /// <summary>
    /// Gets or sets the target note ID.
    /// </summary>
    public Guid TargetNoteId { get; set; }

    /// <summary>
    /// Gets or sets the link description or context.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the link type or relationship.
    /// </summary>
    public string? LinkType { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the navigation property to the source note.
    /// </summary>
    public Note? SourceNote { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the target note.
    /// </summary>
    public Note? TargetNote { get; set; }
}
