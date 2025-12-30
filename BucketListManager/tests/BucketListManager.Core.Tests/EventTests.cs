// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BucketListManager.Core.Tests;

public class EventTests
{
    [Test]
    public void BucketListItemCreatedEvent_CreatesEventWithCorrectProperties()
    {
        // Arrange
        var itemId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var beforeCreate = DateTime.UtcNow;

        // Act
        var evt = new BucketListItemCreatedEvent
        {
            BucketListItemId = itemId,
            UserId = userId,
            Title = "Skydive",
            Category = Category.Adventure,
            Priority = Priority.Critical
        };
        var afterCreate = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.BucketListItemId, Is.EqualTo(itemId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Title, Is.EqualTo("Skydive"));
            Assert.That(evt.Category, Is.EqualTo(Category.Adventure));
            Assert.That(evt.Priority, Is.EqualTo(Priority.Critical));
            Assert.That(evt.Timestamp, Is.GreaterThanOrEqualTo(beforeCreate));
            Assert.That(evt.Timestamp, Is.LessThanOrEqualTo(afterCreate));
        });
    }

    [Test]
    public void BucketListItemCompletedEvent_CreatesEventWithCorrectProperties()
    {
        // Arrange
        var itemId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var completedDate = new DateTime(2024, 12, 25);

        // Act
        var evt = new BucketListItemCompletedEvent
        {
            BucketListItemId = itemId,
            UserId = userId,
            CompletedDate = completedDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.BucketListItemId, Is.EqualTo(itemId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.CompletedDate, Is.EqualTo(completedDate));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void MemoryCreatedEvent_CreatesEventWithCorrectProperties()
    {
        // Arrange
        var memoryId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var itemId = Guid.NewGuid();

        // Act
        var evt = new MemoryCreatedEvent
        {
            MemoryId = memoryId,
            UserId = userId,
            BucketListItemId = itemId,
            Title = "Amazing photos from Paris"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.MemoryId, Is.EqualTo(memoryId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.BucketListItemId, Is.EqualTo(itemId));
            Assert.That(evt.Title, Is.EqualTo("Amazing photos from Paris"));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void MemoryUpdatedEvent_CreatesEventWithCorrectProperties()
    {
        // Arrange
        var memoryId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        // Act
        var evt = new MemoryUpdatedEvent
        {
            MemoryId = memoryId,
            UserId = userId
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.MemoryId, Is.EqualTo(memoryId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void MilestoneCreatedEvent_CreatesEventWithCorrectProperties()
    {
        // Arrange
        var milestoneId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var itemId = Guid.NewGuid();

        // Act
        var evt = new MilestoneCreatedEvent
        {
            MilestoneId = milestoneId,
            UserId = userId,
            BucketListItemId = itemId,
            Title = "Complete training"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.MilestoneId, Is.EqualTo(milestoneId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.BucketListItemId, Is.EqualTo(itemId));
            Assert.That(evt.Title, Is.EqualTo("Complete training"));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void MilestoneCompletedEvent_CreatesEventWithCorrectProperties()
    {
        // Arrange
        var milestoneId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var itemId = Guid.NewGuid();
        var completedDate = new DateTime(2024, 8, 15);

        // Act
        var evt = new MilestoneCompletedEvent
        {
            MilestoneId = milestoneId,
            UserId = userId,
            BucketListItemId = itemId,
            CompletedDate = completedDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.MilestoneId, Is.EqualTo(milestoneId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.BucketListItemId, Is.EqualTo(itemId));
            Assert.That(evt.CompletedDate, Is.EqualTo(completedDate));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }
}
