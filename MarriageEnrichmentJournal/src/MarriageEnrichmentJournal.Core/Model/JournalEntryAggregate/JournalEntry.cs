// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MarriageEnrichmentJournal.Core;

/// <summary>
/// Represents a marriage enrichment journal entry.
/// </summary>
public class JournalEntry
{
    /// <summary>
    /// Gets or sets the unique identifier for the journal entry.
    /// </summary>
    public Guid JournalEntryId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who created this entry.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the title of the entry.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the content of the entry.
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the type of entry.
    /// </summary>
    public EntryType EntryType { get; set; }

    /// <summary>
    /// Gets or sets the entry date.
    /// </summary>
    public DateTime EntryDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets a value indicating whether this entry is shared with partner.
    /// </summary>
    public bool IsSharedWithPartner { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this entry is private.
    /// </summary>
    public bool IsPrivate { get; set; }

    /// <summary>
    /// Gets or sets tags for categorization.
    /// </summary>
    public string? Tags { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the collection of gratitude entries associated with this journal entry.
    /// </summary>
    public ICollection<Gratitude> Gratitudes { get; set; } = new List<Gratitude>();

    /// <summary>
    /// Gets or sets the collection of reflections associated with this journal entry.
    /// </summary>
    public ICollection<Reflection> Reflections { get; set; } = new List<Reflection>();

    /// <summary>
    /// Shares the entry with partner.
    /// </summary>
    public void ShareWithPartner()
    {
        IsSharedWithPartner = true;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Marks the entry as private.
    /// </summary>
    public void MarkAsPrivate()
    {
        IsPrivate = true;
        IsSharedWithPartner = false;
        UpdatedAt = DateTime.UtcNow;
    }
}
