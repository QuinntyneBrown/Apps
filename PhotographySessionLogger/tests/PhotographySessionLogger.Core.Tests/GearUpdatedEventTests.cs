// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PhotographySessionLogger.Core.Tests;

public class GearUpdatedEventTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesEvent()
    {
        // Arrange & Act
        var eventRecord = new GearUpdatedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.GearId, Is.EqualTo(Guid.Empty));
            Assert.That(eventRecord.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(eventRecord.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Constructor_WithProperties_SetsValuesCorrectly()
    {
        // Arrange
        var gearId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var eventRecord = new GearUpdatedEvent
        {
            GearId = gearId,
            UserId = userId,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.GearId, Is.EqualTo(gearId));
            Assert.That(eventRecord.UserId, Is.EqualTo(userId));
            Assert.That(eventRecord.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Equality_SameValues_AreEqual()
    {
        // Arrange
        var gearId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new GearUpdatedEvent
        {
            GearId = gearId,
            UserId = userId,
            Timestamp = timestamp
        };

        var event2 = new GearUpdatedEvent
        {
            GearId = gearId,
            UserId = userId,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Equality_DifferentValues_AreNotEqual()
    {
        // Arrange
        var event1 = new GearUpdatedEvent
        {
            GearId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        var event2 = new GearUpdatedEvent
        {
            GearId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
