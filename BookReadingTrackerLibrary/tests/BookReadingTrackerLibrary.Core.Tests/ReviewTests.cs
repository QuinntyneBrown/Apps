// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BookReadingTrackerLibrary.Core.Tests;

public class ReviewTests
{
    [Test]
    public void Constructor_DefaultValues_SetsPropertiesCorrectly()
    {
        // Arrange & Act
        var review = new Review
        {
            ReviewId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            BookId = Guid.NewGuid(),
            Rating = 4,
            ReviewText = "Great book!",
            IsRecommended = true
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(review.ReviewId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(review.UserId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(review.BookId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(review.Rating, Is.EqualTo(4));
            Assert.That(review.ReviewText, Is.EqualTo("Great book!"));
            Assert.That(review.IsRecommended, Is.True);
            Assert.That(review.ReviewDate, Is.Not.EqualTo(default(DateTime)));
            Assert.That(review.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void IsRatingValid_WithValidRating_ReturnsTrue()
    {
        // Arrange
        var review = new Review { Rating = 3 };

        // Act
        var isValid = review.IsRatingValid();

        // Assert
        Assert.That(isValid, Is.True);
    }

    [Test]
    public void IsRatingValid_WithRating1_ReturnsTrue()
    {
        // Arrange
        var review = new Review { Rating = 1 };

        // Act
        var isValid = review.IsRatingValid();

        // Assert
        Assert.That(isValid, Is.True);
    }

    [Test]
    public void IsRatingValid_WithRating5_ReturnsTrue()
    {
        // Arrange
        var review = new Review { Rating = 5 };

        // Act
        var isValid = review.IsRatingValid();

        // Assert
        Assert.That(isValid, Is.True);
    }

    [Test]
    public void IsRatingValid_WithRating0_ReturnsFalse()
    {
        // Arrange
        var review = new Review { Rating = 0 };

        // Act
        var isValid = review.IsRatingValid();

        // Assert
        Assert.That(isValid, Is.False);
    }

    [Test]
    public void IsRatingValid_WithRating6_ReturnsFalse()
    {
        // Arrange
        var review = new Review { Rating = 6 };

        // Act
        var isValid = review.IsRatingValid();

        // Assert
        Assert.That(isValid, Is.False);
    }

    [Test]
    public void IsRatingValid_WithNegativeRating_ReturnsFalse()
    {
        // Arrange
        var review = new Review { Rating = -1 };

        // Act
        var isValid = review.IsRatingValid();

        // Assert
        Assert.That(isValid, Is.False);
    }

    [Test]
    public void Review_WithBook_AssociatesBookCorrectly()
    {
        // Arrange
        var book = new Book
        {
            BookId = Guid.NewGuid(),
            Title = "Test Book"
        };
        var review = new Review
        {
            BookId = book.BookId,
            Book = book
        };

        // Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(review.Book, Is.Not.Null);
            Assert.That(review.Book.BookId, Is.EqualTo(book.BookId));
        });
    }

    [Test]
    public void Review_AllRatingsFrom1To5_AreValid()
    {
        // Arrange & Act & Assert
        for (int rating = 1; rating <= 5; rating++)
        {
            var review = new Review { Rating = rating };
            Assert.That(review.IsRatingValid(), Is.True, $"Rating {rating} should be valid");
        }
    }

    [Test]
    public void Review_CanBeNotRecommended()
    {
        // Arrange & Act
        var review = new Review
        {
            Rating = 2,
            IsRecommended = false
        };

        // Assert
        Assert.That(review.IsRecommended, Is.False);
    }

    [Test]
    public void Review_WithEmptyReviewText_IsAllowed()
    {
        // Arrange & Act
        var review = new Review
        {
            ReviewText = string.Empty
        };

        // Assert
        Assert.That(review.ReviewText, Is.EqualTo(string.Empty));
    }
}
