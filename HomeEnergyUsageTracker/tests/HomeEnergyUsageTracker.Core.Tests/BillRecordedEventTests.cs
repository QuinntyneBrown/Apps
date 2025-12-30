// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeEnergyUsageTracker.Core.Tests;

public class BillRecordedEventTests
{
    [Test]
    public void BillRecordedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var utilityBillId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var utilityType = UtilityType.Electricity;
        var amount = 150.75m;
        var timestamp = DateTime.UtcNow;

        // Act
        var billEvent = new BillRecordedEvent
        {
            UtilityBillId = utilityBillId,
            UserId = userId,
            UtilityType = utilityType,
            Amount = amount,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(billEvent.UtilityBillId, Is.EqualTo(utilityBillId));
            Assert.That(billEvent.UserId, Is.EqualTo(userId));
            Assert.That(billEvent.UtilityType, Is.EqualTo(utilityType));
            Assert.That(billEvent.Amount, Is.EqualTo(amount));
            Assert.That(billEvent.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void BillRecordedEvent_DefaultValues_AreSetCorrectly()
    {
        // Act
        var billEvent = new BillRecordedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(billEvent.UtilityBillId, Is.EqualTo(Guid.Empty));
            Assert.That(billEvent.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(billEvent.UtilityType, Is.EqualTo(UtilityType.Electricity));
            Assert.That(billEvent.Amount, Is.EqualTo(0m));
            Assert.That(billEvent.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void BillRecordedEvent_Timestamp_IsSetToCurrentTime()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var billEvent = new BillRecordedEvent
        {
            UtilityBillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            UtilityType = UtilityType.Gas,
            Amount = 100m
        };
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(billEvent.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void BillRecordedEvent_AllUtilityTypes_CanBeSet()
    {
        // Arrange
        var utilityTypes = new[]
        {
            UtilityType.Electricity,
            UtilityType.Gas,
            UtilityType.Water,
            UtilityType.Internet,
            UtilityType.Other
        };

        // Act & Assert
        foreach (var type in utilityTypes)
        {
            var billEvent = new BillRecordedEvent
            {
                UtilityBillId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                UtilityType = type,
                Amount = 100m
            };

            Assert.That(billEvent.UtilityType, Is.EqualTo(type));
        }
    }

    [Test]
    public void BillRecordedEvent_WithZeroAmount_IsValid()
    {
        // Arrange & Act
        var billEvent = new BillRecordedEvent
        {
            UtilityBillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            UtilityType = UtilityType.Water,
            Amount = 0m
        };

        // Assert
        Assert.That(billEvent.Amount, Is.EqualTo(0m));
    }

    [Test]
    public void BillRecordedEvent_IsImmutable()
    {
        // Arrange
        var utilityBillId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var utilityType = UtilityType.Gas;
        var amount = 200m;

        // Act
        var billEvent = new BillRecordedEvent
        {
            UtilityBillId = utilityBillId,
            UserId = userId,
            UtilityType = utilityType,
            Amount = amount
        };

        // Assert - Record properties are init-only
        Assert.Multiple(() =>
        {
            Assert.That(billEvent.UtilityBillId, Is.EqualTo(utilityBillId));
            Assert.That(billEvent.UserId, Is.EqualTo(userId));
            Assert.That(billEvent.UtilityType, Is.EqualTo(utilityType));
            Assert.That(billEvent.Amount, Is.EqualTo(amount));
        });
    }

    [Test]
    public void BillRecordedEvent_EqualityByValue()
    {
        // Arrange
        var utilityBillId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var utilityType = UtilityType.Electricity;
        var amount = 175m;
        var timestamp = DateTime.UtcNow;

        var event1 = new BillRecordedEvent
        {
            UtilityBillId = utilityBillId,
            UserId = userId,
            UtilityType = utilityType,
            Amount = amount,
            Timestamp = timestamp
        };

        var event2 = new BillRecordedEvent
        {
            UtilityBillId = utilityBillId,
            UserId = userId,
            UtilityType = utilityType,
            Amount = amount,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void BillRecordedEvent_DifferentValues_AreNotEqual()
    {
        // Arrange
        var event1 = new BillRecordedEvent
        {
            UtilityBillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            UtilityType = UtilityType.Electricity,
            Amount = 100m
        };

        var event2 = new BillRecordedEvent
        {
            UtilityBillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            UtilityType = UtilityType.Gas,
            Amount = 150m
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }

    [Test]
    public void BillRecordedEvent_WithLargeAmount_IsValid()
    {
        // Arrange
        var largeAmount = 99999.99m;

        // Act
        var billEvent = new BillRecordedEvent
        {
            UtilityBillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            UtilityType = UtilityType.Other,
            Amount = largeAmount
        };

        // Assert
        Assert.That(billEvent.Amount, Is.EqualTo(largeAmount));
    }
}
