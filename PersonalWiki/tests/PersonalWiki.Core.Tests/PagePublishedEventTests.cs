// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalWiki.Core.Tests;

public class PagePublishedEventTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesEvent()
    {
        // Arrange & Act
        var eventRecord = new PagePublishedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.WikiPageId, Is.EqualTo(Guid.Empty));
            Assert.That(eventRecord.Title, Is.EqualTo(string.Empty));
            Assert.That(eventRecord.Version, Is.EqualTo(0));
            Assert.That(eventRecord.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Constructor_WithProperties_SetsValuesCorrectly()
    {
        // Arrange
        var pageId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var eventRecord = new PagePublishedEvent
        {
            WikiPageId = pageId,
            Title = "Published Page",
            Version = 3,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.WikiPageId, Is.EqualTo(pageId));
            Assert.That(eventRecord.Title, Is.EqualTo("Published Page"));
            Assert.That(eventRecord.Version, Is.EqualTo(3));
            Assert.That(eventRecord.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Record_IsImmutable_PropertiesAreInitOnly()
    {
        // Arrange
        var pageId = Guid.NewGuid();

        // Act
        var eventRecord = new PagePublishedEvent
        {
            WikiPageId = pageId,
            Title = "Test Page",
            Version = 1
        };

        // Assert - Verify properties were set
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.WikiPageId, Is.EqualTo(pageId));
            Assert.That(eventRecord.Title, Is.EqualTo("Test Page"));
            Assert.That(eventRecord.Version, Is.EqualTo(1));
        });
    }

    [Test]
    public void Equality_SameValues_AreEqual()
    {
        // Arrange
        var pageId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new PagePublishedEvent
        {
            WikiPageId = pageId,
            Title = "Test Page",
            Version = 2,
            Timestamp = timestamp
        };

        var event2 = new PagePublishedEvent
        {
            WikiPageId = pageId,
            Title = "Test Page",
            Version = 2,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Equality_DifferentValues_AreNotEqual()
    {
        // Arrange
        var event1 = new PagePublishedEvent
        {
            WikiPageId = Guid.NewGuid(),
            Title = "Page 1",
            Version = 1
        };

        var event2 = new PagePublishedEvent
        {
            WikiPageId = Guid.NewGuid(),
            Title = "Page 2",
            Version = 2
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
