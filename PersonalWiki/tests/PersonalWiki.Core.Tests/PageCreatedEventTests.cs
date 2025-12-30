// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalWiki.Core.Tests;

public class PageCreatedEventTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesEvent()
    {
        // Arrange & Act
        var eventRecord = new PageCreatedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.WikiPageId, Is.EqualTo(Guid.Empty));
            Assert.That(eventRecord.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(eventRecord.Title, Is.EqualTo(string.Empty));
            Assert.That(eventRecord.Slug, Is.EqualTo(string.Empty));
            Assert.That(eventRecord.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Constructor_WithProperties_SetsValuesCorrectly()
    {
        // Arrange
        var pageId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var eventRecord = new PageCreatedEvent
        {
            WikiPageId = pageId,
            UserId = userId,
            Title = "New Page",
            Slug = "new-page",
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.WikiPageId, Is.EqualTo(pageId));
            Assert.That(eventRecord.UserId, Is.EqualTo(userId));
            Assert.That(eventRecord.Title, Is.EqualTo("New Page"));
            Assert.That(eventRecord.Slug, Is.EqualTo("new-page"));
            Assert.That(eventRecord.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Record_IsImmutable_PropertiesAreInitOnly()
    {
        // Arrange
        var pageId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        // Act
        var eventRecord = new PageCreatedEvent
        {
            WikiPageId = pageId,
            UserId = userId,
            Title = "Test Page",
            Slug = "test-page"
        };

        // Assert - Verify properties were set
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.WikiPageId, Is.EqualTo(pageId));
            Assert.That(eventRecord.UserId, Is.EqualTo(userId));
            Assert.That(eventRecord.Title, Is.EqualTo("Test Page"));
            Assert.That(eventRecord.Slug, Is.EqualTo("test-page"));
        });
    }

    [Test]
    public void Equality_SameValues_AreEqual()
    {
        // Arrange
        var pageId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new PageCreatedEvent
        {
            WikiPageId = pageId,
            UserId = userId,
            Title = "Test Page",
            Slug = "test-page",
            Timestamp = timestamp
        };

        var event2 = new PageCreatedEvent
        {
            WikiPageId = pageId,
            UserId = userId,
            Title = "Test Page",
            Slug = "test-page",
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Equality_DifferentValues_AreNotEqual()
    {
        // Arrange
        var event1 = new PageCreatedEvent
        {
            WikiPageId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Page 1"
        };

        var event2 = new PageCreatedEvent
        {
            WikiPageId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Page 2"
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
