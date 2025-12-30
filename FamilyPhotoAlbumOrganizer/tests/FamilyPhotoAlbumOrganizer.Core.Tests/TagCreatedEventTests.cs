// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyPhotoAlbumOrganizer.Core.Tests;

public class TagCreatedEventTests
{
    [Test]
    public void Constructor_CreatesEvent_WithAllProperties()
    {
        // Arrange
        var tagId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Vacation";
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new TagCreatedEvent
        {
            TagId = tagId,
            UserId = userId,
            Name = name,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.TagId, Is.EqualTo(tagId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Event_WithDefaultTimestamp_SetsUtcNow()
    {
        // Arrange
        var tagId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Family";

        // Act
        var evt = new TagCreatedEvent
        {
            TagId = tagId,
            UserId = userId,
            Name = name
        };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void Event_IsRecord_AndSupportsValueEquality()
    {
        // Arrange
        var tagId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Birthday";
        var timestamp = DateTime.UtcNow;

        // Act
        var event1 = new TagCreatedEvent
        {
            TagId = tagId,
            UserId = userId,
            Name = name,
            Timestamp = timestamp
        };

        var event2 = new TagCreatedEvent
        {
            TagId = tagId,
            UserId = userId,
            Name = name,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Event_WithDifferentProperties_AreNotEqual()
    {
        // Arrange
        var tagId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new TagCreatedEvent
        {
            TagId = tagId,
            UserId = userId,
            Name = "Tag1",
            Timestamp = timestamp
        };

        var event2 = new TagCreatedEvent
        {
            TagId = tagId,
            UserId = userId,
            Name = "Tag2",
            Timestamp = timestamp
        };

        // Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }

    [Test]
    public void Event_Properties_AreInitOnly()
    {
        // Arrange
        var tagId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Holiday";

        // Act
        var evt = new TagCreatedEvent
        {
            TagId = tagId,
            UserId = userId,
            Name = name
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.TagId, Is.EqualTo(tagId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Name, Is.EqualTo(name));
        });
    }

    [Test]
    public void Event_ToString_ContainsTypeName()
    {
        // Arrange
        var evt = new TagCreatedEvent
        {
            TagId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Tag"
        };

        // Act
        var result = evt.ToString();

        // Assert
        Assert.That(result, Does.Contain("TagCreatedEvent"));
    }
}
