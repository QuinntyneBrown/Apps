// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace DailyJournalingApp.Core;

/// <summary>
/// Represents a journaling prompt.
/// </summary>
public class Prompt
{
    /// <summary>
    /// Gets or sets the unique identifier for the prompt.
    /// </summary>
    public Guid PromptId { get; set; }

    /// <summary>
    /// Gets or sets the text of the prompt.
    /// </summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category or theme of the prompt.
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is a system-provided prompt.
    /// </summary>
    public bool IsSystemPrompt { get; set; } = true;

    /// <summary>
    /// Gets or sets the user ID if this is a user-created prompt.
    /// </summary>
    public Guid? CreatedByUserId { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the collection of journal entries created from this prompt.
    /// </summary>
    public ICollection<JournalEntry> JournalEntries { get; set; } = new List<JournalEntry>();
}
