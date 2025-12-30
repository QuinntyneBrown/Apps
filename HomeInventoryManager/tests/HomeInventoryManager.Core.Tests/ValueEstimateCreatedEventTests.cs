// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeInventoryManager.Core.Tests;

public class ValueEstimateCreatedEventTests
{
    [Test]
    public void ValueEstimateCreatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var valueEstimateId = Guid.NewGuid();
        var itemId = Guid.NewGuid();
        var estimatedValue = 1500m;
        var estimationDate = DateTime.UtcNow;
        var timestamp = DateTime.UtcNow;

        // Act
        var estimateEvent = new ValueEstimateCreatedEvent
        {
            ValueEstimateId = valueEstimateId,
            ItemId = itemId,
            EstimatedValue = estimatedValue,
            EstimationDate = estimationDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(estimateEvent.ValueEstimateId, Is.EqualTo(valueEstimateId));
            Assert.That(estimateEvent.ItemId, Is.EqualTo(itemId));
            Assert.That(estimateEvent.EstimatedValue, Is.EqualTo(estimatedValue));
            Assert.That(estimateEvent.EstimationDate, Is.EqualTo(estimationDate));
            Assert.That(estimateEvent.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void ValueEstimateCreatedEvent_DefaultValues_AreSetCorrectly()
    {
        // Act
        var estimateEvent = new ValueEstimateCreatedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(estimateEvent.ValueEstimateId, Is.EqualTo(Guid.Empty));
            Assert.That(estimateEvent.ItemId, Is.EqualTo(Guid.Empty));
            Assert.That(estimateEvent.EstimatedValue, Is.EqualTo(0m));
            Assert.That(estimateEvent.EstimationDate, Is.EqualTo(default(DateTime)));
            Assert.That(estimateEvent.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void ValueEstimateCreatedEvent_Timestamp_IsSetToCurrentTime()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var estimateEvent = new ValueEstimateCreatedEvent
        {
            ValueEstimateId = Guid.NewGuid(),
            ItemId = Guid.NewGuid(),
            EstimatedValue = 2000m,
            EstimationDate = DateTime.UtcNow
        };
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(estimateEvent.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void ValueEstimateCreatedEvent_WithZeroValue_IsValid()
    {
        // Arrange & Act
        var estimateEvent = new ValueEstimateCreatedEvent
        {
            ValueEstimateId = Guid.NewGuid(),
            ItemId = Guid.NewGuid(),
            EstimatedValue = 0m,
            EstimationDate = DateTime.UtcNow
        };

        // Assert
        Assert.That(estimateEvent.EstimatedValue, Is.EqualTo(0m));
    }

    [Test]
    public void ValueEstimateCreatedEvent_IsImmutable()
    {
        // Arrange
        var valueEstimateId = Guid.NewGuid();
        var itemId = Guid.NewGuid();
        var estimatedValue = 3500m;
        var estimationDate = DateTime.UtcNow;

        // Act
        var estimateEvent = new ValueEstimateCreatedEvent
        {
            ValueEstimateId = valueEstimateId,
            ItemId = itemId,
            EstimatedValue = estimatedValue,
            EstimationDate = estimationDate
        };

        // Assert - Record properties are init-only
        Assert.Multiple(() =>
        {
            Assert.That(estimateEvent.ValueEstimateId, Is.EqualTo(valueEstimateId));
            Assert.That(estimateEvent.ItemId, Is.EqualTo(itemId));
            Assert.That(estimateEvent.EstimatedValue, Is.EqualTo(estimatedValue));
            Assert.That(estimateEvent.EstimationDate, Is.EqualTo(estimationDate));
        });
    }

    [Test]
    public void ValueEstimateCreatedEvent_EqualityByValue()
    {
        // Arrange
        var valueEstimateId = Guid.NewGuid();
        var itemId = Guid.NewGuid();
        var estimatedValue = 2500m;
        var estimationDate = DateTime.UtcNow;
        var timestamp = DateTime.UtcNow;

        var event1 = new ValueEstimateCreatedEvent
        {
            ValueEstimateId = valueEstimateId,
            ItemId = itemId,
            EstimatedValue = estimatedValue,
            EstimationDate = estimationDate,
            Timestamp = timestamp
        };

        var event2 = new ValueEstimateCreatedEvent
        {
            ValueEstimateId = valueEstimateId,
            ItemId = itemId,
            EstimatedValue = estimatedValue,
            EstimationDate = estimationDate,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void ValueEstimateCreatedEvent_DifferentValues_AreNotEqual()
    {
        // Arrange
        var event1 = new ValueEstimateCreatedEvent
        {
            ValueEstimateId = Guid.NewGuid(),
            ItemId = Guid.NewGuid(),
            EstimatedValue = 1000m,
            EstimationDate = DateTime.UtcNow
        };

        var event2 = new ValueEstimateCreatedEvent
        {
            ValueEstimateId = Guid.NewGuid(),
            ItemId = Guid.NewGuid(),
            EstimatedValue = 2000m,
            EstimationDate = DateTime.UtcNow
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }

    [Test]
    public void ValueEstimateCreatedEvent_WithLargeValue_IsValid()
    {
        // Arrange
        var largeValue = 999999.99m;

        // Act
        var estimateEvent = new ValueEstimateCreatedEvent
        {
            ValueEstimateId = Guid.NewGuid(),
            ItemId = Guid.NewGuid(),
            EstimatedValue = largeValue,
            EstimationDate = DateTime.UtcNow
        };

        // Assert
        Assert.That(estimateEvent.EstimatedValue, Is.EqualTo(largeValue));
    }

    [Test]
    public void ValueEstimateCreatedEvent_WithPastEstimationDate_IsValid()
    {
        // Arrange
        var pastDate = DateTime.UtcNow.AddYears(-1);

        // Act
        var estimateEvent = new ValueEstimateCreatedEvent
        {
            ValueEstimateId = Guid.NewGuid(),
            ItemId = Guid.NewGuid(),
            EstimatedValue = 1200m,
            EstimationDate = pastDate
        };

        // Assert
        Assert.That(estimateEvent.EstimationDate, Is.EqualTo(pastDate));
    }
}
