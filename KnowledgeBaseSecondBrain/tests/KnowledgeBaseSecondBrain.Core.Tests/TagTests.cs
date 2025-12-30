// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace KnowledgeBaseSecondBrain.Core.Tests;

public class TagTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesTag()
    {
        // Arrange
        var tagId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Important";
        var color = "#FF5733";

        // Act
        var tag = new Tag
        {
            TagId = tagId,
            UserId = userId,
            Name = name,
            Color = color
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(tag.TagId, Is.EqualTo(tagId));
            Assert.That(tag.UserId, Is.EqualTo(userId));
            Assert.That(tag.Name, Is.EqualTo(name));
            Assert.That(tag.Color, Is.EqualTo(color));
            Assert.That(tag.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void GetNoteCount_WithNoNotes_ReturnsZero()
    {
        // Arrange
        var tag = new Tag();

        // Act
        var count = tag.GetNoteCount();

        // Assert
        Assert.That(count, Is.EqualTo(0));
    }

    [Test]
    public void GetNoteCount_WithMultipleNotes_ReturnsCorrectCount()
    {
        // Arrange
        var tag = new Tag();
        tag.Notes.Add(new Note { NoteId = Guid.NewGuid() });
        tag.Notes.Add(new Note { NoteId = Guid.NewGuid() });
        tag.Notes.Add(new Note { NoteId = Guid.NewGuid() });

        // Act
        var count = tag.GetNoteCount();

        // Assert
        Assert.That(count, Is.EqualTo(3));
    }

    [Test]
    public void TagId_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var tag = new Tag();
        var expectedId = Guid.NewGuid();

        // Act
        tag.TagId = expectedId;

        // Assert
        Assert.That(tag.TagId, Is.EqualTo(expectedId));
    }

    [Test]
    public void UserId_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var tag = new Tag();
        var expectedUserId = Guid.NewGuid();

        // Act
        tag.UserId = expectedUserId;

        // Assert
        Assert.That(tag.UserId, Is.EqualTo(expectedUserId));
    }

    [Test]
    public void Name_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var tag = new Tag();
        var expectedName = "Work";

        // Act
        tag.Name = expectedName;

        // Assert
        Assert.That(tag.Name, Is.EqualTo(expectedName));
    }

    [Test]
    public void Color_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var tag = new Tag();
        var expectedColor = "#00FF00";

        // Act
        tag.Color = expectedColor;

        // Assert
        Assert.That(tag.Color, Is.EqualTo(expectedColor));
    }

    [Test]
    public void Color_CanBeNull()
    {
        // Arrange
        var tag = new Tag();

        // Act
        tag.Color = null;

        // Assert
        Assert.That(tag.Color, Is.Null);
    }

    [Test]
    public void Notes_DefaultsToEmptyCollection()
    {
        // Arrange & Act
        var tag = new Tag();

        // Assert
        Assert.That(tag.Notes, Is.Not.Null);
        Assert.That(tag.Notes, Is.Empty);
    }

    [Test]
    public void Notes_CanAddNotes_ReturnsCorrectCount()
    {
        // Arrange
        var tag = new Tag();
        var note = new Note { NoteId = Guid.NewGuid() };

        // Act
        tag.Notes.Add(note);

        // Assert
        Assert.That(tag.Notes.Count, Is.EqualTo(1));
        Assert.That(tag.Notes.First(), Is.EqualTo(note));
    }
}
