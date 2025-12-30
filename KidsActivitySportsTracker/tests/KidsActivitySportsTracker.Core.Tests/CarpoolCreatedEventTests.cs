// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace KidsActivitySportsTracker.Core.Tests;

public class CarpoolCreatedEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesEvent()
    {
        // Arrange
        var carpoolId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Soccer Carpool";

        // Act
        var evt = new CarpoolCreatedEvent
        {
            CarpoolId = carpoolId,
            UserId = userId,
            Name = name
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.CarpoolId, Is.EqualTo(carpoolId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Timestamp_IsSetAutomatically()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var evt = new CarpoolCreatedEvent
        {
            CarpoolId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(evt.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation));
        Assert.That(evt.Timestamp, Is.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void CarpoolId_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedId = Guid.NewGuid();

        // Act
        var evt = new CarpoolCreatedEvent { CarpoolId = expectedId };

        // Assert
        Assert.That(evt.CarpoolId, Is.EqualTo(expectedId));
    }

    [Test]
    public void UserId_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedUserId = Guid.NewGuid();

        // Act
        var evt = new CarpoolCreatedEvent { UserId = expectedUserId };

        // Assert
        Assert.That(evt.UserId, Is.EqualTo(expectedUserId));
    }

    [Test]
    public void Name_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedName = "Basketball Carpool";

        // Act
        var evt = new CarpoolCreatedEvent { Name = expectedName };

        // Assert
        Assert.That(evt.Name, Is.EqualTo(expectedName));
    }
}
