// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CampingTripPlanner.Core.Tests;

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
            CampsiteId = Guid.NewGuid(),
            Rating = 5,
            ReviewText = "Amazing campsite!"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(review.ReviewId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(review.UserId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(review.CampsiteId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(review.Rating, Is.EqualTo(5));
            Assert.That(review.ReviewText, Is.EqualTo("Amazing campsite!"));
            Assert.That(review.ReviewDate, Is.Not.EqualTo(default(DateTime)));
            Assert.That(review.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Review_WithRating1_StoresRatingCorrectly()
    {
        // Arrange & Act
        var review = new Review
        {
            Rating = 1
        };

        // Assert
        Assert.That(review.Rating, Is.EqualTo(1));
    }

    [Test]
    public void Review_WithRating5_StoresRatingCorrectly()
    {
        // Arrange & Act
        var review = new Review
        {
            Rating = 5
        };

        // Assert
        Assert.That(review.Rating, Is.EqualTo(5));
    }

    [Test]
    public void Review_AllRatingsFrom1To5_CanBeSet()
    {
        // Arrange & Act & Assert
        for (int rating = 1; rating <= 5; rating++)
        {
            var review = new Review { Rating = rating };
            Assert.That(review.Rating, Is.EqualTo(rating));
        }
    }

    [Test]
    public void Review_WithReviewText_StoresTextCorrectly()
    {
        // Arrange & Act
        var review = new Review
        {
            ReviewText = "Great facilities and friendly staff"
        };

        // Assert
        Assert.That(review.ReviewText, Is.EqualTo("Great facilities and friendly staff"));
    }

    [Test]
    public void Review_WithNullReviewText_AllowsNull()
    {
        // Arrange & Act
        var review = new Review
        {
            ReviewText = null
        };

        // Assert
        Assert.That(review.ReviewText, Is.Null);
    }

    [Test]
    public void Review_WithCampsite_AssociatesCampsiteCorrectly()
    {
        // Arrange
        var campsite = new Campsite
        {
            CampsiteId = Guid.NewGuid(),
            Name = "Mountain View"
        };
        var review = new Review
        {
            CampsiteId = campsite.CampsiteId,
            Campsite = campsite
        };

        // Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(review.Campsite, Is.Not.Null);
            Assert.That(review.Campsite.CampsiteId, Is.EqualTo(campsite.CampsiteId));
            Assert.That(review.Campsite.Name, Is.EqualTo("Mountain View"));
        });
    }

    [Test]
    public void Review_WithNullCampsite_AllowsNull()
    {
        // Arrange & Act
        var review = new Review
        {
            CampsiteId = Guid.NewGuid(),
            Campsite = null
        };

        // Assert
        Assert.That(review.Campsite, Is.Null);
    }

    [Test]
    public void Review_WithSpecificReviewDate_StoresDateCorrectly()
    {
        // Arrange
        var reviewDate = new DateTime(2024, 7, 15);

        // Act
        var review = new Review
        {
            ReviewDate = reviewDate
        };

        // Assert
        Assert.That(review.ReviewDate, Is.EqualTo(reviewDate));
    }

    [Test]
    public void Review_DefaultReviewDate_IsUtcNow()
    {
        // Arrange
        var beforeCreate = DateTime.UtcNow;

        // Act
        var review = new Review();
        var afterCreate = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(review.ReviewDate, Is.GreaterThanOrEqualTo(beforeCreate));
            Assert.That(review.ReviewDate, Is.LessThanOrEqualTo(afterCreate));
        });
    }
}
