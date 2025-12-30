// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ConferenceEventManager.Infrastructure.Tests.Data;

/// <summary>
/// Contains unit tests for the <see cref="ConferenceEventManagerContext"/> class.
/// </summary>
[TestFixture]
public class ConferenceEventManagerContextTests
{
    private DbContextOptions<ConferenceEventManagerContext> _options = null!;
    private ConferenceEventManagerContext _context = null!;
    private Guid _testUserId;

    /// <summary>
    /// Sets up the test context before each test.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        _testUserId = Guid.NewGuid();
        _options = new DbContextOptionsBuilder<ConferenceEventManagerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ConferenceEventManagerContext(_options);
    }

    /// <summary>
    /// Cleans up resources after each test.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    #region Event Tests

    /// <summary>
    /// Tests that an event can be created successfully.
    /// </summary>
    [Test]
    public async Task CreateEvent_ShouldAddEventToDatabase()
    {
        // Arrange
        var conferenceEvent = new Event
        {
            EventId = Guid.NewGuid(),
            UserId = _testUserId,
            Name = "Tech Conference 2024",
            EventType = EventType.Conference,
            StartDate = DateTime.UtcNow.AddDays(30),
            EndDate = DateTime.UtcNow.AddDays(33),
            Location = "San Francisco, CA",
            IsVirtual = false,
            IsRegistered = true,
            DidAttend = false,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        _context.Events.Add(conferenceEvent);
        await _context.SaveChangesAsync();

        // Assert
        var retrieved = await _context.Events.FindAsync(conferenceEvent.EventId);
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved.Name, Is.EqualTo("Tech Conference 2024"));
        Assert.That(retrieved.EventType, Is.EqualTo(EventType.Conference));
    }

    /// <summary>
    /// Tests that an event can be updated successfully.
    /// </summary>
    [Test]
    public async Task UpdateEvent_ShouldModifyExistingEvent()
    {
        // Arrange
        var conferenceEvent = new Event
        {
            EventId = Guid.NewGuid(),
            UserId = _testUserId,
            Name = "Original Event",
            EventType = EventType.Workshop,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
            IsRegistered = false,
            CreatedAt = DateTime.UtcNow
        };
        _context.Events.Add(conferenceEvent);
        await _context.SaveChangesAsync();

        // Act
        conferenceEvent.IsRegistered = true;
        conferenceEvent.DidAttend = true;
        await _context.SaveChangesAsync();

        // Assert
        var updated = await _context.Events.FindAsync(conferenceEvent.EventId);
        Assert.That(updated, Is.Not.Null);
        Assert.That(updated.IsRegistered, Is.True);
        Assert.That(updated.DidAttend, Is.True);
    }

    /// <summary>
    /// Tests that an event can be deleted successfully.
    /// </summary>
    [Test]
    public async Task DeleteEvent_ShouldRemoveEventFromDatabase()
    {
        // Arrange
        var conferenceEvent = new Event
        {
            EventId = Guid.NewGuid(),
            UserId = _testUserId,
            Name = "To Delete",
            EventType = EventType.Seminar,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
            CreatedAt = DateTime.UtcNow
        };
        _context.Events.Add(conferenceEvent);
        await _context.SaveChangesAsync();

        // Act
        _context.Events.Remove(conferenceEvent);
        await _context.SaveChangesAsync();

        // Assert
        var deleted = await _context.Events.FindAsync(conferenceEvent.EventId);
        Assert.That(deleted, Is.Null);
    }

    #endregion

    #region Session Tests

    /// <summary>
    /// Tests that a session can be created successfully.
    /// </summary>
    [Test]
    public async Task CreateSession_ShouldAddSessionToDatabase()
    {
        // Arrange
        var conferenceEvent = new Event
        {
            EventId = Guid.NewGuid(),
            UserId = _testUserId,
            Name = "Test Event",
            EventType = EventType.Conference,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
            CreatedAt = DateTime.UtcNow
        };
        _context.Events.Add(conferenceEvent);
        await _context.SaveChangesAsync();

        var session = new Session
        {
            SessionId = Guid.NewGuid(),
            UserId = _testUserId,
            EventId = conferenceEvent.EventId,
            Title = "Introduction to .NET",
            Speaker = "John Doe",
            StartTime = DateTime.UtcNow.AddDays(1),
            PlansToAttend = true,
            DidAttend = false,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        _context.Sessions.Add(session);
        await _context.SaveChangesAsync();

        // Assert
        var retrieved = await _context.Sessions.FindAsync(session.SessionId);
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved.Title, Is.EqualTo("Introduction to .NET"));
        Assert.That(retrieved.Speaker, Is.EqualTo("John Doe"));
    }

    /// <summary>
    /// Tests that a session can be updated successfully.
    /// </summary>
    [Test]
    public async Task UpdateSession_ShouldModifyExistingSession()
    {
        // Arrange
        var conferenceEvent = new Event
        {
            EventId = Guid.NewGuid(),
            UserId = _testUserId,
            Name = "Test Event",
            EventType = EventType.Conference,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
            CreatedAt = DateTime.UtcNow
        };
        _context.Events.Add(conferenceEvent);
        await _context.SaveChangesAsync();

        var session = new Session
        {
            SessionId = Guid.NewGuid(),
            UserId = _testUserId,
            EventId = conferenceEvent.EventId,
            Title = "Test Session",
            StartTime = DateTime.UtcNow,
            PlansToAttend = false,
            DidAttend = false,
            CreatedAt = DateTime.UtcNow
        };
        _context.Sessions.Add(session);
        await _context.SaveChangesAsync();

        // Act
        session.PlansToAttend = true;
        session.DidAttend = true;
        await _context.SaveChangesAsync();

        // Assert
        var updated = await _context.Sessions.FindAsync(session.SessionId);
        Assert.That(updated, Is.Not.Null);
        Assert.That(updated.PlansToAttend, Is.True);
        Assert.That(updated.DidAttend, Is.True);
    }

    /// <summary>
    /// Tests that a session can be deleted successfully.
    /// </summary>
    [Test]
    public async Task DeleteSession_ShouldRemoveSessionFromDatabase()
    {
        // Arrange
        var conferenceEvent = new Event
        {
            EventId = Guid.NewGuid(),
            UserId = _testUserId,
            Name = "Test Event",
            EventType = EventType.Conference,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
            CreatedAt = DateTime.UtcNow
        };
        _context.Events.Add(conferenceEvent);
        await _context.SaveChangesAsync();

        var session = new Session
        {
            SessionId = Guid.NewGuid(),
            UserId = _testUserId,
            EventId = conferenceEvent.EventId,
            Title = "To Delete",
            StartTime = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };
        _context.Sessions.Add(session);
        await _context.SaveChangesAsync();

        // Act
        _context.Sessions.Remove(session);
        await _context.SaveChangesAsync();

        // Assert
        var deleted = await _context.Sessions.FindAsync(session.SessionId);
        Assert.That(deleted, Is.Null);
    }

    #endregion

    #region Contact Tests

    /// <summary>
    /// Tests that a contact can be created successfully.
    /// </summary>
    [Test]
    public async Task CreateContact_ShouldAddContactToDatabase()
    {
        // Arrange
        var conferenceEvent = new Event
        {
            EventId = Guid.NewGuid(),
            UserId = _testUserId,
            Name = "Test Event",
            EventType = EventType.Conference,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
            CreatedAt = DateTime.UtcNow
        };
        _context.Events.Add(conferenceEvent);
        await _context.SaveChangesAsync();

        var contact = new Contact
        {
            ContactId = Guid.NewGuid(),
            UserId = _testUserId,
            EventId = conferenceEvent.EventId,
            Name = "Jane Smith",
            Company = "Tech Corp",
            JobTitle = "Software Engineer",
            Email = "jane@techcorp.com",
            CreatedAt = DateTime.UtcNow
        };

        // Act
        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync();

        // Assert
        var retrieved = await _context.Contacts.FindAsync(contact.ContactId);
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved.Name, Is.EqualTo("Jane Smith"));
        Assert.That(retrieved.Company, Is.EqualTo("Tech Corp"));
    }

    /// <summary>
    /// Tests that a contact can be updated successfully.
    /// </summary>
    [Test]
    public async Task UpdateContact_ShouldModifyExistingContact()
    {
        // Arrange
        var conferenceEvent = new Event
        {
            EventId = Guid.NewGuid(),
            UserId = _testUserId,
            Name = "Test Event",
            EventType = EventType.Conference,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
            CreatedAt = DateTime.UtcNow
        };
        _context.Events.Add(conferenceEvent);
        await _context.SaveChangesAsync();

        var contact = new Contact
        {
            ContactId = Guid.NewGuid(),
            UserId = _testUserId,
            EventId = conferenceEvent.EventId,
            Name = "Original Name",
            CreatedAt = DateTime.UtcNow
        };
        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync();

        // Act
        contact.Name = "Updated Name";
        contact.Company = "New Company";
        await _context.SaveChangesAsync();

        // Assert
        var updated = await _context.Contacts.FindAsync(contact.ContactId);
        Assert.That(updated, Is.Not.Null);
        Assert.That(updated.Name, Is.EqualTo("Updated Name"));
        Assert.That(updated.Company, Is.EqualTo("New Company"));
    }

    /// <summary>
    /// Tests that a contact can be deleted successfully.
    /// </summary>
    [Test]
    public async Task DeleteContact_ShouldRemoveContactFromDatabase()
    {
        // Arrange
        var conferenceEvent = new Event
        {
            EventId = Guid.NewGuid(),
            UserId = _testUserId,
            Name = "Test Event",
            EventType = EventType.Conference,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
            CreatedAt = DateTime.UtcNow
        };
        _context.Events.Add(conferenceEvent);
        await _context.SaveChangesAsync();

        var contact = new Contact
        {
            ContactId = Guid.NewGuid(),
            UserId = _testUserId,
            EventId = conferenceEvent.EventId,
            Name = "To Delete",
            CreatedAt = DateTime.UtcNow
        };
        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync();

        // Act
        _context.Contacts.Remove(contact);
        await _context.SaveChangesAsync();

        // Assert
        var deleted = await _context.Contacts.FindAsync(contact.ContactId);
        Assert.That(deleted, Is.Null);
    }

    #endregion

    #region Note Tests

    /// <summary>
    /// Tests that a note can be created successfully.
    /// </summary>
    [Test]
    public async Task CreateNote_ShouldAddNoteToDatabase()
    {
        // Arrange
        var conferenceEvent = new Event
        {
            EventId = Guid.NewGuid(),
            UserId = _testUserId,
            Name = "Test Event",
            EventType = EventType.Conference,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
            CreatedAt = DateTime.UtcNow
        };
        _context.Events.Add(conferenceEvent);
        await _context.SaveChangesAsync();

        var note = new Note
        {
            NoteId = Guid.NewGuid(),
            UserId = _testUserId,
            EventId = conferenceEvent.EventId,
            Content = "Important insight from keynote",
            Category = "Keynote",
            Tags = new List<string> { "AI", "Cloud", "Innovation" },
            CreatedAt = DateTime.UtcNow
        };

        // Act
        _context.Notes.Add(note);
        await _context.SaveChangesAsync();

        // Assert
        var retrieved = await _context.Notes.FindAsync(note.NoteId);
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved.Content, Is.EqualTo("Important insight from keynote"));
        Assert.That(retrieved.Tags, Has.Count.EqualTo(3));
    }

    /// <summary>
    /// Tests that a note can be updated successfully.
    /// </summary>
    [Test]
    public async Task UpdateNote_ShouldModifyExistingNote()
    {
        // Arrange
        var conferenceEvent = new Event
        {
            EventId = Guid.NewGuid(),
            UserId = _testUserId,
            Name = "Test Event",
            EventType = EventType.Conference,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
            CreatedAt = DateTime.UtcNow
        };
        _context.Events.Add(conferenceEvent);
        await _context.SaveChangesAsync();

        var note = new Note
        {
            NoteId = Guid.NewGuid(),
            UserId = _testUserId,
            EventId = conferenceEvent.EventId,
            Content = "Original content",
            Tags = new List<string> { "Tag1" },
            CreatedAt = DateTime.UtcNow
        };
        _context.Notes.Add(note);
        await _context.SaveChangesAsync();

        // Act
        note.Content = "Updated content";
        note.Category = "Technical";
        await _context.SaveChangesAsync();

        // Assert
        var updated = await _context.Notes.FindAsync(note.NoteId);
        Assert.That(updated, Is.Not.Null);
        Assert.That(updated.Content, Is.EqualTo("Updated content"));
        Assert.That(updated.Category, Is.EqualTo("Technical"));
    }

    /// <summary>
    /// Tests that a note can be deleted successfully.
    /// </summary>
    [Test]
    public async Task DeleteNote_ShouldRemoveNoteFromDatabase()
    {
        // Arrange
        var conferenceEvent = new Event
        {
            EventId = Guid.NewGuid(),
            UserId = _testUserId,
            Name = "Test Event",
            EventType = EventType.Conference,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
            CreatedAt = DateTime.UtcNow
        };
        _context.Events.Add(conferenceEvent);
        await _context.SaveChangesAsync();

        var note = new Note
        {
            NoteId = Guid.NewGuid(),
            UserId = _testUserId,
            EventId = conferenceEvent.EventId,
            Content = "To Delete",
            CreatedAt = DateTime.UtcNow
        };
        _context.Notes.Add(note);
        await _context.SaveChangesAsync();

        // Act
        _context.Notes.Remove(note);
        await _context.SaveChangesAsync();

        // Assert
        var deleted = await _context.Notes.FindAsync(note.NoteId);
        Assert.That(deleted, Is.Null);
    }

    #endregion

    #region Relationship Tests

    /// <summary>
    /// Tests that relationships between events and sessions work correctly.
    /// </summary>
    [Test]
    public async Task EventSessionRelationship_ShouldLoadCorrectly()
    {
        // Arrange
        var conferenceEvent = new Event
        {
            EventId = Guid.NewGuid(),
            UserId = _testUserId,
            Name = "Test Event",
            EventType = EventType.Conference,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
            CreatedAt = DateTime.UtcNow
        };
        _context.Events.Add(conferenceEvent);
        await _context.SaveChangesAsync();

        var session = new Session
        {
            SessionId = Guid.NewGuid(),
            UserId = _testUserId,
            EventId = conferenceEvent.EventId,
            Title = "Test Session",
            StartTime = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };
        _context.Sessions.Add(session);
        await _context.SaveChangesAsync();

        // Act
        var loadedEvent = await _context.Events
            .Include(e => e.Sessions)
            .FirstOrDefaultAsync(e => e.EventId == conferenceEvent.EventId);

        // Assert
        Assert.That(loadedEvent, Is.Not.Null);
        Assert.That(loadedEvent.Sessions, Has.Count.EqualTo(1));
        Assert.That(loadedEvent.Sessions.First().Title, Is.EqualTo("Test Session"));
    }

    #endregion
}
