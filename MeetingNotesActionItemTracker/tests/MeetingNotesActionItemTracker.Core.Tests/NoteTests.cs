// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MeetingNotesActionItemTracker.Core.Tests;

public class NoteTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesNote()
    {
        // Arrange
        var noteId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var meetingId = Guid.NewGuid();
        var content = "This is an important discussion point";
        var category = "Decisions";
        var isImportant = true;

        // Act
        var note = new Note
        {
            NoteId = noteId,
            UserId = userId,
            MeetingId = meetingId,
            Content = content,
            Category = category,
            IsImportant = isImportant
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(note.NoteId, Is.EqualTo(noteId));
            Assert.That(note.UserId, Is.EqualTo(userId));
            Assert.That(note.MeetingId, Is.EqualTo(meetingId));
            Assert.That(note.Content, Is.EqualTo(content));
            Assert.That(note.Category, Is.EqualTo(category));
            Assert.That(note.IsImportant, Is.True);
            Assert.That(note.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Constructor_DefaultValues_InitializesCorrectly()
    {
        // Act
        var note = new Note();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(note.Content, Is.EqualTo(string.Empty));
            Assert.That(note.Category, Is.Null);
            Assert.That(note.IsImportant, Is.False);
            Assert.That(note.UpdatedAt, Is.Null);
            Assert.That(note.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void ToggleImportant_WhenFalse_SetsToTrue()
    {
        // Arrange
        var note = new Note
        {
            NoteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MeetingId = Guid.NewGuid(),
            Content = "Test note",
            IsImportant = false
        };

        // Act
        note.ToggleImportant();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(note.IsImportant, Is.True);
            Assert.That(note.UpdatedAt, Is.Not.Null);
        });
    }

    [Test]
    public void ToggleImportant_WhenTrue_SetsToFalse()
    {
        // Arrange
        var note = new Note
        {
            NoteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MeetingId = Guid.NewGuid(),
            Content = "Test note",
            IsImportant = true
        };

        // Act
        note.ToggleImportant();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(note.IsImportant, Is.False);
            Assert.That(note.UpdatedAt, Is.Not.Null);
        });
    }

    [Test]
    public void ToggleImportant_CalledTwice_ReturnsToOriginalState()
    {
        // Arrange
        var note = new Note
        {
            NoteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MeetingId = Guid.NewGuid(),
            Content = "Test note",
            IsImportant = false
        };

        // Act
        note.ToggleImportant();
        note.ToggleImportant();

        // Assert
        Assert.That(note.IsImportant, Is.False);
    }

    [Test]
    public void Meeting_CanBeAssociated_WithNote()
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
            Content = "Important point",
            Meeting = meeting
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(note.MeetingId, Is.EqualTo(meeting.MeetingId));
            Assert.That(note.Meeting, Is.Not.Null);
            Assert.That(note.Meeting.Title, Is.EqualTo("Team Meeting"));
        });
    }

    [Test]
    public void Category_CanBeSetToDifferentValues()
    {
        // Arrange
        var note = new Note
        {
            NoteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MeetingId = Guid.NewGuid(),
            Content = "Test note"
        };

        // Act & Assert
        note.Category = "Decisions";
        Assert.That(note.Category, Is.EqualTo("Decisions"));

        note.Category = "Action Items";
        Assert.That(note.Category, Is.EqualTo("Action Items"));

        note.Category = "Discussion Points";
        Assert.That(note.Category, Is.EqualTo("Discussion Points"));
    }

    [Test]
    public void Content_CanStoreLongText()
    {
        // Arrange
        var note = new Note
        {
            NoteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MeetingId = Guid.NewGuid()
        };

        var longContent = "This is a very long note content that might contain multiple paragraphs, detailed discussion points, and comprehensive information about the meeting topic.";

        // Act
        note.Content = longContent;

        // Assert
        Assert.That(note.Content, Is.EqualTo(longContent));
    }

    [Test]
    public void UpdatedAt_IsSetWhenToggleImportant()
    {
        // Arrange
        var note = new Note
        {
            NoteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MeetingId = Guid.NewGuid(),
            Content = "Test note",
            IsImportant = false
        };

        var beforeCall = DateTime.UtcNow;

        // Act
        note.ToggleImportant();

        var afterCall = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(note.UpdatedAt, Is.Not.Null);
            Assert.That(note.UpdatedAt!.Value, Is.GreaterThanOrEqualTo(beforeCall));
            Assert.That(note.UpdatedAt!.Value, Is.LessThanOrEqualTo(afterCall));
        });
    }
}
