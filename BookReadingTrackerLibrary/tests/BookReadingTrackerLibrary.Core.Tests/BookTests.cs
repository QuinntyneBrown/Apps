// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BookReadingTrackerLibrary.Core.Tests;

public class BookTests
{
    [Test]
    public void Constructor_DefaultValues_SetsPropertiesCorrectly()
    {
        // Arrange & Act
        var book = new Book
        {
            BookId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Book",
            Author = "Test Author",
            Genre = Genre.Fiction,
            Status = ReadingStatus.ToRead,
            TotalPages = 300
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(book.BookId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(book.UserId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(book.Title, Is.EqualTo("Test Book"));
            Assert.That(book.Author, Is.EqualTo("Test Author"));
            Assert.That(book.Genre, Is.EqualTo(Genre.Fiction));
            Assert.That(book.Status, Is.EqualTo(ReadingStatus.ToRead));
            Assert.That(book.TotalPages, Is.EqualTo(300));
            Assert.That(book.CurrentPage, Is.EqualTo(0));
            Assert.That(book.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(book.ReadingLogs, Is.Not.Null);
            Assert.That(book.Reviews, Is.Not.Null);
        });
    }

    [Test]
    public void GetProgressPercentage_WithValidPages_ReturnsCorrectPercentage()
    {
        // Arrange
        var book = new Book
        {
            TotalPages = 400,
            CurrentPage = 100
        };

        // Act
        var progress = book.GetProgressPercentage();

        // Assert
        Assert.That(progress, Is.EqualTo(25m));
    }

    [Test]
    public void GetProgressPercentage_WithZeroTotalPages_ReturnsZero()
    {
        // Arrange
        var book = new Book
        {
            TotalPages = 0,
            CurrentPage = 0
        };

        // Act
        var progress = book.GetProgressPercentage();

        // Assert
        Assert.That(progress, Is.EqualTo(0m));
    }

    [Test]
    public void GetProgressPercentage_WithFullProgress_Returns100()
    {
        // Arrange
        var book = new Book
        {
            TotalPages = 300,
            CurrentPage = 300
        };

        // Act
        var progress = book.GetProgressPercentage();

        // Assert
        Assert.That(progress, Is.EqualTo(100m));
    }

    [Test]
    public void StartReading_SetsStatusAndStartDate()
    {
        // Arrange
        var book = new Book
        {
            Status = ReadingStatus.ToRead
        };
        var beforeStart = DateTime.UtcNow;

        // Act
        book.StartReading();
        var afterStart = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(book.Status, Is.EqualTo(ReadingStatus.CurrentlyReading));
            Assert.That(book.StartDate, Is.Not.Null);
            Assert.That(book.StartDate, Is.GreaterThanOrEqualTo(beforeStart));
            Assert.That(book.StartDate, Is.LessThanOrEqualTo(afterStart));
        });
    }

    [Test]
    public void CompleteReading_SetsStatusCurrentPageAndFinishDate()
    {
        // Arrange
        var book = new Book
        {
            Status = ReadingStatus.CurrentlyReading,
            TotalPages = 500,
            CurrentPage = 450
        };
        var beforeComplete = DateTime.UtcNow;

        // Act
        book.CompleteReading();
        var afterComplete = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(book.Status, Is.EqualTo(ReadingStatus.Completed));
            Assert.That(book.CurrentPage, Is.EqualTo(book.TotalPages));
            Assert.That(book.FinishDate, Is.Not.Null);
            Assert.That(book.FinishDate, Is.GreaterThanOrEqualTo(beforeComplete));
            Assert.That(book.FinishDate, Is.LessThanOrEqualTo(afterComplete));
        });
    }

    [Test]
    public void Book_WithISBN_StoresISBNCorrectly()
    {
        // Arrange & Act
        var book = new Book
        {
            ISBN = "978-3-16-148410-0"
        };

        // Assert
        Assert.That(book.ISBN, Is.EqualTo("978-3-16-148410-0"));
    }

    [Test]
    public void Book_CanAddMultipleReadingLogs()
    {
        // Arrange
        var book = new Book();
        var log1 = new ReadingLog { ReadingLogId = Guid.NewGuid() };
        var log2 = new ReadingLog { ReadingLogId = Guid.NewGuid() };

        // Act
        book.ReadingLogs.Add(log1);
        book.ReadingLogs.Add(log2);

        // Assert
        Assert.That(book.ReadingLogs, Has.Count.EqualTo(2));
    }

    [Test]
    public void Book_CanAddMultipleReviews()
    {
        // Arrange
        var book = new Book();
        var review1 = new Review { ReviewId = Guid.NewGuid() };
        var review2 = new Review { ReviewId = Guid.NewGuid() };

        // Act
        book.Reviews.Add(review1);
        book.Reviews.Add(review2);

        // Assert
        Assert.That(book.Reviews, Has.Count.EqualTo(2));
    }

    [Test]
    public void Book_AllGenresCanBeAssigned()
    {
        // Arrange & Act & Assert
        var genres = Enum.GetValues<Genre>();
        foreach (var genre in genres)
        {
            var book = new Book { Genre = genre };
            Assert.That(book.Genre, Is.EqualTo(genre));
        }
    }

    [Test]
    public void Book_AllStatusesCanBeAssigned()
    {
        // Arrange & Act & Assert
        var statuses = Enum.GetValues<ReadingStatus>();
        foreach (var status in statuses)
        {
            var book = new Book { Status = status };
            Assert.That(book.Status, Is.EqualTo(status));
        }
    }
}
