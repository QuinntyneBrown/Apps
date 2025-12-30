// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace NeighborhoodSocialNetwork.Core.Tests;

public class RecommendationTypeTests
{
    [Test]
    public void RecommendationType_Restaurant_HasCorrectValue()
    {
        // Arrange & Act
        var type = RecommendationType.Restaurant;

        // Assert
        Assert.That(type, Is.EqualTo(RecommendationType.Restaurant));
        Assert.That((int)type, Is.EqualTo(0));
    }

    [Test]
    public void RecommendationType_ServiceProvider_HasCorrectValue()
    {
        // Arrange & Act
        var type = RecommendationType.ServiceProvider;

        // Assert
        Assert.That(type, Is.EqualTo(RecommendationType.ServiceProvider));
        Assert.That((int)type, Is.EqualTo(1));
    }

    [Test]
    public void RecommendationType_Shop_HasCorrectValue()
    {
        // Arrange & Act
        var type = RecommendationType.Shop;

        // Assert
        Assert.That(type, Is.EqualTo(RecommendationType.Shop));
        Assert.That((int)type, Is.EqualTo(2));
    }

    [Test]
    public void RecommendationType_Healthcare_HasCorrectValue()
    {
        // Arrange & Act
        var type = RecommendationType.Healthcare;

        // Assert
        Assert.That(type, Is.EqualTo(RecommendationType.Healthcare));
        Assert.That((int)type, Is.EqualTo(3));
    }

    [Test]
    public void RecommendationType_Entertainment_HasCorrectValue()
    {
        // Arrange & Act
        var type = RecommendationType.Entertainment;

        // Assert
        Assert.That(type, Is.EqualTo(RecommendationType.Entertainment));
        Assert.That((int)type, Is.EqualTo(4));
    }

    [Test]
    public void RecommendationType_Education_HasCorrectValue()
    {
        // Arrange & Act
        var type = RecommendationType.Education;

        // Assert
        Assert.That(type, Is.EqualTo(RecommendationType.Education));
        Assert.That((int)type, Is.EqualTo(5));
    }

    [Test]
    public void RecommendationType_Other_HasCorrectValue()
    {
        // Arrange & Act
        var type = RecommendationType.Other;

        // Assert
        Assert.That(type, Is.EqualTo(RecommendationType.Other));
        Assert.That((int)type, Is.EqualTo(6));
    }

    [Test]
    public void RecommendationType_AllValues_CanBeAssigned()
    {
        // Arrange
        var types = new[]
        {
            RecommendationType.Restaurant,
            RecommendationType.ServiceProvider,
            RecommendationType.Shop,
            RecommendationType.Healthcare,
            RecommendationType.Entertainment,
            RecommendationType.Education,
            RecommendationType.Other
        };

        // Act & Assert
        foreach (var type in types)
        {
            var recommendation = new Recommendation { RecommendationType = type };
            Assert.That(recommendation.RecommendationType, Is.EqualTo(type));
        }
    }
}
