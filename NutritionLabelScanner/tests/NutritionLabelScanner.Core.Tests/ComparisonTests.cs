// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace NutritionLabelScanner.Core.Tests;

public class ComparisonTests
{
    [Test]
    public void Constructor_CreatesComparison_WithDefaultValues()
    {
        // Arrange & Act
        var comparison = new Comparison();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(comparison.ComparisonId, Is.EqualTo(Guid.Empty));
            Assert.That(comparison.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(comparison.Name, Is.EqualTo(string.Empty));
            Assert.That(comparison.ProductIds, Is.EqualTo("[]"));
            Assert.That(comparison.Results, Is.Null);
            Assert.That(comparison.WinnerProductId, Is.Null);
            Assert.That(comparison.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void ComparisonId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var comparison = new Comparison();
        var expectedId = Guid.NewGuid();

        // Act
        comparison.ComparisonId = expectedId;

        // Assert
        Assert.That(comparison.ComparisonId, Is.EqualTo(expectedId));
    }

    [Test]
    public void UserId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var comparison = new Comparison();
        var expectedUserId = Guid.NewGuid();

        // Act
        comparison.UserId = expectedUserId;

        // Assert
        Assert.That(comparison.UserId, Is.EqualTo(expectedUserId));
    }

    [Test]
    public void Name_CanBeSet_AndRetrieved()
    {
        // Arrange
        var comparison = new Comparison();
        var expectedName = "Protein Bars Comparison";

        // Act
        comparison.Name = expectedName;

        // Assert
        Assert.That(comparison.Name, Is.EqualTo(expectedName));
    }

    [Test]
    public void ProductIds_CanBeSet_AndRetrieved()
    {
        // Arrange
        var comparison = new Comparison();
        var expectedProductIds = "[\"guid1\", \"guid2\", \"guid3\"]";

        // Act
        comparison.ProductIds = expectedProductIds;

        // Assert
        Assert.That(comparison.ProductIds, Is.EqualTo(expectedProductIds));
    }

    [Test]
    public void Results_CanBeSet_AndRetrieved()
    {
        // Arrange
        var comparison = new Comparison();
        var expectedResults = "Product A has better nutrition profile";

        // Act
        comparison.Results = expectedResults;

        // Assert
        Assert.That(comparison.Results, Is.EqualTo(expectedResults));
    }

    [Test]
    public void WinnerProductId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var comparison = new Comparison();
        var expectedWinnerId = Guid.NewGuid();

        // Act
        comparison.WinnerProductId = expectedWinnerId;

        // Assert
        Assert.That(comparison.WinnerProductId, Is.EqualTo(expectedWinnerId));
    }

    [Test]
    public void CreatedAt_CanBeSet_AndRetrieved()
    {
        // Arrange
        var comparison = new Comparison();
        var expectedDate = new DateTime(2024, 1, 1);

        // Act
        comparison.CreatedAt = expectedDate;

        // Assert
        Assert.That(comparison.CreatedAt, Is.EqualTo(expectedDate));
    }

    [Test]
    public void HasWinner_ReturnsFalse_WhenWinnerProductIdIsNull()
    {
        // Arrange
        var comparison = new Comparison
        {
            ComparisonId = Guid.NewGuid(),
            WinnerProductId = null
        };

        // Act
        var result = comparison.HasWinner();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void HasWinner_ReturnsTrue_WhenWinnerProductIdIsSet()
    {
        // Arrange
        var comparison = new Comparison
        {
            ComparisonId = Guid.NewGuid(),
            WinnerProductId = Guid.NewGuid()
        };

        // Act
        var result = comparison.HasWinner();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void Comparison_WithAllPropertiesSet_IsValid()
    {
        // Arrange
        var comparisonId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var winnerId = Guid.NewGuid();

        // Act
        var comparison = new Comparison
        {
            ComparisonId = comparisonId,
            UserId = userId,
            Name = "Test Comparison",
            ProductIds = "[\"id1\", \"id2\"]",
            Results = "Test Results",
            WinnerProductId = winnerId
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(comparison.ComparisonId, Is.EqualTo(comparisonId));
            Assert.That(comparison.UserId, Is.EqualTo(userId));
            Assert.That(comparison.Name, Is.EqualTo("Test Comparison"));
            Assert.That(comparison.ProductIds, Is.EqualTo("[\"id1\", \"id2\"]"));
            Assert.That(comparison.Results, Is.EqualTo("Test Results"));
            Assert.That(comparison.WinnerProductId, Is.EqualTo(winnerId));
            Assert.That(comparison.HasWinner(), Is.True);
        });
    }
}
