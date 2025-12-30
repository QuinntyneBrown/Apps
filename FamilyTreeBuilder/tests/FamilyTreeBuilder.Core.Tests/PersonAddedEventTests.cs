// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyTreeBuilder.Core.Tests;

public class PersonAddedEventTests
{
    [Test]
    public void Constructor_CreatesEvent_WithAllProperties()
    {
        // Arrange
        var personId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var firstName = "John";
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new PersonAddedEvent
        {
            PersonId = personId,
            UserId = userId,
            FirstName = firstName,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.PersonId, Is.EqualTo(personId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.FirstName, Is.EqualTo(firstName));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Event_WithDefaultTimestamp_SetsUtcNow()
    {
        // Arrange
        var personId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var firstName = "Jane";

        // Act
        var evt = new PersonAddedEvent
        {
            PersonId = personId,
            UserId = userId,
            FirstName = firstName
        };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void Event_IsRecord_AndSupportsValueEquality()
    {
        // Arrange
        var personId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var firstName = "Bob";
        var timestamp = DateTime.UtcNow;

        // Act
        var event1 = new PersonAddedEvent
        {
            PersonId = personId,
            UserId = userId,
            FirstName = firstName,
            Timestamp = timestamp
        };

        var event2 = new PersonAddedEvent
        {
            PersonId = personId,
            UserId = userId,
            FirstName = firstName,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Event_WithDifferentProperties_AreNotEqual()
    {
        // Arrange
        var personId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new PersonAddedEvent
        {
            PersonId = personId,
            UserId = userId,
            FirstName = "Alice",
            Timestamp = timestamp
        };

        var event2 = new PersonAddedEvent
        {
            PersonId = personId,
            UserId = userId,
            FirstName = "Bob",
            Timestamp = timestamp
        };

        // Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }

    [Test]
    public void Event_Properties_AreInitOnly()
    {
        // Arrange
        var personId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var firstName = "Charlie";

        // Act
        var evt = new PersonAddedEvent
        {
            PersonId = personId,
            UserId = userId,
            FirstName = firstName
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.PersonId, Is.EqualTo(personId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.FirstName, Is.EqualTo(firstName));
        });
    }

    [Test]
    public void Event_ToString_ContainsTypeName()
    {
        // Arrange
        var evt = new PersonAddedEvent
        {
            PersonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            FirstName = "Test"
        };

        // Act
        var result = evt.ToString();

        // Assert
        Assert.That(result, Does.Contain("PersonAddedEvent"));
    }
}
