// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BookReadingTrackerLibrary.Core.Tests;

public class ReadingLogTests
{
    [Test]
    public void Constructor_DefaultValues_SetsPropertiesCorrectly()
    {
        // Arrange & Act
        var log = new ReadingLog
        {
            ReadingLogId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            BookId = Guid.NewGuid(),
            StartPage = 1,
            EndPage = 50,
            StartTime = DateTime.UtcNow
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(log.ReadingLogId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(log.UserId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(log.BookId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(log.StartPage, Is.EqualTo(1));
            Assert.That(log.EndPage, Is.EqualTo(50));
            Assert.That(log.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void GetDurationInMinutes_WithEndTime_ReturnsCorrectDuration()
    {
        // Arrange
        var startTime = new DateTime(2024, 1, 1, 10, 0, 0);
        var endTime = new DateTime(2024, 1, 1, 11, 30, 0);
        var log = new ReadingLog
        {
            StartTime = startTime,
            EndTime = endTime
        };

        // Act
        var duration = log.GetDurationInMinutes();

        // Assert
        Assert.That(duration, Is.EqualTo(90));
    }

    [Test]
    public void GetDurationInMinutes_WithoutEndTime_ReturnsZero()
    {
        // Arrange
        var log = new ReadingLog
        {
            StartTime = DateTime.UtcNow,
            EndTime = null
        };

        // Act
        var duration = log.GetDurationInMinutes();

        // Assert
        Assert.That(duration, Is.EqualTo(0));
    }

    [Test]
    public void GetDurationInMinutes_WithShortSession_ReturnsCorrectMinutes()
    {
        // Arrange
        var startTime = new DateTime(2024, 1, 1, 10, 0, 0);
        var endTime = new DateTime(2024, 1, 1, 10, 15, 0);
        var log = new ReadingLog
        {
            StartTime = startTime,
            EndTime = endTime
        };

        // Act
        var duration = log.GetDurationInMinutes();

        // Assert
        Assert.That(duration, Is.EqualTo(15));
    }

    [Test]
    public void GetPagesRead_ReturnsCorrectCount()
    {
        // Arrange
        var log = new ReadingLog
        {
            StartPage = 10,
            EndPage = 60
        };

        // Act
        var pagesRead = log.GetPagesRead();

        // Assert
        Assert.That(pagesRead, Is.EqualTo(50));
    }

    [Test]
    public void GetPagesRead_WithSinglePage_ReturnsZero()
    {
        // Arrange
        var log = new ReadingLog
        {
            StartPage = 100,
            EndPage = 100
        };

        // Act
        var pagesRead = log.GetPagesRead();

        // Assert
        Assert.That(pagesRead, Is.EqualTo(0));
    }

    [Test]
    public void GetPagesRead_WithLargeRange_ReturnsCorrectCount()
    {
        // Arrange
        var log = new ReadingLog
        {
            StartPage = 1,
            EndPage = 500
        };

        // Act
        var pagesRead = log.GetPagesRead();

        // Assert
        Assert.That(pagesRead, Is.EqualTo(499));
    }

    [Test]
    public void ReadingLog_WithNotes_StoresNotesCorrectly()
    {
        // Arrange & Act
        var log = new ReadingLog
        {
            Notes = "This was a great reading session!"
        };

        // Assert
        Assert.That(log.Notes, Is.EqualTo("This was a great reading session!"));
    }

    [Test]
    public void ReadingLog_WithBook_AssociatesBookCorrectly()
    {
        // Arrange
        var book = new Book
        {
            BookId = Guid.NewGuid(),
            Title = "Test Book"
        };
        var log = new ReadingLog
        {
            BookId = book.BookId,
            Book = book
        };

        // Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(log.Book, Is.Not.Null);
            Assert.That(log.Book.BookId, Is.EqualTo(book.BookId));
            Assert.That(log.Book.Title, Is.EqualTo("Test Book"));
        });
    }

    [Test]
    public void ReadingLog_CanStoreNullBook()
    {
        // Arrange & Act
        var log = new ReadingLog
        {
            BookId = Guid.NewGuid(),
            Book = null
        };

        // Assert
        Assert.That(log.Book, Is.Null);
    }
}
