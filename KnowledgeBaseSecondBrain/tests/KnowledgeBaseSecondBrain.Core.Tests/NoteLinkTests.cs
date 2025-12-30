// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace KnowledgeBaseSecondBrain.Core.Tests;

public class NoteLinkTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesNoteLink()
    {
        // Arrange
        var noteLinkId = Guid.NewGuid();
        var sourceNoteId = Guid.NewGuid();
        var targetNoteId = Guid.NewGuid();
        var linkType = "references";

        // Act
        var noteLink = new NoteLink
        {
            NoteLinkId = noteLinkId,
            SourceNoteId = sourceNoteId,
            TargetNoteId = targetNoteId,
            LinkType = linkType
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(noteLink.NoteLinkId, Is.EqualTo(noteLinkId));
            Assert.That(noteLink.SourceNoteId, Is.EqualTo(sourceNoteId));
            Assert.That(noteLink.TargetNoteId, Is.EqualTo(targetNoteId));
            Assert.That(noteLink.LinkType, Is.EqualTo(linkType));
            Assert.That(noteLink.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void NoteLinkId_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var noteLink = new NoteLink();
        var expectedId = Guid.NewGuid();

        // Act
        noteLink.NoteLinkId = expectedId;

        // Assert
        Assert.That(noteLink.NoteLinkId, Is.EqualTo(expectedId));
    }

    [Test]
    public void SourceNoteId_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var noteLink = new NoteLink();
        var expectedSourceId = Guid.NewGuid();

        // Act
        noteLink.SourceNoteId = expectedSourceId;

        // Assert
        Assert.That(noteLink.SourceNoteId, Is.EqualTo(expectedSourceId));
    }

    [Test]
    public void TargetNoteId_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var noteLink = new NoteLink();
        var expectedTargetId = Guid.NewGuid();

        // Act
        noteLink.TargetNoteId = expectedTargetId;

        // Assert
        Assert.That(noteLink.TargetNoteId, Is.EqualTo(expectedTargetId));
    }

    [Test]
    public void Description_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var noteLink = new NoteLink();
        var expectedDescription = "This note references that concept";

        // Act
        noteLink.Description = expectedDescription;

        // Assert
        Assert.That(noteLink.Description, Is.EqualTo(expectedDescription));
    }

    [Test]
    public void LinkType_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var noteLink = new NoteLink();
        var expectedLinkType = "related-to";

        // Act
        noteLink.LinkType = expectedLinkType;

        // Assert
        Assert.That(noteLink.LinkType, Is.EqualTo(expectedLinkType));
    }

    [Test]
    public void Description_CanBeNull()
    {
        // Arrange
        var noteLink = new NoteLink();

        // Act
        noteLink.Description = null;

        // Assert
        Assert.That(noteLink.Description, Is.Null);
    }

    [Test]
    public void LinkType_CanBeNull()
    {
        // Arrange
        var noteLink = new NoteLink();

        // Act
        noteLink.LinkType = null;

        // Assert
        Assert.That(noteLink.LinkType, Is.Null);
    }

    [Test]
    public void SourceNote_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var noteLink = new NoteLink();
        var sourceNote = new Note { NoteId = Guid.NewGuid() };

        // Act
        noteLink.SourceNote = sourceNote;

        // Assert
        Assert.That(noteLink.SourceNote, Is.EqualTo(sourceNote));
    }

    [Test]
    public void TargetNote_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var noteLink = new NoteLink();
        var targetNote = new Note { NoteId = Guid.NewGuid() };

        // Act
        noteLink.TargetNote = targetNote;

        // Assert
        Assert.That(noteLink.TargetNote, Is.EqualTo(targetNote));
    }
}
