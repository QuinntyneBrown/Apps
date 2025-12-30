// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ConferenceEventManager.Core.Tests;

public class NoteTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesInstance()
    {
        // Arrange & Act
        var note = new Note();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(note.Content, Is.EqualTo(string.Empty));
            Assert.That(note.Category, Is.Null);
            Assert.That(note.Tags, Is.Not.Null);
            Assert.That(note.Tags, Is.Empty);
            Assert.That(note.UpdatedAt, Is.Null);
            Assert.That(note.Event, Is.Null);
        });
    }

    [Test]
    public void Properties_CanBeSet()
    {
        // Arrange
        var noteId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var eventId = Guid.NewGuid();

        // Act
        var note = new Note
        {
            NoteId = noteId,
            UserId = userId,
            EventId = eventId,
            Content = "Key takeaways from the keynote",
            Category = "Insights",
            Tags = new List<string> { "keynote", "AI", "future" }
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(note.NoteId, Is.EqualTo(noteId));
            Assert.That(note.UserId, Is.EqualTo(userId));
            Assert.That(note.EventId, Is.EqualTo(eventId));
            Assert.That(note.Content, Is.EqualTo("Key takeaways from the keynote"));
            Assert.That(note.Category, Is.EqualTo("Insights"));
            Assert.That(note.Tags, Has.Count.EqualTo(3));
            Assert.That(note.Tags, Contains.Item("keynote"));
            Assert.That(note.Tags, Contains.Item("AI"));
            Assert.That(note.Tags, Contains.Item("future"));
        });
    }

    [Test]
    public void AddTag_NewTag_AddsTagAndUpdatesTimestamp()
    {
        // Arrange
        var note = new Note
        {
            UpdatedAt = null
        };

        // Act
        note.AddTag("important");

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(note.Tags, Has.Count.EqualTo(1));
            Assert.That(note.Tags, Contains.Item("important"));
            Assert.That(note.UpdatedAt, Is.Not.Null);
            Assert.That(note.UpdatedAt.Value, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void AddTag_DuplicateTag_DoesNotAddDuplicate()
    {
        // Arrange
        var note = new Note();
        note.AddTag("important");
        var originalUpdatedAt = note.UpdatedAt;

        // Act
        note.AddTag("important");

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(note.Tags, Has.Count.EqualTo(1));
            Assert.That(note.Tags, Contains.Item("important"));
            Assert.That(note.UpdatedAt, Is.EqualTo(originalUpdatedAt));
        });
    }

    [Test]
    public void AddTag_DifferentCaseTag_DoesNotAddDuplicate()
    {
        // Arrange
        var note = new Note();
        note.AddTag("Important");

        // Act
        note.AddTag("important");

        // Assert
        Assert.That(note.Tags, Has.Count.EqualTo(1));
    }

    [Test]
    public void AddTag_MultipleTags_AddsAllTags()
    {
        // Arrange
        var note = new Note();

        // Act
        note.AddTag("tag1");
        note.AddTag("tag2");
        note.AddTag("tag3");

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(note.Tags, Has.Count.EqualTo(3));
            Assert.That(note.Tags, Contains.Item("tag1"));
            Assert.That(note.Tags, Contains.Item("tag2"));
            Assert.That(note.Tags, Contains.Item("tag3"));
        });
    }

    [Test]
    public void AddTag_EmptyString_AddsEmptyTag()
    {
        // Arrange
        var note = new Note();

        // Act
        note.AddTag("");

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(note.Tags, Has.Count.EqualTo(1));
            Assert.That(note.Tags, Contains.Item(""));
        });
    }

    [Test]
    public void Event_CanBeAssigned()
    {
        // Arrange
        var evt = new Event { EventId = Guid.NewGuid(), Name = "Tech Conference" };
        var note = new Note();

        // Act
        note.Event = evt;

        // Assert
        Assert.That(note.Event, Is.EqualTo(evt));
    }
}
