// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MeetingNotesActionItemTracker.Infrastructure.Tests;

/// <summary>
/// Unit tests for the MeetingNotesActionItemTrackerContext.
/// </summary>
[TestFixture]
public class MeetingNotesActionItemTrackerContextTests
{
    private MeetingNotesActionItemTrackerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<MeetingNotesActionItemTrackerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new MeetingNotesActionItemTrackerContext(options);
    }

    /// <summary>
    /// Tears down the test context.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    /// <summary>
    /// Tests that Meetings can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Meetings_CanAddAndRetrieve()
    {
        // Arrange
        var meeting = new Meeting
        {
            MeetingId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Meeting",
            MeetingDateTime = DateTime.UtcNow,
            DurationMinutes = 60,
            Location = "Conference Room",
            Attendees = new List<string> { "John", "Jane" },
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Meetings.Add(meeting);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Meetings.FindAsync(meeting.MeetingId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Test Meeting"));
        Assert.That(retrieved.DurationMinutes, Is.EqualTo(60));
    }

    /// <summary>
    /// Tests that Notes can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Notes_CanAddAndRetrieve()
    {
        // Arrange
        var meeting = new Meeting
        {
            MeetingId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Meeting",
            MeetingDateTime = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        var note = new Note
        {
            NoteId = Guid.NewGuid(),
            UserId = meeting.UserId,
            MeetingId = meeting.MeetingId,
            Content = "Important discussion point",
            Category = "Discussion",
            IsImportant = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Meetings.Add(meeting);
        _context.Notes.Add(note);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Notes.FindAsync(note.NoteId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Content, Is.EqualTo("Important discussion point"));
        Assert.That(retrieved.IsImportant, Is.True);
    }

    /// <summary>
    /// Tests that ActionItems can be added and retrieved.
    /// </summary>
    [Test]
    public async Task ActionItems_CanAddAndRetrieve()
    {
        // Arrange
        var meeting = new Meeting
        {
            MeetingId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Meeting",
            MeetingDateTime = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        var actionItem = new ActionItem
        {
            ActionItemId = Guid.NewGuid(),
            UserId = meeting.UserId,
            MeetingId = meeting.MeetingId,
            Description = "Follow up on proposal",
            ResponsiblePerson = "John Doe",
            DueDate = DateTime.UtcNow.AddDays(7),
            Priority = Priority.High,
            Status = ActionItemStatus.NotStarted,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Meetings.Add(meeting);
        _context.ActionItems.Add(actionItem);
        await _context.SaveChangesAsync();

        var retrieved = await _context.ActionItems.FindAsync(actionItem.ActionItemId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Description, Is.EqualTo("Follow up on proposal"));
        Assert.That(retrieved.Priority, Is.EqualTo(Priority.High));
        Assert.That(retrieved.Status, Is.EqualTo(ActionItemStatus.NotStarted));
    }

    /// <summary>
    /// Tests cascade delete behavior.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedEntities()
    {
        // Arrange
        var meeting = new Meeting
        {
            MeetingId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Meeting",
            MeetingDateTime = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        var note = new Note
        {
            NoteId = Guid.NewGuid(),
            UserId = meeting.UserId,
            MeetingId = meeting.MeetingId,
            Content = "Test note",
            CreatedAt = DateTime.UtcNow,
        };

        _context.Meetings.Add(meeting);
        _context.Notes.Add(note);
        await _context.SaveChangesAsync();

        // Act
        _context.Meetings.Remove(meeting);
        await _context.SaveChangesAsync();

        var retrievedNote = await _context.Notes.FindAsync(note.NoteId);

        // Assert
        Assert.That(retrievedNote, Is.Null);
    }
}
