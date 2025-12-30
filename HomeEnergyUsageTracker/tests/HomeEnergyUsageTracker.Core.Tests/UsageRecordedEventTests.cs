// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeEnergyUsageTracker.Core.Tests;

public class UsageRecordedEventTests
{
    [Test]
    public void UsageRecordedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var usageId = Guid.NewGuid();
        var date = DateTime.UtcNow;
        var amount = 123.45m;
        var timestamp = DateTime.UtcNow;

        // Act
        var usageEvent = new UsageRecordedEvent
        {
            UsageId = usageId,
            Date = date,
            Amount = amount,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(usageEvent.UsageId, Is.EqualTo(usageId));
            Assert.That(usageEvent.Date, Is.EqualTo(date));
            Assert.That(usageEvent.Amount, Is.EqualTo(amount));
            Assert.That(usageEvent.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void UsageRecordedEvent_DefaultValues_AreSetCorrectly()
    {
        // Act
        var usageEvent = new UsageRecordedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(usageEvent.UsageId, Is.EqualTo(Guid.Empty));
            Assert.That(usageEvent.Date, Is.EqualTo(default(DateTime)));
            Assert.That(usageEvent.Amount, Is.EqualTo(0m));
            Assert.That(usageEvent.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void UsageRecordedEvent_Timestamp_IsSetToCurrentTime()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var usageEvent = new UsageRecordedEvent
        {
            UsageId = Guid.NewGuid(),
            Date = DateTime.UtcNow,
            Amount = 100m
        };
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(usageEvent.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void UsageRecordedEvent_WithZeroAmount_IsValid()
    {
        // Arrange & Act
        var usageEvent = new UsageRecordedEvent
        {
            UsageId = Guid.NewGuid(),
            Date = DateTime.UtcNow,
            Amount = 0m
        };

        // Assert
        Assert.That(usageEvent.Amount, Is.EqualTo(0m));
    }

    [Test]
    public void UsageRecordedEvent_IsImmutable()
    {
        // Arrange
        var usageId = Guid.NewGuid();
        var date = DateTime.UtcNow;
        var amount = 250m;

        // Act
        var usageEvent = new UsageRecordedEvent
        {
            UsageId = usageId,
            Date = date,
            Amount = amount
        };

        // Assert - Record properties are init-only
        Assert.Multiple(() =>
        {
            Assert.That(usageEvent.UsageId, Is.EqualTo(usageId));
            Assert.That(usageEvent.Date, Is.EqualTo(date));
            Assert.That(usageEvent.Amount, Is.EqualTo(amount));
        });
    }

    [Test]
    public void UsageRecordedEvent_EqualityByValue()
    {
        // Arrange
        var usageId = Guid.NewGuid();
        var date = DateTime.UtcNow;
        var amount = 150m;
        var timestamp = DateTime.UtcNow;

        var event1 = new UsageRecordedEvent
        {
            UsageId = usageId,
            Date = date,
            Amount = amount,
            Timestamp = timestamp
        };

        var event2 = new UsageRecordedEvent
        {
            UsageId = usageId,
            Date = date,
            Amount = amount,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void UsageRecordedEvent_DifferentValues_AreNotEqual()
    {
        // Arrange
        var event1 = new UsageRecordedEvent
        {
            UsageId = Guid.NewGuid(),
            Date = DateTime.UtcNow,
            Amount = 100m
        };

        var event2 = new UsageRecordedEvent
        {
            UsageId = Guid.NewGuid(),
            Date = DateTime.UtcNow,
            Amount = 200m
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }

    [Test]
    public void UsageRecordedEvent_WithNegativeAmount_CanBeSet()
    {
        // Arrange & Act
        var usageEvent = new UsageRecordedEvent
        {
            UsageId = Guid.NewGuid(),
            Date = DateTime.UtcNow,
            Amount = -50m
        };

        // Assert
        Assert.That(usageEvent.Amount, Is.EqualTo(-50m));
    }

    [Test]
    public void UsageRecordedEvent_WithLargeAmount_IsValid()
    {
        // Arrange
        var largeAmount = 999999.99m;

        // Act
        var usageEvent = new UsageRecordedEvent
        {
            UsageId = Guid.NewGuid(),
            Date = DateTime.UtcNow,
            Amount = largeAmount
        };

        // Assert
        Assert.That(usageEvent.Amount, Is.EqualTo(largeAmount));
    }
}
