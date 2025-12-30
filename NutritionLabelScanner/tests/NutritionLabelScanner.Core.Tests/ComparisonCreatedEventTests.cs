// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace NutritionLabelScanner.Core.Tests;

public class ComparisonCreatedEventTests
{
    [Test]
    public void ComparisonCreatedEvent_CanBeCreated_WithAllProperties()
    {
        // Arrange
        var comparisonId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Protein Bars Comparison";
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new ComparisonCreatedEvent
        {
            ComparisonId = comparisonId,
            UserId = userId,
            Name = name,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ComparisonId, Is.EqualTo(comparisonId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void ComparisonCreatedEvent_DefaultTimestamp_IsNotDefault()
    {
        // Arrange & Act
        var evt = new ComparisonCreatedEvent
        {
            ComparisonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Comparison"
        };

        // Assert
        Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
    }

    [Test]
    public void ComparisonCreatedEvent_IsRecord_SupportsValueEquality()
    {
        // Arrange
        var comparisonId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var evt1 = new ComparisonCreatedEvent
        {
            ComparisonId = comparisonId,
            UserId = userId,
            Name = "Test",
            Timestamp = timestamp
        };

        var evt2 = new ComparisonCreatedEvent
        {
            ComparisonId = comparisonId,
            UserId = userId,
            Name = "Test",
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void ComparisonCreatedEvent_DifferentValues_AreNotEqual()
    {
        // Arrange
        var evt1 = new ComparisonCreatedEvent
        {
            ComparisonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Comparison 1"
        };

        var evt2 = new ComparisonCreatedEvent
        {
            ComparisonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Comparison 2"
        };

        // Act & Assert
        Assert.That(evt1, Is.Not.EqualTo(evt2));
    }
}
