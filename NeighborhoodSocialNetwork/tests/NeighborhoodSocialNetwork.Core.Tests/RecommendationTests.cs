// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace NeighborhoodSocialNetwork.Core.Tests;

public class RecommendationTests
{
    [Test]
    public void Constructor_CreatesRecommendation_WithDefaultValues()
    {
        // Arrange & Act
        var recommendation = new Recommendation();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(recommendation.RecommendationId, Is.EqualTo(Guid.Empty));
            Assert.That(recommendation.NeighborId, Is.EqualTo(Guid.Empty));
            Assert.That(recommendation.Title, Is.EqualTo(string.Empty));
            Assert.That(recommendation.Description, Is.EqualTo(string.Empty));
            Assert.That(recommendation.RecommendationType, Is.EqualTo(RecommendationType.Restaurant));
            Assert.That(recommendation.BusinessName, Is.Null);
            Assert.That(recommendation.Location, Is.Null);
            Assert.That(recommendation.Rating, Is.Null);
            Assert.That(recommendation.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(recommendation.UpdatedAt, Is.Null);
            Assert.That(recommendation.Neighbor, Is.Null);
        });
    }

    [Test]
    public void RecommendationId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var recommendation = new Recommendation();
        var expectedId = Guid.NewGuid();

        // Act
        recommendation.RecommendationId = expectedId;

        // Assert
        Assert.That(recommendation.RecommendationId, Is.EqualTo(expectedId));
    }

    [Test]
    public void NeighborId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var recommendation = new Recommendation();
        var expectedId = Guid.NewGuid();

        // Act
        recommendation.NeighborId = expectedId;

        // Assert
        Assert.That(recommendation.NeighborId, Is.EqualTo(expectedId));
    }

    [Test]
    public void Title_CanBeSet_AndRetrieved()
    {
        // Arrange
        var recommendation = new Recommendation();
        var expectedTitle = "Best Pizza in Town";

        // Act
        recommendation.Title = expectedTitle;

        // Assert
        Assert.That(recommendation.Title, Is.EqualTo(expectedTitle));
    }

    [Test]
    public void Description_CanBeSet_AndRetrieved()
    {
        // Arrange
        var recommendation = new Recommendation();
        var expectedDescription = "Amazing authentic Italian pizza";

        // Act
        recommendation.Description = expectedDescription;

        // Assert
        Assert.That(recommendation.Description, Is.EqualTo(expectedDescription));
    }

    [Test]
    public void RecommendationType_CanBeSet_AndRetrieved()
    {
        // Arrange
        var recommendation = new Recommendation();

        // Act
        recommendation.RecommendationType = RecommendationType.ServiceProvider;

        // Assert
        Assert.That(recommendation.RecommendationType, Is.EqualTo(RecommendationType.ServiceProvider));
    }

    [Test]
    public void BusinessName_CanBeSet_AndRetrieved()
    {
        // Arrange
        var recommendation = new Recommendation();
        var expectedName = "Joe's Pizza";

        // Act
        recommendation.BusinessName = expectedName;

        // Assert
        Assert.That(recommendation.BusinessName, Is.EqualTo(expectedName));
    }

    [Test]
    public void Location_CanBeSet_AndRetrieved()
    {
        // Arrange
        var recommendation = new Recommendation();
        var expectedLocation = "456 Oak Street";

        // Act
        recommendation.Location = expectedLocation;

        // Assert
        Assert.That(recommendation.Location, Is.EqualTo(expectedLocation));
    }

    [Test]
    public void Rating_CanBeSet_AndRetrieved()
    {
        // Arrange
        var recommendation = new Recommendation();

        // Act
        recommendation.Rating = 5;

        // Assert
        Assert.That(recommendation.Rating, Is.EqualTo(5));
    }

    [Test]
    public void Rating_CanBeNull()
    {
        // Arrange
        var recommendation = new Recommendation();

        // Act
        recommendation.Rating = null;

        // Assert
        Assert.That(recommendation.Rating, Is.Null);
    }

    [Test]
    public void CreatedAt_CanBeSet_AndRetrieved()
    {
        // Arrange
        var recommendation = new Recommendation();
        var expectedDate = new DateTime(2024, 1, 1);

        // Act
        recommendation.CreatedAt = expectedDate;

        // Assert
        Assert.That(recommendation.CreatedAt, Is.EqualTo(expectedDate));
    }

    [Test]
    public void UpdatedAt_CanBeSet_AndRetrieved()
    {
        // Arrange
        var recommendation = new Recommendation();
        var expectedDate = new DateTime(2024, 2, 1);

        // Act
        recommendation.UpdatedAt = expectedDate;

        // Assert
        Assert.That(recommendation.UpdatedAt, Is.EqualTo(expectedDate));
    }

    [Test]
    public void Neighbor_CanBeSet_AndRetrieved()
    {
        // Arrange
        var recommendation = new Recommendation();
        var neighbor = new Neighbor { NeighborId = Guid.NewGuid(), Name = "John Doe" };

        // Act
        recommendation.Neighbor = neighbor;

        // Assert
        Assert.That(recommendation.Neighbor, Is.EqualTo(neighbor));
    }

    [Test]
    public void Recommendation_WithAllPropertiesSet_IsValid()
    {
        // Arrange
        var recommendationId = Guid.NewGuid();
        var neighborId = Guid.NewGuid();

        // Act
        var recommendation = new Recommendation
        {
            RecommendationId = recommendationId,
            NeighborId = neighborId,
            Title = "Great Service",
            Description = "Highly recommended",
            RecommendationType = RecommendationType.ServiceProvider,
            BusinessName = "ABC Services",
            Location = "Downtown",
            Rating = 5
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(recommendation.RecommendationId, Is.EqualTo(recommendationId));
            Assert.That(recommendation.NeighborId, Is.EqualTo(neighborId));
            Assert.That(recommendation.Title, Is.EqualTo("Great Service"));
            Assert.That(recommendation.Description, Is.EqualTo("Highly recommended"));
            Assert.That(recommendation.RecommendationType, Is.EqualTo(RecommendationType.ServiceProvider));
            Assert.That(recommendation.BusinessName, Is.EqualTo("ABC Services"));
            Assert.That(recommendation.Location, Is.EqualTo("Downtown"));
            Assert.That(recommendation.Rating, Is.EqualTo(5));
        });
    }
}
