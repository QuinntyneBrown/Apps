// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace BookReadingTrackerLibrary.Core;

/// <summary>
/// Represents the persistence surface for the BookReadingTrackerLibrary system.
/// </summary>
public interface IBookReadingTrackerLibraryContext
{
    /// <summary>
    /// Gets or sets the DbSet of books.
    /// </summary>
    DbSet<Book> Books { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of reading logs.
    /// </summary>
    DbSet<ReadingLog> ReadingLogs { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of wishlist items.
    /// </summary>
    DbSet<Wishlist> Wishlists { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of reviews.
    /// </summary>
    DbSet<Review> Reviews { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
