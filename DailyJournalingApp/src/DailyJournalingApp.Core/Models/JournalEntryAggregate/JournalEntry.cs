// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace DailyJournalingApp.Core;

/// <summary>
/// Represents a daily journal entry.
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
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the title of the entry.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the content of the entry.
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the entry date.
    /// </summary>
    public DateTime EntryDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the mood for this entry.
    /// </summary>
    public Mood Mood { get; set; }

    /// <summary>
    /// Gets or sets the prompt ID if this entry was created from a prompt.
    /// </summary>
    public Guid? PromptId { get; set; }

    /// <summary>
    /// Gets or sets tags for categorization (comma-separated).
    /// </summary>
    public string? Tags { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this entry is a favorite.
    /// </summary>
    public bool IsFavorite { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the prompt this entry was created from.
    /// </summary>
    public Prompt? Prompt { get; set; }

    /// <summary>
    /// Marks the entry as favorite.
    /// </summary>
    public void MarkAsFavorite()
    {
        IsFavorite = true;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Updates the mood.
    /// </summary>
    /// <param name="newMood">The new mood.</param>
    public void UpdateMood(Mood newMood)
    {
        Mood = newMood;
        UpdatedAt = DateTime.UtcNow;
    }
}
