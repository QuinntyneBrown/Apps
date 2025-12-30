// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MeetingNotesActionItemTracker.Api.Tests;

[TestFixture]
public class NoteCommandTests
{
    [Test]
    public async Task CreateNoteCommand_CreatesNewNote()
    {
        // Arrange
        var notes = new List<Note>();
        var mockSet = TestHelpers.CreateMockDbSet(notes);
        var mockContext = TestHelpers.CreateMockContext();
        mockContext.Setup(c => c.Notes).Returns(mockSet.Object);

        var handler = new CreateNoteCommandHandler(mockContext.Object);
        var command = new CreateNoteCommand(
            UserId: Guid.NewGuid(),
            MeetingId: Guid.NewGuid(),
            Content: "Test note content",
            Category: "Important",
            IsImportant: true
        );

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Content, Is.EqualTo(command.Content));
        Assert.That(result.IsImportant, Is.True);
        Assert.That(notes.Count, Is.EqualTo(1));
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task UpdateNoteCommand_UpdatesExistingNote()
    {
        // Arrange
        var noteId = Guid.NewGuid();
        var notes = new List<Note>
        {
            new Note
            {
                NoteId = noteId,
                UserId = Guid.NewGuid(),
                MeetingId = Guid.NewGuid(),
                Content = "Old content",
                IsImportant = false,
                CreatedAt = DateTime.UtcNow
            }
        };

        var mockSet = TestHelpers.CreateMockDbSet(notes);
        var mockContext = TestHelpers.CreateMockContext();
        mockContext.Setup(c => c.Notes).Returns(mockSet.Object);

        var handler = new UpdateNoteCommandHandler(mockContext.Object);
        var command = new UpdateNoteCommand(
            NoteId: noteId,
            Content: "Updated content",
            Category: "Critical",
            IsImportant: true
        );

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Content, Is.EqualTo("Updated content"));
        Assert.That(result.IsImportant, Is.True);
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task DeleteNoteCommand_DeletesExistingNote()
    {
        // Arrange
        var noteId = Guid.NewGuid();
        var notes = new List<Note>
        {
            new Note
            {
                NoteId = noteId,
                UserId = Guid.NewGuid(),
                MeetingId = Guid.NewGuid(),
                Content = "Test note",
                CreatedAt = DateTime.UtcNow
            }
        };

        var mockSet = TestHelpers.CreateMockDbSet(notes);
        var mockContext = TestHelpers.CreateMockContext();
        mockContext.Setup(c => c.Notes).Returns(mockSet.Object);

        var handler = new DeleteNoteCommandHandler(mockContext.Object);
        var command = new DeleteNoteCommand(noteId);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(notes.Count, Is.EqualTo(0));
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
