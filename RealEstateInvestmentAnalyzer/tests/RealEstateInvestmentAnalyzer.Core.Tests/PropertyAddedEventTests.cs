// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RealEstateInvestmentAnalyzer.Core.Tests;

public class PropertyAddedEventTests
{
    [Test]
    public void Constructor_WithValidParameters_CreatesEvent()
    {
        // Arrange
        var propertyId = Guid.NewGuid();
        var address = "123 Main St";
        var purchasePrice = 250000m;

        // Act
        var eventData = new PropertyAddedEvent
        {
            PropertyId = propertyId,
            Address = address,
            PurchasePrice = purchasePrice
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.PropertyId, Is.EqualTo(propertyId));
            Assert.That(eventData.Address, Is.EqualTo(address));
            Assert.That(eventData.PurchasePrice, Is.EqualTo(purchasePrice));
            Assert.That(eventData.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Timestamp_DefaultValue_IsSetToUtcNow()
    {
        // Arrange
        var beforeCreate = DateTime.UtcNow;

        // Act
        var eventData = new PropertyAddedEvent
        {
            PropertyId = Guid.NewGuid(),
            Address = "456 Oak Ave",
            PurchasePrice = 300000m
        };

        var afterCreate = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.Timestamp, Is.GreaterThanOrEqualTo(beforeCreate));
            Assert.That(eventData.Timestamp, Is.LessThanOrEqualTo(afterCreate));
        });
    }

    [Test]
    public void Record_Equality_WorksCorrectly()
    {
        // Arrange
        var propertyId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new PropertyAddedEvent
        {
            PropertyId = propertyId,
            Address = "123 Main St",
            PurchasePrice = 250000m,
            Timestamp = timestamp
        };

        var event2 = new PropertyAddedEvent
        {
            PropertyId = propertyId,
            Address = "123 Main St",
            PurchasePrice = 250000m,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Record_Inequality_WorksCorrectly()
    {
        // Arrange
        var event1 = new PropertyAddedEvent
        {
            PropertyId = Guid.NewGuid(),
            Address = "123 Main St",
            PurchasePrice = 250000m
        };

        var event2 = new PropertyAddedEvent
        {
            PropertyId = Guid.NewGuid(),
            Address = "456 Oak Ave",
            PurchasePrice = 300000m
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
