// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace KnowledgeBaseSecondBrain.Core.Tests;

public class NotesLinkedEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesEvent()
    {
        // Arrange
        var noteLinkId = Guid.NewGuid();
        var sourceNoteId = Guid.NewGuid();
        var targetNoteId = Guid.NewGuid();
        var linkType = "references";

        // Act
        var evt = new NotesLinkedEvent
        {
            NoteLinkId = noteLinkId,
            SourceNoteId = sourceNoteId,
            TargetNoteId = targetNoteId,
            LinkType = linkType
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.NoteLinkId, Is.EqualTo(noteLinkId));
            Assert.That(evt.SourceNoteId, Is.EqualTo(sourceNoteId));
            Assert.That(evt.TargetNoteId, Is.EqualTo(targetNoteId));
            Assert.That(evt.LinkType, Is.EqualTo(linkType));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Timestamp_IsSetAutomatically()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var evt = new NotesLinkedEvent
        {
            NoteLinkId = Guid.NewGuid(),
            SourceNoteId = Guid.NewGuid(),
            TargetNoteId = Guid.NewGuid()
        };

        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(evt.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation));
        Assert.That(evt.Timestamp, Is.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void NoteLinkId_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedId = Guid.NewGuid();

        // Act
        var evt = new NotesLinkedEvent { NoteLinkId = expectedId };

        // Assert
        Assert.That(evt.NoteLinkId, Is.EqualTo(expectedId));
    }

    [Test]
    public void SourceNoteId_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedSourceId = Guid.NewGuid();

        // Act
        var evt = new NotesLinkedEvent { SourceNoteId = expectedSourceId };

        // Assert
        Assert.That(evt.SourceNoteId, Is.EqualTo(expectedSourceId));
    }

    [Test]
    public void TargetNoteId_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedTargetId = Guid.NewGuid();

        // Act
        var evt = new NotesLinkedEvent { TargetNoteId = expectedTargetId };

        // Assert
        Assert.That(evt.TargetNoteId, Is.EqualTo(expectedTargetId));
    }

    [Test]
    public void LinkType_CanBeNull()
    {
        // Arrange & Act
        var evt = new NotesLinkedEvent { LinkType = null };

        // Assert
        Assert.That(evt.LinkType, Is.Null);
    }

    [Test]
    public void LinkType_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedLinkType = "related-to";

        // Act
        var evt = new NotesLinkedEvent { LinkType = expectedLinkType };

        // Assert
        Assert.That(evt.LinkType, Is.EqualTo(expectedLinkType));
    }
}
