// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BookReadingTrackerLibrary.Core;

/// <summary>
/// Represents a book wishlist item.
/// </summary>
public class Wishlist
{
    /// <summary>
    /// Gets or sets the unique identifier for the wishlist item.
    /// </summary>
    public Guid WishlistId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this wishlist item.
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
    public Genre? Genre { get; set; }

    /// <summary>
    /// Gets or sets the priority level (1-5, where 5 is highest).
    /// </summary>
    public int Priority { get; set; } = 3;

    /// <summary>
    /// Gets or sets optional notes about why this book is on the wishlist.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the book has been acquired.
    /// </summary>
    public bool IsAcquired { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Marks the wishlist item as acquired.
    /// </summary>
    public void MarkAsAcquired()
    {
        IsAcquired = true;
    }
}
