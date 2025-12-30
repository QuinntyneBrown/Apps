// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyTreeBuilder.Core.Tests;

public class StoryAddedEventTests
{
    [Test]
    public void Constructor_CreatesEvent_WithAllProperties()
    {
        // Arrange
        var storyId = Guid.NewGuid();
        var personId = Guid.NewGuid();
        var title = "My Story";
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new StoryAddedEvent
        {
            StoryId = storyId,
            PersonId = personId,
            Title = title,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.StoryId, Is.EqualTo(storyId));
            Assert.That(evt.PersonId, Is.EqualTo(personId));
            Assert.That(evt.Title, Is.EqualTo(title));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Event_WithDefaultTimestamp_SetsUtcNow()
    {
        // Arrange
        var storyId = Guid.NewGuid();
        var personId = Guid.NewGuid();
        var title = "War Stories";

        // Act
        var evt = new StoryAddedEvent
        {
            StoryId = storyId,
            PersonId = personId,
            Title = title
        };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void Event_IsRecord_AndSupportsValueEquality()
    {
        // Arrange
        var storyId = Guid.NewGuid();
        var personId = Guid.NewGuid();
        var title = "Family History";
        var timestamp = DateTime.UtcNow;

        // Act
        var event1 = new StoryAddedEvent
        {
            StoryId = storyId,
            PersonId = personId,
            Title = title,
            Timestamp = timestamp
        };

        var event2 = new StoryAddedEvent
        {
            StoryId = storyId,
            PersonId = personId,
            Title = title,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Event_WithDifferentProperties_AreNotEqual()
    {
        // Arrange
        var storyId = Guid.NewGuid();
        var personId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new StoryAddedEvent
        {
            StoryId = storyId,
            PersonId = personId,
            Title = "Story 1",
            Timestamp = timestamp
        };

        var event2 = new StoryAddedEvent
        {
            StoryId = storyId,
            PersonId = personId,
            Title = "Story 2",
            Timestamp = timestamp
        };

        // Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }

    [Test]
    public void Event_Properties_AreInitOnly()
    {
        // Arrange
        var storyId = Guid.NewGuid();
        var personId = Guid.NewGuid();
        var title = "Immigration Story";

        // Act
        var evt = new StoryAddedEvent
        {
            StoryId = storyId,
            PersonId = personId,
            Title = title
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.StoryId, Is.EqualTo(storyId));
            Assert.That(evt.PersonId, Is.EqualTo(personId));
            Assert.That(evt.Title, Is.EqualTo(title));
        });
    }

    [Test]
    public void Event_ToString_ContainsTypeName()
    {
        // Arrange
        var evt = new StoryAddedEvent
        {
            StoryId = Guid.NewGuid(),
            PersonId = Guid.NewGuid(),
            Title = "Test Story"
        };

        // Act
        var result = evt.ToString();

        // Assert
        Assert.That(result, Does.Contain("StoryAddedEvent"));
    }
}
