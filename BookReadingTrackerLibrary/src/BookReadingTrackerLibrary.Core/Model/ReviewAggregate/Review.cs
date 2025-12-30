// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BookReadingTrackerLibrary.Core;

/// <summary>
/// Represents a book review.
/// </summary>
public class Review
{
    /// <summary>
    /// Gets or sets the unique identifier for the review.
    /// </summary>
    public Guid ReviewId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who wrote this review.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the book ID associated with this review.
    /// </summary>
    public Guid BookId { get; set; }

    /// <summary>
    /// Gets or sets the book associated with this review.
    /// </summary>
    public Book? Book { get; set; }

    /// <summary>
    /// Gets or sets the rating (1-5 stars).
    /// </summary>
    public int Rating { get; set; }

    /// <summary>
    /// Gets or sets the review text.
    /// </summary>
    public string ReviewText { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the book is recommended.
    /// </summary>
    public bool IsRecommended { get; set; }

    /// <summary>
    /// Gets or sets the date when the review was written.
    /// </summary>
    public DateTime ReviewDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Validates the rating is within valid range.
    /// </summary>
    /// <returns>True if rating is valid; otherwise, false.</returns>
    public bool IsRatingValid()
    {
        return Rating >= 1 && Rating <= 5;
    }
}
