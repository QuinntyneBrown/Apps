// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace DocumentVaultOrganizer.Core.Tests;

public class CategoryCreatedEventTests
{
    [Test]
    public void Constructor_CreatesEvent_WithAllProperties()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var name = "Legal Documents";
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new CategoryCreatedEvent
        {
            DocumentCategoryId = categoryId,
            Name = name,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.DocumentCategoryId, Is.EqualTo(categoryId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Event_WithDefaultTimestamp_SetsUtcNow()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var name = "Medical Records";

        // Act
        var evt = new CategoryCreatedEvent
        {
            DocumentCategoryId = categoryId,
            Name = name
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.DocumentCategoryId, Is.EqualTo(categoryId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Event_IsRecord_AndSupportsValueEquality()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var name = "Financial";
        var timestamp = DateTime.UtcNow;

        // Act
        var event1 = new CategoryCreatedEvent
        {
            DocumentCategoryId = categoryId,
            Name = name,
            Timestamp = timestamp
        };

        var event2 = new CategoryCreatedEvent
        {
            DocumentCategoryId = categoryId,
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
        var categoryId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new CategoryCreatedEvent
        {
            DocumentCategoryId = categoryId,
            Name = "Category1",
            Timestamp = timestamp
        };

        var event2 = new CategoryCreatedEvent
        {
            DocumentCategoryId = categoryId,
            Name = "Category2",
            Timestamp = timestamp
        };

        // Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }

    [Test]
    public void Event_WithEmptyName_CanBeCreated()
    {
        // Arrange
        var categoryId = Guid.NewGuid();

        // Act
        var evt = new CategoryCreatedEvent
        {
            DocumentCategoryId = categoryId,
            Name = string.Empty
        };

        // Assert
        Assert.That(evt.Name, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Event_Properties_AreInitOnly()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var name = "Tax Documents";

        // Act
        var evt = new CategoryCreatedEvent
        {
            DocumentCategoryId = categoryId,
            Name = name
        };

        // Assert - Properties are init-only, so they can only be set during initialization
        Assert.Multiple(() =>
        {
            Assert.That(evt.DocumentCategoryId, Is.EqualTo(categoryId));
            Assert.That(evt.Name, Is.EqualTo(name));
        });
    }

    [Test]
    public void Event_ToString_ContainsTypeName()
    {
        // Arrange
        var evt = new CategoryCreatedEvent
        {
            DocumentCategoryId = Guid.NewGuid(),
            Name = "Test Category"
        };

        // Act
        var result = evt.ToString();

        // Assert
        Assert.That(result, Does.Contain("CategoryCreatedEvent"));
    }
}
