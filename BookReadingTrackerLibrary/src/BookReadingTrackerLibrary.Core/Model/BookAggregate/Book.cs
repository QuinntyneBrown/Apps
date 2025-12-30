// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BookReadingTrackerLibrary.Core;

/// <summary>
/// Represents a book in the reading tracker library.
/// </summary>
public class Book
{
    /// <summary>
    /// Gets or sets the unique identifier for the book.
    /// </summary>
    public Guid BookId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this book.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the title of the book.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the author of the book.
    /// </summary>
    public string Author { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the ISBN of the book.
    /// </summary>
    public string? ISBN { get; set; }

    /// <summary>
    /// Gets or sets the genre of the book.
    /// </summary>
    public Genre Genre { get; set; }

    /// <summary>
    /// Gets or sets the reading status of the book.
    /// </summary>
    public ReadingStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the total number of pages in the book.
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// Gets or sets the current page number.
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// Gets or sets the date when reading started.
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the date when reading finished.
    /// </summary>
    public DateTime? FinishDate { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the collection of reading logs for this book.
    /// </summary>
    public ICollection<ReadingLog> ReadingLogs { get; set; } = new List<ReadingLog>();

    /// <summary>
    /// Gets or sets the collection of reviews for this book.
    /// </summary>
    public ICollection<Review> Reviews { get; set; } = new List<Review>();

    /// <summary>
    /// Calculates the reading progress percentage.
    /// </summary>
    /// <returns>The progress percentage.</returns>
    public decimal GetProgressPercentage()
    {
        if (TotalPages == 0) return 0;
        return (decimal)CurrentPage / TotalPages * 100;
    }

    /// <summary>
    /// Marks the book as currently reading.
    /// </summary>
    public void StartReading()
    {
        Status = ReadingStatus.CurrentlyReading;
        StartDate = DateTime.UtcNow;
    }

    /// <summary>
    /// Marks the book as completed.
    /// </summary>
    public void CompleteReading()
    {
        Status = ReadingStatus.Completed;
        CurrentPage = TotalPages;
        FinishDate = DateTime.UtcNow;
    }
}
