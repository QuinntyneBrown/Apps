// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace KnowledgeBaseSecondBrain.Core.Tests;

public class NoteTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesNote()
    {
        // Arrange
        var noteId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var title = "Test Note";
        var content = "Note content";
        var noteType = NoteType.Concept;

        // Act
        var note = new Note
        {
            NoteId = noteId,
            UserId = userId,
            Title = title,
            Content = content,
            NoteType = noteType
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(note.NoteId, Is.EqualTo(noteId));
            Assert.That(note.UserId, Is.EqualTo(userId));
            Assert.That(note.Title, Is.EqualTo(title));
            Assert.That(note.Content, Is.EqualTo(content));
            Assert.That(note.NoteType, Is.EqualTo(noteType));
            Assert.That(note.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(note.LastModifiedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void UpdateContent_ValidContent_UpdatesContentAndTimestamp()
    {
        // Arrange
        var note = new Note
        {
            Content = "Original content"
        };
        var originalTimestamp = note.LastModifiedAt;
        Thread.Sleep(10);
        var newContent = "Updated content";

        // Act
        note.UpdateContent(newContent);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(note.Content, Is.EqualTo(newContent));
            Assert.That(note.LastModifiedAt, Is.GreaterThan(originalTimestamp));
        });
    }

    [Test]
    public void Archive_WhenCalled_SetsIsArchivedTrueAndUpdatesTimestamp()
    {
        // Arrange
        var note = new Note { IsArchived = false };
        var originalTimestamp = note.LastModifiedAt;
        Thread.Sleep(10);

        // Act
        note.Archive();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(note.IsArchived, Is.True);
            Assert.That(note.LastModifiedAt, Is.GreaterThan(originalTimestamp));
        });
    }

    [Test]
    public void Unarchive_WhenCalled_SetsIsArchivedFalseAndUpdatesTimestamp()
    {
        // Arrange
        var note = new Note { IsArchived = true };
        var originalTimestamp = note.LastModifiedAt;
        Thread.Sleep(10);

        // Act
        note.Unarchive();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(note.IsArchived, Is.False);
            Assert.That(note.LastModifiedAt, Is.GreaterThan(originalTimestamp));
        });
    }

    [Test]
    public void IsFavorite_DefaultsToFalse()
    {
        // Arrange & Act
        var note = new Note();

        // Assert
        Assert.That(note.IsFavorite, Is.False);
    }

    [Test]
    public void IsFavorite_CanBeSetToTrue()
    {
        // Arrange
        var note = new Note();

        // Act
        note.IsFavorite = true;

        // Assert
        Assert.That(note.IsFavorite, Is.True);
    }

    [Test]
    public void IsArchived_DefaultsToFalse()
    {
        // Arrange & Act
        var note = new Note();

        // Assert
        Assert.That(note.IsArchived, Is.False);
    }

    [Test]
    public void ParentNoteId_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var note = new Note();
        var parentNoteId = Guid.NewGuid();

        // Act
        note.ParentNoteId = parentNoteId;

        // Assert
        Assert.That(note.ParentNoteId, Is.EqualTo(parentNoteId));
    }

    [Test]
    public void ParentNoteId_CanBeNull()
    {
        // Arrange
        var note = new Note();

        // Act
        note.ParentNoteId = null;

        // Assert
        Assert.That(note.ParentNoteId, Is.Null);
    }

    [Test]
    public void ChildNotes_DefaultsToEmptyCollection()
    {
        // Arrange & Act
        var note = new Note();

        // Assert
        Assert.That(note.ChildNotes, Is.Not.Null);
        Assert.That(note.ChildNotes, Is.Empty);
    }

    [Test]
    public void Tags_DefaultsToEmptyCollection()
    {
        // Arrange & Act
        var note = new Note();

        // Assert
        Assert.That(note.Tags, Is.Not.Null);
        Assert.That(note.Tags, Is.Empty);
    }

    [Test]
    public void OutgoingLinks_DefaultsToEmptyCollection()
    {
        // Arrange & Act
        var note = new Note();

        // Assert
        Assert.That(note.OutgoingLinks, Is.Not.Null);
        Assert.That(note.OutgoingLinks, Is.Empty);
    }

    [Test]
    public void IncomingLinks_DefaultsToEmptyCollection()
    {
        // Arrange & Act
        var note = new Note();

        // Assert
        Assert.That(note.IncomingLinks, Is.Not.Null);
        Assert.That(note.IncomingLinks, Is.Empty);
    }

    [Test]
    public void Tags_CanAddTags_ReturnsCorrectCount()
    {
        // Arrange
        var note = new Note();
        var tag = new Tag { TagId = Guid.NewGuid(), Name = "Important" };

        // Act
        note.Tags.Add(tag);

        // Assert
        Assert.That(note.Tags.Count, Is.EqualTo(1));
        Assert.That(note.Tags.First(), Is.EqualTo(tag));
    }
}
