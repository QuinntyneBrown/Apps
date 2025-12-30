// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BookReadingTrackerLibrary.Core;

/// <summary>
/// Represents a reading session log entry.
/// </summary>
public class ReadingLog
{
    /// <summary>
    /// Gets or sets the unique identifier for the reading log.
    /// </summary>
    public Guid ReadingLogId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this reading log.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the book ID associated with this log.
    /// </summary>
    public Guid BookId { get; set; }

    /// <summary>
    /// Gets or sets the book associated with this log.
    /// </summary>
    public Book? Book { get; set; }

    /// <summary>
    /// Gets or sets the start page of the reading session.
    /// </summary>
    public int StartPage { get; set; }

    /// <summary>
    /// Gets or sets the end page of the reading session.
    /// </summary>
    public int EndPage { get; set; }

    /// <summary>
    /// Gets or sets the start time of the reading session.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Gets or sets the end time of the reading session.
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// Gets or sets optional notes about the reading session.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Calculates the duration of the reading session in minutes.
    /// </summary>
    /// <returns>The duration in minutes.</returns>
    public int GetDurationInMinutes()
    {
        if (!EndTime.HasValue) return 0;
        return (int)(EndTime.Value - StartTime).TotalMinutes;
    }

    /// <summary>
    /// Calculates the number of pages read in this session.
    /// </summary>
    /// <returns>The number of pages read.</returns>
    public int GetPagesRead()
    {
        return EndPage - StartPage;
    }
}
