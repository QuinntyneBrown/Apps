// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalReadingList.Core.Tests;

public class ResourceAddedEventTests
{
    [Test]
    public void Constructor_WithValidParameters_CreatesEvent()
    {
        // Arrange
        var resourceId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var title = "Clean Code";
        var resourceType = ResourceType.Book;

        // Act
        var eventData = new ResourceAddedEvent
        {
            ResourceId = resourceId,
            UserId = userId,
            Title = title,
            ResourceType = resourceType
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.ResourceId, Is.EqualTo(resourceId));
            Assert.That(eventData.UserId, Is.EqualTo(userId));
            Assert.That(eventData.Title, Is.EqualTo(title));
            Assert.That(eventData.ResourceType, Is.EqualTo(resourceType));
            Assert.That(eventData.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Constructor_DifferentResourceTypes_StoresCorrectly()
    {
        // Arrange & Act
        var bookEvent = new ResourceAddedEvent
        {
            ResourceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Design Patterns",
            ResourceType = ResourceType.Book
        };

        var articleEvent = new ResourceAddedEvent
        {
            ResourceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Introduction to Microservices",
            ResourceType = ResourceType.Article
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(bookEvent.ResourceType, Is.EqualTo(ResourceType.Book));
            Assert.That(articleEvent.ResourceType, Is.EqualTo(ResourceType.Article));
        });
    }

    [Test]
    public void Timestamp_DefaultValue_IsSetToUtcNow()
    {
        // Arrange
        var beforeCreate = DateTime.UtcNow;

        // Act
        var eventData = new ResourceAddedEvent
        {
            ResourceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Resource",
            ResourceType = ResourceType.BlogPost
        };

        var afterCreate = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.Timestamp, Is.GreaterThanOrEqualTo(beforeCreate));
            Assert.That(eventData.Timestamp, Is.LessThanOrEqualTo(afterCreate));
        });
    }

    [Test]
    public void Record_Equality_WorksCorrectly()
    {
        // Arrange
        var resourceId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new ResourceAddedEvent
        {
            ResourceId = resourceId,
            UserId = userId,
            Title = "Test",
            ResourceType = ResourceType.Book,
            Timestamp = timestamp
        };

        var event2 = new ResourceAddedEvent
        {
            ResourceId = resourceId,
            UserId = userId,
            Title = "Test",
            ResourceType = ResourceType.Book,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Record_Inequality_WorksCorrectly()
    {
        // Arrange
        var event1 = new ResourceAddedEvent
        {
            ResourceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Resource 1",
            ResourceType = ResourceType.Book
        };

        var event2 = new ResourceAddedEvent
        {
            ResourceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Resource 2",
            ResourceType = ResourceType.Article
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
