// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BookReadingTrackerLibrary.Infrastructure.Tests;

/// <summary>
/// Unit tests for the BookReadingTrackerLibraryContext.
/// </summary>
[TestFixture]
public class BookReadingTrackerLibraryContextTests
{
    private BookReadingTrackerLibraryContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<BookReadingTrackerLibraryContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new BookReadingTrackerLibraryContext(options);
    }

    /// <summary>
    /// Tears down the test context.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    /// <summary>
    /// Tests that Books can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Books_CanAddAndRetrieve()
    {
        // Arrange
        var book = new Book
        {
            BookId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Book",
            Author = "Test Author",
            ISBN = "1234567890",
            Genre = Genre.Fiction,
            Status = ReadingStatus.WantToRead,
            TotalPages = 300,
            CurrentPage = 0,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Books.FindAsync(book.BookId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Test Book"));
        Assert.That(retrieved.Author, Is.EqualTo("Test Author"));
        Assert.That(retrieved.Genre, Is.EqualTo(Genre.Fiction));
    }

    /// <summary>
    /// Tests that ReadingLogs can be added and retrieved.
    /// </summary>
    [Test]
    public async Task ReadingLogs_CanAddAndRetrieve()
    {
        // Arrange
        var book = new Book
        {
            BookId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Book",
            Author = "Test Author",
            Genre = Genre.Fiction,
            Status = ReadingStatus.CurrentlyReading,
            TotalPages = 300,
            CurrentPage = 50,
            CreatedAt = DateTime.UtcNow,
        };

        var readingLog = new ReadingLog
        {
            ReadingLogId = Guid.NewGuid(),
            UserId = book.UserId,
            BookId = book.BookId,
            StartPage = 1,
            EndPage = 50,
            StartTime = DateTime.UtcNow.AddHours(-2),
            EndTime = DateTime.UtcNow,
            Notes = "Great start!",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Books.Add(book);
        _context.ReadingLogs.Add(readingLog);
        await _context.SaveChangesAsync();

        var retrieved = await _context.ReadingLogs.FindAsync(readingLog.ReadingLogId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.BookId, Is.EqualTo(book.BookId));
        Assert.That(retrieved.StartPage, Is.EqualTo(1));
        Assert.That(retrieved.EndPage, Is.EqualTo(50));
    }

    /// <summary>
    /// Tests that Reviews can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Reviews_CanAddAndRetrieve()
    {
        // Arrange
        var book = new Book
        {
            BookId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Book",
            Author = "Test Author",
            Genre = Genre.Fiction,
            Status = ReadingStatus.Completed,
            TotalPages = 300,
            CurrentPage = 300,
            CreatedAt = DateTime.UtcNow,
        };

        var review = new Review
        {
            ReviewId = Guid.NewGuid(),
            UserId = book.UserId,
            BookId = book.BookId,
            Rating = 5,
            ReviewText = "Amazing book! Highly recommended.",
            IsRecommended = true,
            ReviewDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Books.Add(book);
        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Reviews.FindAsync(review.ReviewId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Rating, Is.EqualTo(5));
        Assert.That(retrieved.ReviewText, Is.EqualTo("Amazing book! Highly recommended."));
        Assert.That(retrieved.IsRecommended, Is.True);
    }

    /// <summary>
    /// Tests that Wishlists can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Wishlists_CanAddAndRetrieve()
    {
        // Arrange
        var wishlist = new Wishlist
        {
            WishlistId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Wishlist Book",
            Author = "Wishlist Author",
            ISBN = "9876543210",
            Genre = Genre.NonFiction,
            Priority = 5,
            Notes = "Must read!",
            IsAcquired = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Wishlists.Add(wishlist);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Wishlists.FindAsync(wishlist.WishlistId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Wishlist Book"));
        Assert.That(retrieved.Priority, Is.EqualTo(5));
        Assert.That(retrieved.IsAcquired, Is.False);
    }
}
