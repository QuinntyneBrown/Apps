// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PhotographySessionLogger.Core.Tests;

public class GearAddedEventTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesEvent()
    {
        // Arrange & Act
        var eventRecord = new GearAddedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.GearId, Is.EqualTo(Guid.Empty));
            Assert.That(eventRecord.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(eventRecord.Name, Is.EqualTo(string.Empty));
            Assert.That(eventRecord.GearType, Is.EqualTo(string.Empty));
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
        var eventRecord = new GearAddedEvent
        {
            GearId = gearId,
            UserId = userId,
            Name = "Canon EOS R5",
            GearType = "Camera",
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.GearId, Is.EqualTo(gearId));
            Assert.That(eventRecord.UserId, Is.EqualTo(userId));
            Assert.That(eventRecord.Name, Is.EqualTo("Canon EOS R5"));
            Assert.That(eventRecord.GearType, Is.EqualTo("Camera"));
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

        var event1 = new GearAddedEvent
        {
            GearId = gearId,
            UserId = userId,
            Name = "Camera",
            GearType = "Camera",
            Timestamp = timestamp
        };

        var event2 = new GearAddedEvent
        {
            GearId = gearId,
            UserId = userId,
            Name = "Camera",
            GearType = "Camera",
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Equality_DifferentValues_AreNotEqual()
    {
        // Arrange
        var event1 = new GearAddedEvent
        {
            GearId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Camera 1",
            GearType = "Camera"
        };

        var event2 = new GearAddedEvent
        {
            GearId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Lens 1",
            GearType = "Lens"
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
