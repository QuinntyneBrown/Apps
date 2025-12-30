// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BookReadingTrackerLibrary.Core;

/// <summary>
/// Event raised when a new book is added to the library.
/// </summary>
public record BookAddedEvent
{
    /// <summary>
    /// Gets the book ID.
    /// </summary>
    public Guid BookId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the book title.
    /// </summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// Gets the book author.
    /// </summary>
    public string Author { get; init; } = string.Empty;

    /// <summary>
    /// Gets the book genre.
    /// </summary>
    public Genre Genre { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
