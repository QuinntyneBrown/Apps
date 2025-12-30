// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BucketListManager.Core.Tests;

public class MilestoneTests
{
    [Test]
    public void Constructor_DefaultValues_SetsPropertiesCorrectly()
    {
        // Arrange & Act
        var milestone = new Milestone
        {
            MilestoneId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            BucketListItemId = Guid.NewGuid(),
            Title = "Book flights",
            Description = "Research and book the best flights"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(milestone.MilestoneId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(milestone.UserId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(milestone.BucketListItemId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(milestone.Title, Is.EqualTo("Book flights"));
            Assert.That(milestone.Description, Is.EqualTo("Research and book the best flights"));
            Assert.That(milestone.IsCompleted, Is.False);
            Assert.That(milestone.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Milestone_WhenCompleted_StoresCompletedDateCorrectly()
    {
        // Arrange
        var completedDate = new DateTime(2024, 6, 15);

        // Act
        var milestone = new Milestone
        {
            IsCompleted = true,
            CompletedDate = completedDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(milestone.IsCompleted, Is.True);
            Assert.That(milestone.CompletedDate, Is.EqualTo(completedDate));
        });
    }

    [Test]
    public void Milestone_WhenNotCompleted_CompletedDateIsNull()
    {
        // Arrange & Act
        var milestone = new Milestone
        {
            IsCompleted = false,
            CompletedDate = null
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(milestone.IsCompleted, Is.False);
            Assert.That(milestone.CompletedDate, Is.Null);
        });
    }

    [Test]
    public void Milestone_WithDescription_StoresDescriptionCorrectly()
    {
        // Arrange & Act
        var milestone = new Milestone
        {
            Description = "This is an important step"
        };

        // Assert
        Assert.That(milestone.Description, Is.EqualTo("This is an important step"));
    }

    [Test]
    public void Milestone_WithNullDescription_AllowsNull()
    {
        // Arrange & Act
        var milestone = new Milestone
        {
            Description = null
        };

        // Assert
        Assert.That(milestone.Description, Is.Null);
    }

    [Test]
    public void Milestone_WithBucketListItem_AssociatesItemCorrectly()
    {
        // Arrange
        var item = new BucketListItem
        {
            BucketListItemId = Guid.NewGuid(),
            Title = "Run a marathon"
        };
        var milestone = new Milestone
        {
            BucketListItemId = item.BucketListItemId,
            BucketListItem = item
        };

        // Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(milestone.BucketListItem, Is.Not.Null);
            Assert.That(milestone.BucketListItem.BucketListItemId, Is.EqualTo(item.BucketListItemId));
            Assert.That(milestone.BucketListItem.Title, Is.EqualTo("Run a marathon"));
        });
    }

    [Test]
    public void Milestone_WithNullBucketListItem_AllowsNull()
    {
        // Arrange & Act
        var milestone = new Milestone
        {
            BucketListItemId = Guid.NewGuid(),
            BucketListItem = null
        };

        // Assert
        Assert.That(milestone.BucketListItem, Is.Null);
    }

    [Test]
    public void Milestone_DefaultIsCompleted_IsFalse()
    {
        // Arrange & Act
        var milestone = new Milestone();

        // Assert
        Assert.That(milestone.IsCompleted, Is.False);
    }

    [Test]
    public void Milestone_CanBeMarkedAsCompleted()
    {
        // Arrange
        var milestone = new Milestone
        {
            IsCompleted = false
        };

        // Act
        milestone.IsCompleted = true;

        // Assert
        Assert.That(milestone.IsCompleted, Is.True);
    }

    [Test]
    public void Milestone_CanBeMarkedAsIncomplete()
    {
        // Arrange
        var milestone = new Milestone
        {
            IsCompleted = true
        };

        // Act
        milestone.IsCompleted = false;

        // Assert
        Assert.That(milestone.IsCompleted, Is.False);
    }
}
