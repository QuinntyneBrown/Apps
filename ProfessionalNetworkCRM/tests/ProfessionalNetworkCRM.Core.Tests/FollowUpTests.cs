// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalNetworkCRM.Core.Tests;

public class FollowUpTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesFollowUp()
    {
        // Arrange & Act
        var followUp = new FollowUp();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(followUp.FollowUpId, Is.EqualTo(Guid.Empty));
            Assert.That(followUp.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(followUp.ContactId, Is.EqualTo(Guid.Empty));
            Assert.That(followUp.Description, Is.EqualTo(string.Empty));
            Assert.That(followUp.DueDate, Is.EqualTo(default(DateTime)));
            Assert.That(followUp.IsCompleted, Is.False);
            Assert.That(followUp.CompletedDate, Is.Null);
            Assert.That(followUp.Priority, Is.EqualTo("Medium"));
            Assert.That(followUp.Notes, Is.Null);
            Assert.That(followUp.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
            Assert.That(followUp.UpdatedAt, Is.Null);
            Assert.That(followUp.Contact, Is.Null);
        });
    }

    [Test]
    public void Constructor_WithValidParameters_CreatesFollowUp()
    {
        // Arrange
        var followUpId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var contactId = Guid.NewGuid();
        var description = "Schedule follow-up meeting";
        var dueDate = new DateTime(2024, 7, 1, 10, 0, 0);
        var priority = "High";

        // Act
        var followUp = new FollowUp
        {
            FollowUpId = followUpId,
            UserId = userId,
            ContactId = contactId,
            Description = description,
            DueDate = dueDate,
            Priority = priority
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(followUp.FollowUpId, Is.EqualTo(followUpId));
            Assert.That(followUp.UserId, Is.EqualTo(userId));
            Assert.That(followUp.ContactId, Is.EqualTo(contactId));
            Assert.That(followUp.Description, Is.EqualTo(description));
            Assert.That(followUp.DueDate, Is.EqualTo(dueDate));
            Assert.That(followUp.Priority, Is.EqualTo(priority));
        });
    }

    [Test]
    public void Complete_PendingFollowUp_MarksAsCompleted()
    {
        // Arrange
        var followUp = new FollowUp
        {
            Description = "Send thank you email",
            DueDate = DateTime.UtcNow.AddDays(1),
            IsCompleted = false
        };
        var beforeComplete = DateTime.UtcNow;

        // Act
        followUp.Complete();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(followUp.IsCompleted, Is.True);
            Assert.That(followUp.CompletedDate, Is.Not.Null);
            Assert.That(followUp.CompletedDate.Value, Is.GreaterThanOrEqualTo(beforeComplete));
            Assert.That(followUp.CompletedDate.Value, Is.LessThanOrEqualTo(DateTime.UtcNow));
            Assert.That(followUp.UpdatedAt, Is.Not.Null);
            Assert.That(followUp.UpdatedAt.Value, Is.GreaterThanOrEqualTo(beforeComplete));
        });
    }

    [Test]
    public void Complete_AlreadyCompleted_UpdatesTimestamp()
    {
        // Arrange
        var followUp = new FollowUp
        {
            Description = "Follow up on proposal",
            DueDate = DateTime.UtcNow.AddDays(-1),
            IsCompleted = true,
            CompletedDate = DateTime.UtcNow.AddDays(-1)
        };
        var previousCompletedDate = followUp.CompletedDate;

        // Act
        Thread.Sleep(10); // Ensure time difference
        followUp.Complete();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(followUp.IsCompleted, Is.True);
            Assert.That(followUp.CompletedDate, Is.Not.EqualTo(previousCompletedDate));
            Assert.That(followUp.CompletedDate, Is.GreaterThan(previousCompletedDate));
        });
    }

    [Test]
    public void Reschedule_ValidDate_UpdatesDueDateAndTimestamp()
    {
        // Arrange
        var followUp = new FollowUp
        {
            Description = "Quarterly check-in",
            DueDate = new DateTime(2024, 7, 1)
        };
        var newDueDate = new DateTime(2024, 7, 15);
        var beforeReschedule = DateTime.UtcNow;

        // Act
        followUp.Reschedule(newDueDate);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(followUp.DueDate, Is.EqualTo(newDueDate));
            Assert.That(followUp.UpdatedAt, Is.Not.Null);
            Assert.That(followUp.UpdatedAt.Value, Is.GreaterThanOrEqualTo(beforeReschedule));
            Assert.That(followUp.UpdatedAt.Value, Is.LessThanOrEqualTo(DateTime.UtcNow));
        });
    }

    [Test]
    public void Reschedule_EarlierDate_UpdatesCorrectly()
    {
        // Arrange
        var followUp = new FollowUp
        {
            Description = "Review contract",
            DueDate = new DateTime(2024, 7, 15)
        };
        var newDueDate = new DateTime(2024, 7, 1);

        // Act
        followUp.Reschedule(newDueDate);

        // Assert
        Assert.That(followUp.DueDate, Is.EqualTo(newDueDate));
    }

    [Test]
    public void Reschedule_LaterDate_UpdatesCorrectly()
    {
        // Arrange
        var followUp = new FollowUp
        {
            Description = "Annual review",
            DueDate = new DateTime(2024, 7, 1)
        };
        var newDueDate = new DateTime(2024, 12, 31);

        // Act
        followUp.Reschedule(newDueDate);

        // Assert
        Assert.That(followUp.DueDate, Is.EqualTo(newDueDate));
    }

    [Test]
    public void IsOverdue_PastDueAndNotCompleted_ReturnsTrue()
    {
        // Arrange
        var followUp = new FollowUp
        {
            Description = "Overdue task",
            DueDate = DateTime.UtcNow.AddDays(-1),
            IsCompleted = false
        };

        // Act
        var isOverdue = followUp.IsOverdue();

        // Assert
        Assert.That(isOverdue, Is.True);
    }

    [Test]
    public void IsOverdue_FutureDueAndNotCompleted_ReturnsFalse()
    {
        // Arrange
        var followUp = new FollowUp
        {
            Description = "Future task",
            DueDate = DateTime.UtcNow.AddDays(1),
            IsCompleted = false
        };

        // Act
        var isOverdue = followUp.IsOverdue();

        // Assert
        Assert.That(isOverdue, Is.False);
    }

    [Test]
    public void IsOverdue_PastDueButCompleted_ReturnsFalse()
    {
        // Arrange
        var followUp = new FollowUp
        {
            Description = "Completed task",
            DueDate = DateTime.UtcNow.AddDays(-1),
            IsCompleted = true,
            CompletedDate = DateTime.UtcNow
        };

        // Act
        var isOverdue = followUp.IsOverdue();

        // Assert
        Assert.That(isOverdue, Is.False);
    }

    [Test]
    public void IsOverdue_TodaysDueAndNotCompleted_ReturnsFalse()
    {
        // Arrange
        var followUp = new FollowUp
        {
            Description = "Today's task",
            DueDate = DateTime.UtcNow.AddHours(1),
            IsCompleted = false
        };

        // Act
        var isOverdue = followUp.IsOverdue();

        // Assert
        Assert.That(isOverdue, Is.False);
    }

    [Test]
    public void Priority_HighPriority_SetsCorrectly()
    {
        // Arrange & Act
        var followUp = new FollowUp
        {
            Description = "Urgent follow-up",
            Priority = "High"
        };

        // Assert
        Assert.That(followUp.Priority, Is.EqualTo("High"));
    }

    [Test]
    public void Priority_LowPriority_SetsCorrectly()
    {
        // Arrange & Act
        var followUp = new FollowUp
        {
            Description = "Low priority task",
            Priority = "Low"
        };

        // Assert
        Assert.That(followUp.Priority, Is.EqualTo("Low"));
    }

    [Test]
    public void FollowUp_WithNotes_StoresNotesCorrectly()
    {
        // Arrange & Act
        var followUp = new FollowUp
        {
            Description = "Call John",
            Notes = "Discuss Q3 results and next steps"
        };

        // Assert
        Assert.That(followUp.Notes, Is.EqualTo("Discuss Q3 results and next steps"));
    }

    [Test]
    public void FollowUp_WithAllProperties_SetsAllCorrectly()
    {
        // Arrange & Act
        var followUp = new FollowUp
        {
            FollowUpId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ContactId = Guid.NewGuid(),
            Description = "Comprehensive follow-up",
            DueDate = new DateTime(2024, 8, 1),
            Priority = "High",
            Notes = "Important client meeting preparation",
            IsCompleted = false
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(followUp.Description, Is.EqualTo("Comprehensive follow-up"));
            Assert.That(followUp.DueDate, Is.EqualTo(new DateTime(2024, 8, 1)));
            Assert.That(followUp.Priority, Is.EqualTo("High"));
            Assert.That(followUp.Notes, Is.EqualTo("Important client meeting preparation"));
            Assert.That(followUp.IsCompleted, Is.False);
        });
    }
}
