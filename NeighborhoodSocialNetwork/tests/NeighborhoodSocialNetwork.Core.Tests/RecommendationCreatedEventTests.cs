// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace NeighborhoodSocialNetwork.Core.Tests;

public class RecommendationCreatedEventTests
{
    [Test]
    public void RecommendationCreatedEvent_CanBeCreated_WithAllProperties()
    {
        // Arrange
        var recommendationId = Guid.NewGuid();
        var neighborId = Guid.NewGuid();
        var title = "Best Pizza";
        var type = RecommendationType.Restaurant;
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new RecommendationCreatedEvent
        {
            RecommendationId = recommendationId,
            NeighborId = neighborId,
            Title = title,
            RecommendationType = type,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.RecommendationId, Is.EqualTo(recommendationId));
            Assert.That(evt.NeighborId, Is.EqualTo(neighborId));
            Assert.That(evt.Title, Is.EqualTo(title));
            Assert.That(evt.RecommendationType, Is.EqualTo(type));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void RecommendationCreatedEvent_DefaultTimestamp_IsNotDefault()
    {
        // Arrange & Act
        var evt = new RecommendationCreatedEvent
        {
            RecommendationId = Guid.NewGuid(),
            NeighborId = Guid.NewGuid(),
            Title = "Test",
            RecommendationType = RecommendationType.Restaurant
        };

        // Assert
        Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
    }

    [Test]
    public void RecommendationCreatedEvent_IsRecord_SupportsValueEquality()
    {
        // Arrange
        var recommendationId = Guid.NewGuid();
        var neighborId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var evt1 = new RecommendationCreatedEvent
        {
            RecommendationId = recommendationId,
            NeighborId = neighborId,
            Title = "Test",
            RecommendationType = RecommendationType.Restaurant,
            Timestamp = timestamp
        };

        var evt2 = new RecommendationCreatedEvent
        {
            RecommendationId = recommendationId,
            NeighborId = neighborId,
            Title = "Test",
            RecommendationType = RecommendationType.Restaurant,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void RecommendationCreatedEvent_DifferentValues_AreNotEqual()
    {
        // Arrange
        var evt1 = new RecommendationCreatedEvent
        {
            RecommendationId = Guid.NewGuid(),
            NeighborId = Guid.NewGuid(),
            Title = "Recommendation 1",
            RecommendationType = RecommendationType.Restaurant
        };

        var evt2 = new RecommendationCreatedEvent
        {
            RecommendationId = Guid.NewGuid(),
            NeighborId = Guid.NewGuid(),
            Title = "Recommendation 2",
            RecommendationType = RecommendationType.ServiceProvider
        };

        // Act & Assert
        Assert.That(evt1, Is.Not.EqualTo(evt2));
    }
}
