// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MarriageEnrichmentJournal.Core;

/// <summary>
/// Represents a gratitude entry.
/// </summary>
public class Gratitude
{
    /// <summary>
    /// Gets or sets the unique identifier for the gratitude entry.
    /// </summary>
    public Guid GratitudeId { get; set; }

    /// <summary>
    /// Gets or sets the journal entry ID this gratitude belongs to.
    /// </summary>
    public Guid? JournalEntryId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who created this gratitude.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the gratitude text.
    /// </summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date of the gratitude.
    /// </summary>
    public DateTime GratitudeDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the journal entry this gratitude belongs to.
    /// </summary>
    public JournalEntry? JournalEntry { get; set; }
}
