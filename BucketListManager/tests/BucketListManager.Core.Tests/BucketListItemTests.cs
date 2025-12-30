// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BucketListManager.Core.Tests;

public class BucketListItemTests
{
    [Test]
    public void Constructor_DefaultValues_SetsPropertiesCorrectly()
    {
        // Arrange & Act
        var item = new BucketListItem
        {
            BucketListItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Visit Paris",
            Description = "See the Eiffel Tower",
            Category = Category.Travel,
            Priority = Priority.High,
            Status = ItemStatus.NotStarted
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(item.BucketListItemId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(item.UserId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(item.Title, Is.EqualTo("Visit Paris"));
            Assert.That(item.Description, Is.EqualTo("See the Eiffel Tower"));
            Assert.That(item.Category, Is.EqualTo(Category.Travel));
            Assert.That(item.Priority, Is.EqualTo(Priority.High));
            Assert.That(item.Status, Is.EqualTo(ItemStatus.NotStarted));
            Assert.That(item.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(item.Milestones, Is.Not.Null);
            Assert.That(item.Memories, Is.Not.Null);
        });
    }

    [Test]
    public void BucketListItem_WithTargetDate_StoresDateCorrectly()
    {
        // Arrange
        var targetDate = new DateTime(2025, 12, 31);

        // Act
        var item = new BucketListItem
        {
            TargetDate = targetDate
        };

        // Assert
        Assert.That(item.TargetDate, Is.EqualTo(targetDate));
    }

    [Test]
    public void BucketListItem_WithCompletedDate_StoresDateCorrectly()
    {
        // Arrange
        var completedDate = new DateTime(2024, 6, 15);

        // Act
        var item = new BucketListItem
        {
            CompletedDate = completedDate,
            Status = ItemStatus.Completed
        };

        // Assert
        Assert.That(item.CompletedDate, Is.EqualTo(completedDate));
    }

    [Test]
    public void BucketListItem_WithNotes_StoresNotesCorrectly()
    {
        // Arrange & Act
        var item = new BucketListItem
        {
            Notes = "This is a lifelong dream"
        };

        // Assert
        Assert.That(item.Notes, Is.EqualTo("This is a lifelong dream"));
    }

    [Test]
    public void BucketListItem_AllCategoriesCanBeAssigned()
    {
        // Arrange & Act & Assert
        var categories = Enum.GetValues<Category>();
        foreach (var category in categories)
        {
            var item = new BucketListItem { Category = category };
            Assert.That(item.Category, Is.EqualTo(category));
        }
    }

    [Test]
    public void BucketListItem_AllPrioritiesCanBeAssigned()
    {
        // Arrange & Act & Assert
        var priorities = Enum.GetValues<Priority>();
        foreach (var priority in priorities)
        {
            var item = new BucketListItem { Priority = priority };
            Assert.That(item.Priority, Is.EqualTo(priority));
        }
    }

    [Test]
    public void BucketListItem_AllStatusesCanBeAssigned()
    {
        // Arrange & Act & Assert
        var statuses = Enum.GetValues<ItemStatus>();
        foreach (var status in statuses)
        {
            var item = new BucketListItem { Status = status };
            Assert.That(item.Status, Is.EqualTo(status));
        }
    }

    [Test]
    public void BucketListItem_CanAddMultipleMilestones()
    {
        // Arrange
        var item = new BucketListItem();
        var milestone1 = new Milestone { MilestoneId = Guid.NewGuid() };
        var milestone2 = new Milestone { MilestoneId = Guid.NewGuid() };

        // Act
        item.Milestones.Add(milestone1);
        item.Milestones.Add(milestone2);

        // Assert
        Assert.That(item.Milestones, Has.Count.EqualTo(2));
    }

    [Test]
    public void BucketListItem_CanAddMultipleMemories()
    {
        // Arrange
        var item = new BucketListItem();
        var memory1 = new Memory { MemoryId = Guid.NewGuid() };
        var memory2 = new Memory { MemoryId = Guid.NewGuid() };

        // Act
        item.Memories.Add(memory1);
        item.Memories.Add(memory2);

        // Assert
        Assert.That(item.Memories, Has.Count.EqualTo(2));
    }

    [Test]
    public void BucketListItem_StatusInProgress_IsValid()
    {
        // Arrange & Act
        var item = new BucketListItem
        {
            Status = ItemStatus.InProgress
        };

        // Assert
        Assert.That(item.Status, Is.EqualTo(ItemStatus.InProgress));
    }

    [Test]
    public void BucketListItem_StatusOnHold_IsValid()
    {
        // Arrange & Act
        var item = new BucketListItem
        {
            Status = ItemStatus.OnHold
        };

        // Assert
        Assert.That(item.Status, Is.EqualTo(ItemStatus.OnHold));
    }

    [Test]
    public void BucketListItem_StatusCancelled_IsValid()
    {
        // Arrange & Act
        var item = new BucketListItem
        {
            Status = ItemStatus.Cancelled
        };

        // Assert
        Assert.That(item.Status, Is.EqualTo(ItemStatus.Cancelled));
    }
}
