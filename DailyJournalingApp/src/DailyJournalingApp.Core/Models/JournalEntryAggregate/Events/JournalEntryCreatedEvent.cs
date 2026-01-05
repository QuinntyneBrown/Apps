// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace DailyJournalingApp.Core;

/// <summary>
/// Event raised when a new journal entry is created.
/// </summary>
public record JournalEntryCreatedEvent
{
    /// <summary>
    /// Gets the journal entry ID.
    /// </summary>
    public Guid JournalEntryId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the title.
    /// </summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// Gets the mood.
    /// </summary>
    public Mood Mood { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
