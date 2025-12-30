// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MeetingNotesActionItemTracker.Core.Tests;

public class ActionItemTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesActionItem()
    {
        // Arrange
        var actionItemId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var meetingId = Guid.NewGuid();
        var description = "Complete project documentation";
        var responsiblePerson = "John Doe";
        var dueDate = DateTime.UtcNow.AddDays(7);
        var priority = Priority.High;
        var status = ActionItemStatus.InProgress;
        var notes = "Important task";

        // Act
        var actionItem = new ActionItem
        {
            ActionItemId = actionItemId,
            UserId = userId,
            MeetingId = meetingId,
            Description = description,
            ResponsiblePerson = responsiblePerson,
            DueDate = dueDate,
            Priority = priority,
            Status = status,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actionItem.ActionItemId, Is.EqualTo(actionItemId));
            Assert.That(actionItem.UserId, Is.EqualTo(userId));
            Assert.That(actionItem.MeetingId, Is.EqualTo(meetingId));
            Assert.That(actionItem.Description, Is.EqualTo(description));
            Assert.That(actionItem.ResponsiblePerson, Is.EqualTo(responsiblePerson));
            Assert.That(actionItem.DueDate, Is.EqualTo(dueDate));
            Assert.That(actionItem.Priority, Is.EqualTo(priority));
            Assert.That(actionItem.Status, Is.EqualTo(status));
            Assert.That(actionItem.Notes, Is.EqualTo(notes));
            Assert.That(actionItem.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Constructor_DefaultValues_InitializesCorrectly()
    {
        // Act
        var actionItem = new ActionItem();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actionItem.Description, Is.EqualTo(string.Empty));
            Assert.That(actionItem.ResponsiblePerson, Is.Null);
            Assert.That(actionItem.DueDate, Is.Null);
            Assert.That(actionItem.Priority, Is.EqualTo(Priority.Low));
            Assert.That(actionItem.Status, Is.EqualTo(ActionItemStatus.NotStarted));
            Assert.That(actionItem.CompletedDate, Is.Null);
            Assert.That(actionItem.Notes, Is.Null);
            Assert.That(actionItem.UpdatedAt, Is.Null);
            Assert.That(actionItem.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Complete_WhenCalled_SetsStatusToCompletedAndDates()
    {
        // Arrange
        var actionItem = new ActionItem
        {
            ActionItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MeetingId = Guid.NewGuid(),
            Description = "Test task",
            Status = ActionItemStatus.InProgress
        };

        var beforeCall = DateTime.UtcNow;

        // Act
        actionItem.Complete();

        var afterCall = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actionItem.Status, Is.EqualTo(ActionItemStatus.Completed));
            Assert.That(actionItem.CompletedDate, Is.Not.Null);
            Assert.That(actionItem.CompletedDate!.Value, Is.GreaterThanOrEqualTo(beforeCall));
            Assert.That(actionItem.CompletedDate!.Value, Is.LessThanOrEqualTo(afterCall));
            Assert.That(actionItem.UpdatedAt, Is.Not.Null);
            Assert.That(actionItem.UpdatedAt!.Value, Is.GreaterThanOrEqualTo(beforeCall));
            Assert.That(actionItem.UpdatedAt!.Value, Is.LessThanOrEqualTo(afterCall));
        });
    }

    [Test]
    public void UpdateStatus_ToCompleted_SetsCompletedDateAndUpdatedAt()
    {
        // Arrange
        var actionItem = new ActionItem
        {
            ActionItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MeetingId = Guid.NewGuid(),
            Description = "Test task",
            Status = ActionItemStatus.InProgress
        };

        var beforeCall = DateTime.UtcNow;

        // Act
        actionItem.UpdateStatus(ActionItemStatus.Completed);

        var afterCall = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actionItem.Status, Is.EqualTo(ActionItemStatus.Completed));
            Assert.That(actionItem.CompletedDate, Is.Not.Null);
            Assert.That(actionItem.CompletedDate!.Value, Is.GreaterThanOrEqualTo(beforeCall));
            Assert.That(actionItem.CompletedDate!.Value, Is.LessThanOrEqualTo(afterCall));
            Assert.That(actionItem.UpdatedAt, Is.Not.Null);
        });
    }

    [Test]
    public void UpdateStatus_ToInProgress_DoesNotSetCompletedDate()
    {
        // Arrange
        var actionItem = new ActionItem
        {
            ActionItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MeetingId = Guid.NewGuid(),
            Description = "Test task",
            Status = ActionItemStatus.NotStarted,
            CompletedDate = null
        };

        // Act
        actionItem.UpdateStatus(ActionItemStatus.InProgress);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actionItem.Status, Is.EqualTo(ActionItemStatus.InProgress));
            Assert.That(actionItem.CompletedDate, Is.Null);
            Assert.That(actionItem.UpdatedAt, Is.Not.Null);
        });
    }

    [Test]
    public void UpdateStatus_ToOnHold_UpdatesStatusAndUpdatedAt()
    {
        // Arrange
        var actionItem = new ActionItem
        {
            ActionItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MeetingId = Guid.NewGuid(),
            Description = "Test task",
            Status = ActionItemStatus.InProgress
        };

        // Act
        actionItem.UpdateStatus(ActionItemStatus.OnHold);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actionItem.Status, Is.EqualTo(ActionItemStatus.OnHold));
            Assert.That(actionItem.UpdatedAt, Is.Not.Null);
        });
    }

    [Test]
    public void IsOverdue_WhenPastDueAndNotCompleted_ReturnsTrue()
    {
        // Arrange
        var actionItem = new ActionItem
        {
            ActionItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MeetingId = Guid.NewGuid(),
            Description = "Test task",
            DueDate = DateTime.UtcNow.AddDays(-1),
            Status = ActionItemStatus.InProgress
        };

        // Act
        var result = actionItem.IsOverdue();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsOverdue_WhenPastDueButCompleted_ReturnsFalse()
    {
        // Arrange
        var actionItem = new ActionItem
        {
            ActionItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MeetingId = Guid.NewGuid(),
            Description = "Test task",
            DueDate = DateTime.UtcNow.AddDays(-1),
            Status = ActionItemStatus.Completed
        };

        // Act
        var result = actionItem.IsOverdue();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsOverdue_WhenNoDueDate_ReturnsFalse()
    {
        // Arrange
        var actionItem = new ActionItem
        {
            ActionItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MeetingId = Guid.NewGuid(),
            Description = "Test task",
            DueDate = null,
            Status = ActionItemStatus.InProgress
        };

        // Act
        var result = actionItem.IsOverdue();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsOverdue_WhenDueDateInFuture_ReturnsFalse()
    {
        // Arrange
        var actionItem = new ActionItem
        {
            ActionItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MeetingId = Guid.NewGuid(),
            Description = "Test task",
            DueDate = DateTime.UtcNow.AddDays(5),
            Status = ActionItemStatus.InProgress
        };

        // Act
        var result = actionItem.IsOverdue();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void Meeting_CanBeAssociated_WithActionItem()
    {
        // Arrange
        var meeting = new Meeting
        {
            MeetingId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Sprint Planning",
            MeetingDateTime = DateTime.UtcNow
        };

        var actionItem = new ActionItem
        {
            ActionItemId = Guid.NewGuid(),
            UserId = meeting.UserId,
            MeetingId = meeting.MeetingId,
            Description = "Test task",
            Meeting = meeting
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actionItem.MeetingId, Is.EqualTo(meeting.MeetingId));
            Assert.That(actionItem.Meeting, Is.Not.Null);
            Assert.That(actionItem.Meeting.Title, Is.EqualTo("Sprint Planning"));
        });
    }

    [Test]
    public void Priority_CanBeSetToAllValues()
    {
        // Arrange
        var actionItem = new ActionItem
        {
            ActionItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MeetingId = Guid.NewGuid(),
            Description = "Test task"
        };

        // Act & Assert
        actionItem.Priority = Priority.Low;
        Assert.That(actionItem.Priority, Is.EqualTo(Priority.Low));

        actionItem.Priority = Priority.Medium;
        Assert.That(actionItem.Priority, Is.EqualTo(Priority.Medium));

        actionItem.Priority = Priority.High;
        Assert.That(actionItem.Priority, Is.EqualTo(Priority.High));

        actionItem.Priority = Priority.Critical;
        Assert.That(actionItem.Priority, Is.EqualTo(Priority.Critical));
    }

    [Test]
    public void Status_CanBeSetToAllValues()
    {
        // Arrange
        var actionItem = new ActionItem
        {
            ActionItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MeetingId = Guid.NewGuid(),
            Description = "Test task"
        };

        // Act & Assert
        actionItem.Status = ActionItemStatus.NotStarted;
        Assert.That(actionItem.Status, Is.EqualTo(ActionItemStatus.NotStarted));

        actionItem.Status = ActionItemStatus.InProgress;
        Assert.That(actionItem.Status, Is.EqualTo(ActionItemStatus.InProgress));

        actionItem.Status = ActionItemStatus.Completed;
        Assert.That(actionItem.Status, Is.EqualTo(ActionItemStatus.Completed));

        actionItem.Status = ActionItemStatus.Cancelled;
        Assert.That(actionItem.Status, Is.EqualTo(ActionItemStatus.Cancelled));

        actionItem.Status = ActionItemStatus.OnHold;
        Assert.That(actionItem.Status, Is.EqualTo(ActionItemStatus.OnHold));
    }
}
