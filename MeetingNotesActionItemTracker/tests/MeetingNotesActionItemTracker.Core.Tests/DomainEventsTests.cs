// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MeetingNotesActionItemTracker.Core.Tests;

public class DomainEventsTests
{
    [Test]
    public void ActionItemCompletedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var actionItemId = Guid.NewGuid();
        var completedDate = DateTime.UtcNow;
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new ActionItemCompletedEvent
        {
            ActionItemId = actionItemId,
            CompletedDate = completedDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ActionItemId, Is.EqualTo(actionItemId));
            Assert.That(evt.CompletedDate, Is.EqualTo(completedDate));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void ActionItemCompletedEvent_DefaultTimestamp_IsSet()
    {
        // Act
        var evt = new ActionItemCompletedEvent
        {
            ActionItemId = Guid.NewGuid(),
            CompletedDate = DateTime.UtcNow
        };

        // Assert
        Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
    }

    [Test]
    public void ActionItemCreatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var actionItemId = Guid.NewGuid();
        var meetingId = Guid.NewGuid();
        var description = "Complete project documentation";
        var priority = Priority.High;
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new ActionItemCreatedEvent
        {
            ActionItemId = actionItemId,
            MeetingId = meetingId,
            Description = description,
            Priority = priority,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ActionItemId, Is.EqualTo(actionItemId));
            Assert.That(evt.MeetingId, Is.EqualTo(meetingId));
            Assert.That(evt.Description, Is.EqualTo(description));
            Assert.That(evt.Priority, Is.EqualTo(priority));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void ActionItemCreatedEvent_DefaultTimestamp_IsSet()
    {
        // Act
        var evt = new ActionItemCreatedEvent
        {
            ActionItemId = Guid.NewGuid(),
            MeetingId = Guid.NewGuid(),
            Description = "Test task",
            Priority = Priority.Medium
        };

        // Assert
        Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
    }

    [Test]
    public void ActionItemCreatedEvent_AllPriorities_CanBeSet()
    {
        // Arrange
        var actionItemId = Guid.NewGuid();
        var meetingId = Guid.NewGuid();

        // Act & Assert - Low
        var evt1 = new ActionItemCreatedEvent
        {
            ActionItemId = actionItemId,
            MeetingId = meetingId,
            Description = "Test",
            Priority = Priority.Low
        };
        Assert.That(evt1.Priority, Is.EqualTo(Priority.Low));

        // Act & Assert - High
        var evt2 = new ActionItemCreatedEvent
        {
            ActionItemId = actionItemId,
            MeetingId = meetingId,
            Description = "Test",
            Priority = Priority.High
        };
        Assert.That(evt2.Priority, Is.EqualTo(Priority.High));

        // Act & Assert - Critical
        var evt3 = new ActionItemCreatedEvent
        {
            ActionItemId = actionItemId,
            MeetingId = meetingId,
            Description = "Test",
            Priority = Priority.Critical
        };
        Assert.That(evt3.Priority, Is.EqualTo(Priority.Critical));
    }

    [Test]
    public void MeetingCreatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var meetingId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var title = "Sprint Planning";
        var meetingDateTime = DateTime.UtcNow.AddDays(1);
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new MeetingCreatedEvent
        {
            MeetingId = meetingId,
            UserId = userId,
            Title = title,
            MeetingDateTime = meetingDateTime,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.MeetingId, Is.EqualTo(meetingId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Title, Is.EqualTo(title));
            Assert.That(evt.MeetingDateTime, Is.EqualTo(meetingDateTime));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void MeetingCreatedEvent_DefaultTimestamp_IsSet()
    {
        // Act
        var evt = new MeetingCreatedEvent
        {
            MeetingId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Team Meeting",
            MeetingDateTime = DateTime.UtcNow
        };

        // Assert
        Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
    }

    [Test]
    public void NoteAddedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var noteId = Guid.NewGuid();
        var meetingId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new NoteAddedEvent
        {
            NoteId = noteId,
            MeetingId = meetingId,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.NoteId, Is.EqualTo(noteId));
            Assert.That(evt.MeetingId, Is.EqualTo(meetingId));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void NoteAddedEvent_DefaultTimestamp_IsSet()
    {
        // Act
        var evt = new NoteAddedEvent
        {
            NoteId = Guid.NewGuid(),
            MeetingId = Guid.NewGuid()
        };

        // Assert
        Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
    }
}
