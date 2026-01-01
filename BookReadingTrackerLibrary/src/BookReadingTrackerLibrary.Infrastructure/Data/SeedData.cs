// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BookReadingTrackerLibrary.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using BookReadingTrackerLibrary.Core.Model.UserAggregate;
using BookReadingTrackerLibrary.Core.Model.UserAggregate.Entities;
using BookReadingTrackerLibrary.Core.Services;
namespace BookReadingTrackerLibrary.Infrastructure;

/// <summary>
/// Provides seed data for the BookReadingTrackerLibrary database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(BookReadingTrackerLibraryContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Books.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedBooksAsync(context);
                logger.LogInformation("Initial data seeded successfully.");
            }
            else
            {
                logger.LogInformation("Database already contains data. Skipping seed.");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private static async Task SeedBooksAsync(BookReadingTrackerLibraryContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var books = new List<Book>
        {
            new Book
            {
                BookId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Title = "The Pragmatic Programmer",
                Author = "David Thomas and Andrew Hunt",
                ISBN = "9780135957059",
                Genre = Genre.Technology,
                Status = ReadingStatus.Completed,
                TotalPages = 352,
                CurrentPage = 352,
                StartDate = DateTime.UtcNow.AddMonths(-2),
                FinishDate = DateTime.UtcNow.AddMonths(-1),
                CreatedAt = DateTime.UtcNow.AddMonths(-2),
            },
            new Book
            {
                BookId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Title = "Clean Code",
                Author = "Robert C. Martin",
                ISBN = "9780132350884",
                Genre = Genre.Technology,
                Status = ReadingStatus.CurrentlyReading,
                TotalPages = 464,
                CurrentPage = 250,
                StartDate = DateTime.UtcNow.AddDays(-15),
                CreatedAt = DateTime.UtcNow.AddDays(-15),
            },
            new Book
            {
                BookId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Title = "To Kill a Mockingbird",
                Author = "Harper Lee",
                ISBN = "9780061120084",
                Genre = Genre.Fiction,
                Status = ReadingStatus.WantToRead,
                TotalPages = 324,
                CurrentPage = 0,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Books.AddRange(books);

        // Add sample reading logs
        var readingLogs = new List<ReadingLog>
        {
            new ReadingLog
            {
                ReadingLogId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                BookId = books[1].BookId,
                StartPage = 200,
                EndPage = 250,
                StartTime = DateTime.UtcNow.AddDays(-1).AddHours(-2),
                EndTime = DateTime.UtcNow.AddDays(-1),
                Notes = "Great chapter on naming conventions",
                CreatedAt = DateTime.UtcNow.AddDays(-1),
            },
        };

        context.ReadingLogs.AddRange(readingLogs);

        // Add sample reviews
        var reviews = new List<Review>
        {
            new Review
            {
                ReviewId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                BookId = books[0].BookId,
                Rating = 5,
                ReviewText = "An essential read for any software developer. Timeless advice that remains relevant.",
                IsRecommended = true,
                ReviewDate = DateTime.UtcNow.AddMonths(-1),
                CreatedAt = DateTime.UtcNow.AddMonths(-1),
            },
        };

        context.Reviews.AddRange(reviews);

        // Add sample wishlist items
        var wishlists = new List<Wishlist>
        {
            new Wishlist
            {
                WishlistId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Title = "Design Patterns",
                Author = "Erich Gamma, Richard Helm, Ralph Johnson, John Vlissides",
                ISBN = "9780201633610",
                Genre = Genre.Technology,
                Priority = 5,
                Notes = "Recommended by several colleagues",
                IsAcquired = false,
                CreatedAt = DateTime.UtcNow,
            },
            new Wishlist
            {
                WishlistId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Title = "1984",
                Author = "George Orwell",
                ISBN = "9780451524935",
                Genre = Genre.Fiction,
                Priority = 4,
                Notes = "Classic dystopian novel",
                IsAcquired = false,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Wishlists.AddRange(wishlists);

        await context.SaveChangesAsync();
    }
}
