// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MarriageEnrichmentJournal.Core;

/// <summary>
/// Represents a reflection entry.
/// </summary>
public class Reflection
{
    /// <summary>
    /// Gets or sets the unique identifier for the reflection.
    /// </summary>
    public Guid ReflectionId { get; set; }

    /// <summary>
    /// Gets or sets the journal entry ID this reflection belongs to.
    /// </summary>
    public Guid? JournalEntryId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who created this reflection.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the reflection text.
    /// </summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the topic or focus of the reflection.
    /// </summary>
    public string? Topic { get; set; }

    /// <summary>
    /// Gets or sets the date of the reflection.
    /// </summary>
    public DateTime ReflectionDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the journal entry this reflection belongs to.
    /// </summary>
    public JournalEntry? JournalEntry { get; set; }
}
