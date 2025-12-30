// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PokerGameTracker.Core.Tests;

public class BankrollRecordedEventTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesEvent()
    {
        // Arrange & Act
        var eventRecord = new BankrollRecordedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.BankrollId, Is.EqualTo(Guid.Empty));
            Assert.That(eventRecord.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(eventRecord.Amount, Is.EqualTo(0m));
            Assert.That(eventRecord.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Constructor_WithProperties_SetsValuesCorrectly()
    {
        // Arrange
        var bankrollId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var eventRecord = new BankrollRecordedEvent
        {
            BankrollId = bankrollId,
            UserId = userId,
            Amount = 5000m,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.BankrollId, Is.EqualTo(bankrollId));
            Assert.That(eventRecord.UserId, Is.EqualTo(userId));
            Assert.That(eventRecord.Amount, Is.EqualTo(5000m));
            Assert.That(eventRecord.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Equality_SameValues_AreEqual()
    {
        // Arrange
        var bankrollId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new BankrollRecordedEvent
        {
            BankrollId = bankrollId,
            UserId = userId,
            Amount = 5000m,
            Timestamp = timestamp
        };

        var event2 = new BankrollRecordedEvent
        {
            BankrollId = bankrollId,
            UserId = userId,
            Amount = 5000m,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Equality_DifferentValues_AreNotEqual()
    {
        // Arrange
        var event1 = new BankrollRecordedEvent
        {
            BankrollId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Amount = 5000m
        };

        var event2 = new BankrollRecordedEvent
        {
            BankrollId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Amount = 3000m
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
