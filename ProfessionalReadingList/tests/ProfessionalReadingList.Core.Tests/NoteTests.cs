// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalReadingList.Core.Tests;

public class NoteTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesNote()
    {
        // Arrange & Act
        var note = new Note();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(note.NoteId, Is.EqualTo(Guid.Empty));
            Assert.That(note.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(note.ResourceId, Is.EqualTo(Guid.Empty));
            Assert.That(note.Content, Is.EqualTo(string.Empty));
            Assert.That(note.PageReference, Is.Null);
            Assert.That(note.Quote, Is.Null);
            Assert.That(note.Tags, Is.Not.Null);
            Assert.That(note.Tags, Is.Empty);
            Assert.That(note.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
            Assert.That(note.UpdatedAt, Is.Null);
            Assert.That(note.Resource, Is.Null);
        });
    }

    [Test]
    public void Constructor_WithValidParameters_CreatesNote()
    {
        // Arrange
        var noteId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var resourceId = Guid.NewGuid();
        var content = "Important concept about dependency injection";
        var pageReference = "Page 42";

        // Act
        var note = new Note
        {
            NoteId = noteId,
            UserId = userId,
            ResourceId = resourceId,
            Content = content,
            PageReference = pageReference
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(note.NoteId, Is.EqualTo(noteId));
            Assert.That(note.UserId, Is.EqualTo(userId));
            Assert.That(note.ResourceId, Is.EqualTo(resourceId));
            Assert.That(note.Content, Is.EqualTo(content));
            Assert.That(note.PageReference, Is.EqualTo(pageReference));
        });
    }

    [Test]
    public void Note_WithQuote_SetsCorrectly()
    {
        // Arrange
        var quote = "Clean code is simple and direct.";

        // Act
        var note = new Note
        {
            Content = "Key principle from the book",
            Quote = quote,
            PageReference = "Chapter 1"
        };

        // Assert
        Assert.That(note.Quote, Is.EqualTo(quote));
    }

    [Test]
    public void Note_WithPageReference_SetsCorrectly()
    {
        // Arrange & Act
        var note = new Note
        {
            Content = "Important note",
            PageReference = "Chapter 5, Section 3"
        };

        // Assert
        Assert.That(note.PageReference, Is.EqualTo("Chapter 5, Section 3"));
    }

    [Test]
    public void AddTag_NewTag_AddsTagToList()
    {
        // Arrange
        var note = new Note { Content = "Test note" };
        var tag = "Important";

        // Act
        note.AddTag(tag);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(note.Tags, Contains.Item(tag));
            Assert.That(note.Tags.Count, Is.EqualTo(1));
            Assert.That(note.UpdatedAt, Is.Not.Null);
        });
    }

    [Test]
    public void AddTag_DuplicateTag_DoesNotAddDuplicate()
    {
        // Arrange
        var note = new Note { Content = "Test note" };
        var tag = "KeyConcept";

        // Act
        note.AddTag(tag);
        note.AddTag(tag);

        // Assert
        Assert.That(note.Tags.Count, Is.EqualTo(1));
    }

    [Test]
    public void AddTag_DuplicateTagDifferentCase_DoesNotAddDuplicate()
    {
        // Arrange
        var note = new Note { Content = "Test note" };

        // Act
        note.AddTag("Important");
        note.AddTag("important");

        // Assert
        Assert.That(note.Tags.Count, Is.EqualTo(1));
    }

    [Test]
    public void AddTag_MultipleTags_AddsAllUniqueTags()
    {
        // Arrange
        var note = new Note { Content = "Test note" };

        // Act
        note.AddTag("Important");
        note.AddTag("Review");
        note.AddTag("ActionItem");

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(note.Tags.Count, Is.EqualTo(3));
            Assert.That(note.Tags, Contains.Item("Important"));
            Assert.That(note.Tags, Contains.Item("Review"));
            Assert.That(note.Tags, Contains.Item("ActionItem"));
        });
    }

    [Test]
    public void Note_WithAllOptionalProperties_SetsAllProperties()
    {
        // Arrange & Act
        var note = new Note
        {
            NoteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ResourceId = Guid.NewGuid(),
            Content = "Comprehensive note about design patterns",
            PageReference = "Chapter 3, Page 89",
            Quote = "Design patterns are reusable solutions to common problems."
        };
        note.AddTag("DesignPatterns");
        note.AddTag("BestPractices");

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(note.Content, Is.EqualTo("Comprehensive note about design patterns"));
            Assert.That(note.PageReference, Is.EqualTo("Chapter 3, Page 89"));
            Assert.That(note.Quote, Is.EqualTo("Design patterns are reusable solutions to common problems."));
            Assert.That(note.Tags.Count, Is.EqualTo(2));
        });
    }

    [Test]
    public void Note_LongContent_StoresCorrectly()
    {
        // Arrange
        var longContent = string.Join(" ", Enumerable.Repeat("This is a detailed note.", 50));

        // Act
        var note = new Note
        {
            Content = longContent
        };

        // Assert
        Assert.That(note.Content, Is.EqualTo(longContent));
        Assert.That(note.Content.Length, Is.GreaterThan(100));
    }

    [Test]
    public void Note_EmptyQuote_SetsCorrectly()
    {
        // Arrange & Act
        var note = new Note
        {
            Content = "Note without quote",
            Quote = string.Empty
        };

        // Assert
        Assert.That(note.Quote, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Note_NumericPageReference_SetsCorrectly()
    {
        // Arrange & Act
        var note = new Note
        {
            Content = "Test note",
            PageReference = "123"
        };

        // Assert
        Assert.That(note.PageReference, Is.EqualTo("123"));
    }

    [Test]
    public void Note_MultilineContent_StoresCorrectly()
    {
        // Arrange
        var content = @"Key takeaways:
1. Use dependency injection
2. Follow SOLID principles
3. Write clean code";

        // Act
        var note = new Note
        {
            Content = content
        };

        // Assert
        Assert.That(note.Content, Is.EqualTo(content));
        Assert.That(note.Content, Does.Contain("SOLID"));
    }
}
