// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalReadingList.Core.Tests;

public class NoteAddedEventTests
{
    [Test]
    public void Constructor_WithValidParameters_CreatesEvent()
    {
        // Arrange
        var noteId = Guid.NewGuid();
        var resourceId = Guid.NewGuid();

        // Act
        var eventData = new NoteAddedEvent
        {
            NoteId = noteId,
            ResourceId = resourceId
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.NoteId, Is.EqualTo(noteId));
            Assert.That(eventData.ResourceId, Is.EqualTo(resourceId));
            Assert.That(eventData.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Timestamp_DefaultValue_IsSetToUtcNow()
    {
        // Arrange
        var beforeCreate = DateTime.UtcNow;

        // Act
        var eventData = new NoteAddedEvent
        {
            NoteId = Guid.NewGuid(),
            ResourceId = Guid.NewGuid()
        };

        var afterCreate = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.Timestamp, Is.GreaterThanOrEqualTo(beforeCreate));
            Assert.That(eventData.Timestamp, Is.LessThanOrEqualTo(afterCreate));
        });
    }

    [Test]
    public void Record_Equality_WorksCorrectly()
    {
        // Arrange
        var noteId = Guid.NewGuid();
        var resourceId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new NoteAddedEvent
        {
            NoteId = noteId,
            ResourceId = resourceId,
            Timestamp = timestamp
        };

        var event2 = new NoteAddedEvent
        {
            NoteId = noteId,
            ResourceId = resourceId,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Record_Inequality_DifferentNoteIds_WorksCorrectly()
    {
        // Arrange
        var resourceId = Guid.NewGuid();

        var event1 = new NoteAddedEvent
        {
            NoteId = Guid.NewGuid(),
            ResourceId = resourceId
        };

        var event2 = new NoteAddedEvent
        {
            NoteId = Guid.NewGuid(),
            ResourceId = resourceId
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }

    [Test]
    public void Record_Inequality_DifferentResourceIds_WorksCorrectly()
    {
        // Arrange
        var noteId = Guid.NewGuid();

        var event1 = new NoteAddedEvent
        {
            NoteId = noteId,
            ResourceId = Guid.NewGuid()
        };

        var event2 = new NoteAddedEvent
        {
            NoteId = noteId,
            ResourceId = Guid.NewGuid()
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
