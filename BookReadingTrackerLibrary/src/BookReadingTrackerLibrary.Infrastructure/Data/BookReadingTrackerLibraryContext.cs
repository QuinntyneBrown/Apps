// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BookReadingTrackerLibrary.Core;
using Microsoft.EntityFrameworkCore;

namespace BookReadingTrackerLibrary.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the BookReadingTrackerLibrary system.
/// </summary>
public class BookReadingTrackerLibraryContext : DbContext, IBookReadingTrackerLibraryContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BookReadingTrackerLibraryContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public BookReadingTrackerLibraryContext(DbContextOptions<BookReadingTrackerLibraryContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Book> Books { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<ReadingLog> ReadingLogs { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Wishlist> Wishlists { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Review> Reviews { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookReadingTrackerLibraryContext).Assembly);
    }
}
