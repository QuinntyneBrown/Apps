// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MeetingNotesActionItemTracker.Api.Tests;

[TestFixture]
public class DtoMappingTests
{
    [Test]
    public void Meeting_ToDto_MapsAllProperties()
    {
        // Arrange
        var meeting = new Meeting
        {
            MeetingId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Meeting",
            MeetingDateTime = DateTime.UtcNow,
            DurationMinutes = 60,
            Location = "Conference Room A",
            Attendees = new List<string> { "John", "Jane" },
            Agenda = "Discuss project",
            Summary = "Meeting summary",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // Act
        var dto = meeting.ToDto();

        // Assert
        Assert.That(dto.MeetingId, Is.EqualTo(meeting.MeetingId));
        Assert.That(dto.UserId, Is.EqualTo(meeting.UserId));
        Assert.That(dto.Title, Is.EqualTo(meeting.Title));
        Assert.That(dto.MeetingDateTime, Is.EqualTo(meeting.MeetingDateTime));
        Assert.That(dto.DurationMinutes, Is.EqualTo(meeting.DurationMinutes));
        Assert.That(dto.Location, Is.EqualTo(meeting.Location));
        Assert.That(dto.Attendees, Is.EqualTo(meeting.Attendees));
        Assert.That(dto.Agenda, Is.EqualTo(meeting.Agenda));
        Assert.That(dto.Summary, Is.EqualTo(meeting.Summary));
        Assert.That(dto.CreatedAt, Is.EqualTo(meeting.CreatedAt));
        Assert.That(dto.UpdatedAt, Is.EqualTo(meeting.UpdatedAt));
    }

    [Test]
    public void Note_ToDto_MapsAllProperties()
    {
        // Arrange
        var note = new Note
        {
            NoteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MeetingId = Guid.NewGuid(),
            Content = "Test note content",
            Category = "Important",
            IsImportant = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // Act
        var dto = note.ToDto();

        // Assert
        Assert.That(dto.NoteId, Is.EqualTo(note.NoteId));
        Assert.That(dto.UserId, Is.EqualTo(note.UserId));
        Assert.That(dto.MeetingId, Is.EqualTo(note.MeetingId));
        Assert.That(dto.Content, Is.EqualTo(note.Content));
        Assert.That(dto.Category, Is.EqualTo(note.Category));
        Assert.That(dto.IsImportant, Is.EqualTo(note.IsImportant));
        Assert.That(dto.CreatedAt, Is.EqualTo(note.CreatedAt));
        Assert.That(dto.UpdatedAt, Is.EqualTo(note.UpdatedAt));
    }

    [Test]
    public void ActionItem_ToDto_MapsAllProperties()
    {
        // Arrange
        var actionItem = new Core.ActionItem
        {
            ActionItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MeetingId = Guid.NewGuid(),
            Description = "Complete task",
            ResponsiblePerson = "John Doe",
            DueDate = DateTime.UtcNow.AddDays(7),
            Priority = Priority.High,
            Status = ActionItemStatus.InProgress,
            CompletedDate = null,
            Notes = "Some notes",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // Act
        var dto = actionItem.ToDto();

        // Assert
        Assert.That(dto.ActionItemId, Is.EqualTo(actionItem.ActionItemId));
        Assert.That(dto.UserId, Is.EqualTo(actionItem.UserId));
        Assert.That(dto.MeetingId, Is.EqualTo(actionItem.MeetingId));
        Assert.That(dto.Description, Is.EqualTo(actionItem.Description));
        Assert.That(dto.ResponsiblePerson, Is.EqualTo(actionItem.ResponsiblePerson));
        Assert.That(dto.DueDate, Is.EqualTo(actionItem.DueDate));
        Assert.That(dto.Priority, Is.EqualTo(actionItem.Priority));
        Assert.That(dto.Status, Is.EqualTo(actionItem.Status));
        Assert.That(dto.CompletedDate, Is.EqualTo(actionItem.CompletedDate));
        Assert.That(dto.Notes, Is.EqualTo(actionItem.Notes));
        Assert.That(dto.CreatedAt, Is.EqualTo(actionItem.CreatedAt));
        Assert.That(dto.UpdatedAt, Is.EqualTo(actionItem.UpdatedAt));
    }
}
