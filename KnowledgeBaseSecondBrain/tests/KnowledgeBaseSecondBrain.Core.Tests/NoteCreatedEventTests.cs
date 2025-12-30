// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace KnowledgeBaseSecondBrain.Core.Tests;

public class NoteCreatedEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesEvent()
    {
        // Arrange
        var noteId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var title = "My Note";
        var noteType = NoteType.Concept;

        // Act
        var evt = new NoteCreatedEvent
        {
            NoteId = noteId,
            UserId = userId,
            Title = title,
            NoteType = noteType
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.NoteId, Is.EqualTo(noteId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Title, Is.EqualTo(title));
            Assert.That(evt.NoteType, Is.EqualTo(noteType));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Timestamp_IsSetAutomatically()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var evt = new NoteCreatedEvent
        {
            NoteId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(evt.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation));
        Assert.That(evt.Timestamp, Is.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void NoteId_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedId = Guid.NewGuid();

        // Act
        var evt = new NoteCreatedEvent { NoteId = expectedId };

        // Assert
        Assert.That(evt.NoteId, Is.EqualTo(expectedId));
    }

    [Test]
    public void UserId_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedUserId = Guid.NewGuid();

        // Act
        var evt = new NoteCreatedEvent { UserId = expectedUserId };

        // Assert
        Assert.That(evt.UserId, Is.EqualTo(expectedUserId));
    }

    [Test]
    public void Title_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedTitle = "Important Concept";

        // Act
        var evt = new NoteCreatedEvent { Title = expectedTitle };

        // Assert
        Assert.That(evt.Title, Is.EqualTo(expectedTitle));
    }

    [Test]
    public void NoteType_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedNoteType = NoteType.Permanent;

        // Act
        var evt = new NoteCreatedEvent { NoteType = expectedNoteType };

        // Assert
        Assert.That(evt.NoteType, Is.EqualTo(expectedNoteType));
    }
}
