// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace KidsActivitySportsTracker.Core.Tests;

public class ActivityCreatedEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesEvent()
    {
        // Arrange
        var activityId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var childName = "John Doe";
        var name = "Soccer Practice";

        // Act
        var evt = new ActivityCreatedEvent
        {
            ActivityId = activityId,
            UserId = userId,
            ChildName = childName,
            Name = name
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ActivityId, Is.EqualTo(activityId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.ChildName, Is.EqualTo(childName));
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
        var evt = new ActivityCreatedEvent
        {
            ActivityId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(evt.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation));
        Assert.That(evt.Timestamp, Is.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void ActivityId_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedId = Guid.NewGuid();

        // Act
        var evt = new ActivityCreatedEvent { ActivityId = expectedId };

        // Assert
        Assert.That(evt.ActivityId, Is.EqualTo(expectedId));
    }

    [Test]
    public void UserId_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedUserId = Guid.NewGuid();

        // Act
        var evt = new ActivityCreatedEvent { UserId = expectedUserId };

        // Assert
        Assert.That(evt.UserId, Is.EqualTo(expectedUserId));
    }

    [Test]
    public void ChildName_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedChildName = "Jane Smith";

        // Act
        var evt = new ActivityCreatedEvent { ChildName = expectedChildName };

        // Assert
        Assert.That(evt.ChildName, Is.EqualTo(expectedChildName));
    }

    [Test]
    public void Name_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedName = "Piano Lessons";

        // Act
        var evt = new ActivityCreatedEvent { Name = expectedName };

        // Assert
        Assert.That(evt.Name, Is.EqualTo(expectedName));
    }
}
