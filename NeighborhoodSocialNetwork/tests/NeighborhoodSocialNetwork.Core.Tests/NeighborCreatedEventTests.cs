// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace NeighborhoodSocialNetwork.Core.Tests;

public class NeighborCreatedEventTests
{
    [Test]
    public void NeighborCreatedEvent_CanBeCreated_WithAllProperties()
    {
        // Arrange
        var neighborId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Jane Doe";
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new NeighborCreatedEvent
        {
            NeighborId = neighborId,
            UserId = userId,
            Name = name,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.NeighborId, Is.EqualTo(neighborId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void NeighborCreatedEvent_DefaultTimestamp_IsNotDefault()
    {
        // Arrange & Act
        var evt = new NeighborCreatedEvent
        {
            NeighborId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Neighbor"
        };

        // Assert
        Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
    }

    [Test]
    public void NeighborCreatedEvent_IsRecord_SupportsValueEquality()
    {
        // Arrange
        var neighborId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var evt1 = new NeighborCreatedEvent
        {
            NeighborId = neighborId,
            UserId = userId,
            Name = "Test Neighbor",
            Timestamp = timestamp
        };

        var evt2 = new NeighborCreatedEvent
        {
            NeighborId = neighborId,
            UserId = userId,
            Name = "Test Neighbor",
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void NeighborCreatedEvent_DifferentValues_AreNotEqual()
    {
        // Arrange
        var evt1 = new NeighborCreatedEvent
        {
            NeighborId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Neighbor 1"
        };

        var evt2 = new NeighborCreatedEvent
        {
            NeighborId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Neighbor 2"
        };

        // Act & Assert
        Assert.That(evt1, Is.Not.EqualTo(evt2));
    }
}
