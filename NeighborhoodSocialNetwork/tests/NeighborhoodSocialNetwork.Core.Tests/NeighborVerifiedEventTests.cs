// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace NeighborhoodSocialNetwork.Core.Tests;

public class NeighborVerifiedEventTests
{
    [Test]
    public void NeighborVerifiedEvent_CanBeCreated_WithAllProperties()
    {
        // Arrange
        var neighborId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new NeighborVerifiedEvent
        {
            NeighborId = neighborId,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.NeighborId, Is.EqualTo(neighborId));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void NeighborVerifiedEvent_DefaultTimestamp_IsNotDefault()
    {
        // Arrange & Act
        var evt = new NeighborVerifiedEvent
        {
            NeighborId = Guid.NewGuid()
        };

        // Assert
        Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
    }

    [Test]
    public void NeighborVerifiedEvent_IsRecord_SupportsValueEquality()
    {
        // Arrange
        var neighborId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var evt1 = new NeighborVerifiedEvent
        {
            NeighborId = neighborId,
            Timestamp = timestamp
        };

        var evt2 = new NeighborVerifiedEvent
        {
            NeighborId = neighborId,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void NeighborVerifiedEvent_DifferentValues_AreNotEqual()
    {
        // Arrange
        var evt1 = new NeighborVerifiedEvent
        {
            NeighborId = Guid.NewGuid()
        };

        var evt2 = new NeighborVerifiedEvent
        {
            NeighborId = Guid.NewGuid()
        };

        // Act & Assert
        Assert.That(evt1, Is.Not.EqualTo(evt2));
    }
}
