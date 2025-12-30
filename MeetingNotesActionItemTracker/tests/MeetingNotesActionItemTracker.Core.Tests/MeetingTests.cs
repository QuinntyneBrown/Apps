// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MeetingNotesActionItemTracker.Core.Tests;

public class MeetingTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesMeeting()
    {
        // Arrange
        var meetingId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var title = "Sprint Planning Meeting";
        var meetingDateTime = DateTime.UtcNow.AddDays(1);
        var durationMinutes = 60;
        var location = "Conference Room A";
        var attendees = new List<string> { "John", "Jane", "Bob" };
        var agenda = "Review sprint goals";
        var summary = "Productive meeting";

        // Act
        var meeting = new Meeting
        {
            MeetingId = meetingId,
            UserId = userId,
            Title = title,
            MeetingDateTime = meetingDateTime,
            DurationMinutes = durationMinutes,
            Location = location,
            Attendees = attendees,
            Agenda = agenda,
            Summary = summary
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(meeting.MeetingId, Is.EqualTo(meetingId));
            Assert.That(meeting.UserId, Is.EqualTo(userId));
            Assert.That(meeting.Title, Is.EqualTo(title));
            Assert.That(meeting.MeetingDateTime, Is.EqualTo(meetingDateTime));
            Assert.That(meeting.DurationMinutes, Is.EqualTo(durationMinutes));
            Assert.That(meeting.Location, Is.EqualTo(location));
            Assert.That(meeting.Attendees, Is.EquivalentTo(attendees));
            Assert.That(meeting.Agenda, Is.EqualTo(agenda));
            Assert.That(meeting.Summary, Is.EqualTo(summary));
            Assert.That(meeting.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Constructor_DefaultValues_InitializesCorrectly()
    {
        // Act
        var meeting = new Meeting();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(meeting.Title, Is.EqualTo(string.Empty));
            Assert.That(meeting.DurationMinutes, Is.Null);
            Assert.That(meeting.Location, Is.Null);
            Assert.That(meeting.Attendees, Is.Not.Null);
            Assert.That(meeting.Attendees.Count, Is.EqualTo(0));
            Assert.That(meeting.Agenda, Is.Null);
            Assert.That(meeting.Summary, Is.Null);
            Assert.That(meeting.UpdatedAt, Is.Null);
            Assert.That(meeting.Notes, Is.Not.Null);
            Assert.That(meeting.Notes.Count, Is.EqualTo(0));
            Assert.That(meeting.ActionItems, Is.Not.Null);
            Assert.That(meeting.ActionItems.Count, Is.EqualTo(0));
            Assert.That(meeting.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void AddAttendee_NewAttendee_AddsToList()
    {
        // Arrange
        var meeting = new Meeting
        {
            MeetingId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Team Meeting",
            MeetingDateTime = DateTime.UtcNow
        };

        // Act
        meeting.AddAttendee("John Doe");

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(meeting.Attendees.Count, Is.EqualTo(1));
            Assert.That(meeting.Attendees, Contains.Item("John Doe"));
            Assert.That(meeting.UpdatedAt, Is.Not.Null);
        });
    }

    [Test]
    public void AddAttendee_DuplicateAttendee_DoesNotAddAgain()
    {
        // Arrange
        var meeting = new Meeting
        {
            MeetingId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Team Meeting",
            MeetingDateTime = DateTime.UtcNow
        };

        meeting.AddAttendee("John Doe");

        // Act
        meeting.AddAttendee("John Doe");

        // Assert
        Assert.That(meeting.Attendees.Count, Is.EqualTo(1));
    }

    [Test]
    public void AddAttendee_DuplicateAttendeeDifferentCase_DoesNotAddAgain()
    {
        // Arrange
        var meeting = new Meeting
        {
            MeetingId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Team Meeting",
            MeetingDateTime = DateTime.UtcNow
        };

        meeting.AddAttendee("John Doe");

        // Act
        meeting.AddAttendee("JOHN DOE");

        // Assert
        Assert.That(meeting.Attendees.Count, Is.EqualTo(1));
    }

    [Test]
    public void AddAttendee_MultipleAttendees_AddsAll()
    {
        // Arrange
        var meeting = new Meeting
        {
            MeetingId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Team Meeting",
            MeetingDateTime = DateTime.UtcNow
        };

        // Act
        meeting.AddAttendee("John Doe");
        meeting.AddAttendee("Jane Smith");
        meeting.AddAttendee("Bob Johnson");

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(meeting.Attendees.Count, Is.EqualTo(3));
            Assert.That(meeting.Attendees, Contains.Item("John Doe"));
            Assert.That(meeting.Attendees, Contains.Item("Jane Smith"));
            Assert.That(meeting.Attendees, Contains.Item("Bob Johnson"));
        });
    }

    [Test]
    public void Notes_CanAddNotes_ToCollection()
    {
        // Arrange
        var meeting = new Meeting
        {
            MeetingId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Team Meeting",
            MeetingDateTime = DateTime.UtcNow
        };

        var note = new Note
        {
            NoteId = Guid.NewGuid(),
            UserId = meeting.UserId,
            MeetingId = meeting.MeetingId,
            Content = "Important discussion point"
        };

        // Act
        meeting.Notes.Add(note);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(meeting.Notes.Count, Is.EqualTo(1));
            Assert.That(meeting.Notes.First().NoteId, Is.EqualTo(note.NoteId));
        });
    }

    [Test]
    public void ActionItems_CanAddActionItems_ToCollection()
    {
        // Arrange
        var meeting = new Meeting
        {
            MeetingId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Team Meeting",
            MeetingDateTime = DateTime.UtcNow
        };

        var actionItem = new ActionItem
        {
            ActionItemId = Guid.NewGuid(),
            UserId = meeting.UserId,
            MeetingId = meeting.MeetingId,
            Description = "Follow up on action"
        };

        // Act
        meeting.ActionItems.Add(actionItem);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(meeting.ActionItems.Count, Is.EqualTo(1));
            Assert.That(meeting.ActionItems.First().ActionItemId, Is.EqualTo(actionItem.ActionItemId));
        });
    }

    [Test]
    public void DurationMinutes_CanBeSetToPositiveValue()
    {
        // Arrange
        var meeting = new Meeting
        {
            MeetingId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Team Meeting",
            MeetingDateTime = DateTime.UtcNow
        };

        // Act
        meeting.DurationMinutes = 90;

        // Assert
        Assert.That(meeting.DurationMinutes, Is.EqualTo(90));
    }

    [Test]
    public void Location_CanBeSetToDifferentTypes()
    {
        // Arrange
        var meeting = new Meeting
        {
            MeetingId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Team Meeting",
            MeetingDateTime = DateTime.UtcNow
        };

        // Act & Assert - Physical location
        meeting.Location = "Conference Room A";
        Assert.That(meeting.Location, Is.EqualTo("Conference Room A"));

        // Act & Assert - Virtual location
        meeting.Location = "https://zoom.us/meeting/123";
        Assert.That(meeting.Location, Is.EqualTo("https://zoom.us/meeting/123"));
    }

    [Test]
    public void UpdatedAt_CanBeSet()
    {
        // Arrange
        var meeting = new Meeting
        {
            MeetingId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Team Meeting",
            MeetingDateTime = DateTime.UtcNow
        };

        var updateTime = DateTime.UtcNow;

        // Act
        meeting.UpdatedAt = updateTime;

        // Assert
        Assert.That(meeting.UpdatedAt, Is.EqualTo(updateTime));
    }
}
