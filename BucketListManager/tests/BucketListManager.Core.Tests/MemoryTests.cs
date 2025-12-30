// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BucketListManager.Core.Tests;

public class MemoryTests
{
    [Test]
    public void Constructor_DefaultValues_SetsPropertiesCorrectly()
    {
        // Arrange & Act
        var memory = new Memory
        {
            MemoryId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            BucketListItemId = Guid.NewGuid(),
            Title = "First time in Paris",
            Description = "Amazing experience at the Eiffel Tower"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(memory.MemoryId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(memory.UserId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(memory.BucketListItemId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(memory.Title, Is.EqualTo("First time in Paris"));
            Assert.That(memory.Description, Is.EqualTo("Amazing experience at the Eiffel Tower"));
            Assert.That(memory.MemoryDate, Is.Not.EqualTo(default(DateTime)));
            Assert.That(memory.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Memory_WithPhotoUrl_StoresUrlCorrectly()
    {
        // Arrange & Act
        var memory = new Memory
        {
            PhotoUrl = "https://example.com/photo.jpg"
        };

        // Assert
        Assert.That(memory.PhotoUrl, Is.EqualTo("https://example.com/photo.jpg"));
    }

    [Test]
    public void Memory_WithNullPhotoUrl_AllowsNull()
    {
        // Arrange & Act
        var memory = new Memory
        {
            PhotoUrl = null
        };

        // Assert
        Assert.That(memory.PhotoUrl, Is.Null);
    }

    [Test]
    public void Memory_WithDescription_StoresDescriptionCorrectly()
    {
        // Arrange & Act
        var memory = new Memory
        {
            Description = "This was an unforgettable moment"
        };

        // Assert
        Assert.That(memory.Description, Is.EqualTo("This was an unforgettable moment"));
    }

    [Test]
    public void Memory_WithNullDescription_AllowsNull()
    {
        // Arrange & Act
        var memory = new Memory
        {
            Description = null
        };

        // Assert
        Assert.That(memory.Description, Is.Null);
    }

    [Test]
    public void Memory_WithSpecificMemoryDate_StoresDateCorrectly()
    {
        // Arrange
        var memoryDate = new DateTime(2024, 7, 4);

        // Act
        var memory = new Memory
        {
            MemoryDate = memoryDate
        };

        // Assert
        Assert.That(memory.MemoryDate, Is.EqualTo(memoryDate));
    }

    [Test]
    public void Memory_WithBucketListItem_AssociatesItemCorrectly()
    {
        // Arrange
        var item = new BucketListItem
        {
            BucketListItemId = Guid.NewGuid(),
            Title = "Visit Tokyo"
        };
        var memory = new Memory
        {
            BucketListItemId = item.BucketListItemId,
            BucketListItem = item
        };

        // Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(memory.BucketListItem, Is.Not.Null);
            Assert.That(memory.BucketListItem.BucketListItemId, Is.EqualTo(item.BucketListItemId));
            Assert.That(memory.BucketListItem.Title, Is.EqualTo("Visit Tokyo"));
        });
    }

    [Test]
    public void Memory_WithNullBucketListItem_AllowsNull()
    {
        // Arrange & Act
        var memory = new Memory
        {
            BucketListItemId = Guid.NewGuid(),
            BucketListItem = null
        };

        // Assert
        Assert.That(memory.BucketListItem, Is.Null);
    }

    [Test]
    public void Memory_DefaultMemoryDate_IsUtcNow()
    {
        // Arrange
        var beforeCreate = DateTime.UtcNow;

        // Act
        var memory = new Memory();
        var afterCreate = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(memory.MemoryDate, Is.GreaterThanOrEqualTo(beforeCreate));
            Assert.That(memory.MemoryDate, Is.LessThanOrEqualTo(afterCreate));
        });
    }
}
