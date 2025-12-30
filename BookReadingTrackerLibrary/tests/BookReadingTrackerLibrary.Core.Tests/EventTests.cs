// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BookReadingTrackerLibrary.Core.Tests;

public class EventTests
{
    [Test]
    public void BookAddedEvent_CreatesEventWithCorrectProperties()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var beforeCreate = DateTime.UtcNow;

        // Act
        var evt = new BookAddedEvent
        {
            BookId = bookId,
            UserId = userId,
            Title = "1984",
            Author = "George Orwell",
            Genre = Genre.Fiction
        };
        var afterCreate = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.BookId, Is.EqualTo(bookId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Title, Is.EqualTo("1984"));
            Assert.That(evt.Author, Is.EqualTo("George Orwell"));
            Assert.That(evt.Genre, Is.EqualTo(Genre.Fiction));
            Assert.That(evt.Timestamp, Is.GreaterThanOrEqualTo(beforeCreate));
            Assert.That(evt.Timestamp, Is.LessThanOrEqualTo(afterCreate));
        });
    }

    [Test]
    public void BookStatusChangedEvent_CreatesEventWithCorrectProperties()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        // Act
        var evt = new BookStatusChangedEvent
        {
            BookId = bookId,
            UserId = userId,
            OldStatus = ReadingStatus.ToRead,
            NewStatus = ReadingStatus.CurrentlyReading
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.BookId, Is.EqualTo(bookId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.OldStatus, Is.EqualTo(ReadingStatus.ToRead));
            Assert.That(evt.NewStatus, Is.EqualTo(ReadingStatus.CurrentlyReading));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void ReadingSessionLoggedEvent_CreatesEventWithCorrectProperties()
    {
        // Arrange
        var logId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var bookId = Guid.NewGuid();

        // Act
        var evt = new ReadingSessionLoggedEvent
        {
            ReadingLogId = logId,
            UserId = userId,
            BookId = bookId,
            StartPage = 1,
            EndPage = 50
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ReadingLogId, Is.EqualTo(logId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.BookId, Is.EqualTo(bookId));
            Assert.That(evt.StartPage, Is.EqualTo(1));
            Assert.That(evt.EndPage, Is.EqualTo(50));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void ReadingLogCompletedEvent_CreatesEventWithCorrectProperties()
    {
        // Arrange
        var logId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var bookId = Guid.NewGuid();

        // Act
        var evt = new ReadingLogCompletedEvent
        {
            ReadingLogId = logId,
            UserId = userId,
            BookId = bookId,
            DurationInMinutes = 90
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ReadingLogId, Is.EqualTo(logId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.BookId, Is.EqualTo(bookId));
            Assert.That(evt.DurationInMinutes, Is.EqualTo(90));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void ReviewCreatedEvent_CreatesEventWithCorrectProperties()
    {
        // Arrange
        var reviewId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var bookId = Guid.NewGuid();

        // Act
        var evt = new ReviewCreatedEvent
        {
            ReviewId = reviewId,
            UserId = userId,
            BookId = bookId,
            Rating = 5
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ReviewId, Is.EqualTo(reviewId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.BookId, Is.EqualTo(bookId));
            Assert.That(evt.Rating, Is.EqualTo(5));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void ReviewUpdatedEvent_CreatesEventWithCorrectProperties()
    {
        // Arrange
        var reviewId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var bookId = Guid.NewGuid();

        // Act
        var evt = new ReviewUpdatedEvent
        {
            ReviewId = reviewId,
            UserId = userId,
            BookId = bookId,
            NewRating = 4
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ReviewId, Is.EqualTo(reviewId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.BookId, Is.EqualTo(bookId));
            Assert.That(evt.NewRating, Is.EqualTo(4));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void WishlistItemAddedEvent_CreatesEventWithCorrectProperties()
    {
        // Arrange
        var wishlistId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        // Act
        var evt = new WishlistItemAddedEvent
        {
            WishlistId = wishlistId,
            UserId = userId,
            Title = "Dune",
            Author = "Frank Herbert",
            Priority = 5
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.WishlistId, Is.EqualTo(wishlistId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Title, Is.EqualTo("Dune"));
            Assert.That(evt.Author, Is.EqualTo("Frank Herbert"));
            Assert.That(evt.Priority, Is.EqualTo(5));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void WishlistItemRemovedEvent_CreatesEventWithCorrectProperties()
    {
        // Arrange
        var wishlistId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        // Act
        var evt = new WishlistItemRemovedEvent
        {
            WishlistId = wishlistId,
            UserId = userId
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.WishlistId, Is.EqualTo(wishlistId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }
}
