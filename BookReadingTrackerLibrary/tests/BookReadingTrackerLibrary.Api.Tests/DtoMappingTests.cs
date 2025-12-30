using BookReadingTrackerLibrary.Api.Features.Books;
using BookReadingTrackerLibrary.Api.Features.ReadingLogs;
using BookReadingTrackerLibrary.Api.Features.Wishlists;
using BookReadingTrackerLibrary.Api.Features.Reviews;

namespace BookReadingTrackerLibrary.Api.Tests;

[TestFixture]
public class DtoMappingTests
{
    [Test]
    public void BookDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var book = new Core.Book
        {
            BookId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "The Great Gatsby",
            Author = "F. Scott Fitzgerald",
            ISBN = "978-0743273565",
            Genre = Core.Genre.Fiction,
            Status = Core.ReadingStatus.CurrentlyReading,
            TotalPages = 180,
            CurrentPage = 90,
            StartDate = DateTime.UtcNow.AddDays(-7),
            FinishDate = null,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = book.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.BookId, Is.EqualTo(book.BookId));
            Assert.That(dto.UserId, Is.EqualTo(book.UserId));
            Assert.That(dto.Title, Is.EqualTo(book.Title));
            Assert.That(dto.Author, Is.EqualTo(book.Author));
            Assert.That(dto.ISBN, Is.EqualTo(book.ISBN));
            Assert.That(dto.Genre, Is.EqualTo(book.Genre));
            Assert.That(dto.Status, Is.EqualTo(book.Status));
            Assert.That(dto.TotalPages, Is.EqualTo(book.TotalPages));
            Assert.That(dto.CurrentPage, Is.EqualTo(book.CurrentPage));
            Assert.That(dto.StartDate, Is.EqualTo(book.StartDate));
            Assert.That(dto.FinishDate, Is.EqualTo(book.FinishDate));
            Assert.That(dto.CreatedAt, Is.EqualTo(book.CreatedAt));
            Assert.That(dto.ProgressPercentage, Is.EqualTo(book.GetProgressPercentage()));
        });
    }

    [Test]
    public void ReadingLogDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var readingLog = new Core.ReadingLog
        {
            ReadingLogId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            BookId = Guid.NewGuid(),
            StartPage = 1,
            EndPage = 50,
            StartTime = DateTime.UtcNow.AddHours(-2),
            EndTime = DateTime.UtcNow,
            Notes = "Great first chapter!",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = readingLog.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.ReadingLogId, Is.EqualTo(readingLog.ReadingLogId));
            Assert.That(dto.UserId, Is.EqualTo(readingLog.UserId));
            Assert.That(dto.BookId, Is.EqualTo(readingLog.BookId));
            Assert.That(dto.StartPage, Is.EqualTo(readingLog.StartPage));
            Assert.That(dto.EndPage, Is.EqualTo(readingLog.EndPage));
            Assert.That(dto.StartTime, Is.EqualTo(readingLog.StartTime));
            Assert.That(dto.EndTime, Is.EqualTo(readingLog.EndTime));
            Assert.That(dto.Notes, Is.EqualTo(readingLog.Notes));
            Assert.That(dto.CreatedAt, Is.EqualTo(readingLog.CreatedAt));
            Assert.That(dto.PagesRead, Is.EqualTo(readingLog.GetPagesRead()));
            Assert.That(dto.DurationInMinutes, Is.EqualTo(readingLog.GetDurationInMinutes()));
        });
    }

    [Test]
    public void WishlistDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var wishlist = new Core.Wishlist
        {
            WishlistId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "1984",
            Author = "George Orwell",
            ISBN = "978-0451524935",
            Genre = Core.Genre.Fiction,
            Priority = 5,
            Notes = "Highly recommended by a friend",
            IsAcquired = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = wishlist.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.WishlistId, Is.EqualTo(wishlist.WishlistId));
            Assert.That(dto.UserId, Is.EqualTo(wishlist.UserId));
            Assert.That(dto.Title, Is.EqualTo(wishlist.Title));
            Assert.That(dto.Author, Is.EqualTo(wishlist.Author));
            Assert.That(dto.ISBN, Is.EqualTo(wishlist.ISBN));
            Assert.That(dto.Genre, Is.EqualTo(wishlist.Genre));
            Assert.That(dto.Priority, Is.EqualTo(wishlist.Priority));
            Assert.That(dto.Notes, Is.EqualTo(wishlist.Notes));
            Assert.That(dto.IsAcquired, Is.EqualTo(wishlist.IsAcquired));
            Assert.That(dto.CreatedAt, Is.EqualTo(wishlist.CreatedAt));
        });
    }

    [Test]
    public void ReviewDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var review = new Core.Review
        {
            ReviewId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            BookId = Guid.NewGuid(),
            Rating = 5,
            ReviewText = "An absolute masterpiece! Highly recommended.",
            IsRecommended = true,
            ReviewDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = review.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.ReviewId, Is.EqualTo(review.ReviewId));
            Assert.That(dto.UserId, Is.EqualTo(review.UserId));
            Assert.That(dto.BookId, Is.EqualTo(review.BookId));
            Assert.That(dto.Rating, Is.EqualTo(review.Rating));
            Assert.That(dto.ReviewText, Is.EqualTo(review.ReviewText));
            Assert.That(dto.IsRecommended, Is.EqualTo(review.IsRecommended));
            Assert.That(dto.ReviewDate, Is.EqualTo(review.ReviewDate));
            Assert.That(dto.CreatedAt, Is.EqualTo(review.CreatedAt));
        });
    }
}
