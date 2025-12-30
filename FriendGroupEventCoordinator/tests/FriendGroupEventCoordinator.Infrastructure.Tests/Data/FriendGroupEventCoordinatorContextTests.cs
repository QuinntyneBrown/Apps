// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FriendGroupEventCoordinator.Infrastructure.Tests;

/// <summary>
/// Unit tests for the FriendGroupEventCoordinatorContext.
/// </summary>
[TestFixture]
public class FriendGroupEventCoordinatorContextTests
{
    private FriendGroupEventCoordinatorContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<FriendGroupEventCoordinatorContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new FriendGroupEventCoordinatorContext(options);
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
    /// Tests that Groups can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Groups_CanAddAndRetrieve()
    {
        // Arrange
        var group = new Group
        {
            GroupId = Guid.NewGuid(),
            CreatedByUserId = Guid.NewGuid(),
            Name = "Test Group",
            Description = "A test group",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Groups.Add(group);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Groups.FindAsync(group.GroupId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Group"));
        Assert.That(retrieved.IsActive, Is.True);
    }

    /// <summary>
    /// Tests that Members can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Members_CanAddAndRetrieve()
    {
        // Arrange
        var group = new Group
        {
            GroupId = Guid.NewGuid(),
            CreatedByUserId = Guid.NewGuid(),
            Name = "Test Group",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        var member = new Member
        {
            MemberId = Guid.NewGuid(),
            GroupId = group.GroupId,
            UserId = Guid.NewGuid(),
            Name = "Test Member",
            Email = "test@example.com",
            IsAdmin = false,
            IsActive = true,
            JoinedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Groups.Add(group);
        _context.Members.Add(member);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Members.FindAsync(member.MemberId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Member"));
        Assert.That(retrieved.Email, Is.EqualTo("test@example.com"));
    }

    /// <summary>
    /// Tests that Events can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Events_CanAddAndRetrieve()
    {
        // Arrange
        var group = new Group
        {
            GroupId = Guid.NewGuid(),
            CreatedByUserId = Guid.NewGuid(),
            Name = "Test Group",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        var eventEntity = new Event
        {
            EventId = Guid.NewGuid(),
            GroupId = group.GroupId,
            CreatedByUserId = group.CreatedByUserId,
            Title = "Test Event",
            Description = "A test event",
            EventType = EventType.Social,
            StartDateTime = DateTime.UtcNow.AddDays(7),
            Location = "Test Location",
            IsCancelled = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Groups.Add(group);
        _context.Events.Add(eventEntity);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Events.FindAsync(eventEntity.EventId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Test Event"));
        Assert.That(retrieved.EventType, Is.EqualTo(EventType.Social));
    }

    /// <summary>
    /// Tests that RSVPs can be added and retrieved.
    /// </summary>
    [Test]
    public async Task RSVPs_CanAddAndRetrieve()
    {
        // Arrange
        var group = new Group
        {
            GroupId = Guid.NewGuid(),
            CreatedByUserId = Guid.NewGuid(),
            Name = "Test Group",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        var member = new Member
        {
            MemberId = Guid.NewGuid(),
            GroupId = group.GroupId,
            UserId = Guid.NewGuid(),
            Name = "Test Member",
            IsAdmin = false,
            IsActive = true,
            JoinedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        var eventEntity = new Event
        {
            EventId = Guid.NewGuid(),
            GroupId = group.GroupId,
            CreatedByUserId = group.CreatedByUserId,
            Title = "Test Event",
            Description = "Test",
            EventType = EventType.Social,
            StartDateTime = DateTime.UtcNow.AddDays(7),
            IsCancelled = false,
            CreatedAt = DateTime.UtcNow,
        };

        var rsvp = new RSVP
        {
            RSVPId = Guid.NewGuid(),
            EventId = eventEntity.EventId,
            MemberId = member.MemberId,
            UserId = member.UserId,
            Response = RSVPResponse.Yes,
            AdditionalGuests = 2,
            Notes = "Looking forward to it!",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Groups.Add(group);
        _context.Members.Add(member);
        _context.Events.Add(eventEntity);
        _context.RSVPs.Add(rsvp);
        await _context.SaveChangesAsync();

        var retrieved = await _context.RSVPs.FindAsync(rsvp.RSVPId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Response, Is.EqualTo(RSVPResponse.Yes));
        Assert.That(retrieved.AdditionalGuests, Is.EqualTo(2));
    }

    /// <summary>
    /// Tests that cascade delete works for Group and Members.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedMembers()
    {
        // Arrange
        var group = new Group
        {
            GroupId = Guid.NewGuid(),
            CreatedByUserId = Guid.NewGuid(),
            Name = "Test Group",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        var member = new Member
        {
            MemberId = Guid.NewGuid(),
            GroupId = group.GroupId,
            UserId = Guid.NewGuid(),
            Name = "Test Member",
            IsAdmin = false,
            IsActive = true,
            JoinedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Groups.Add(group);
        _context.Members.Add(member);
        await _context.SaveChangesAsync();

        // Act
        _context.Groups.Remove(group);
        await _context.SaveChangesAsync();

        var retrievedMember = await _context.Members.FindAsync(member.MemberId);

        // Assert
        Assert.That(retrievedMember, Is.Null);
    }

    /// <summary>
    /// Tests that cascade delete works for Member and RSVPs.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedRSVPs()
    {
        // Arrange
        var group = new Group
        {
            GroupId = Guid.NewGuid(),
            CreatedByUserId = Guid.NewGuid(),
            Name = "Test Group",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        var member = new Member
        {
            MemberId = Guid.NewGuid(),
            GroupId = group.GroupId,
            UserId = Guid.NewGuid(),
            Name = "Test Member",
            IsAdmin = false,
            IsActive = true,
            JoinedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        var eventEntity = new Event
        {
            EventId = Guid.NewGuid(),
            GroupId = group.GroupId,
            CreatedByUserId = group.CreatedByUserId,
            Title = "Test Event",
            Description = "Test",
            EventType = EventType.Social,
            StartDateTime = DateTime.UtcNow.AddDays(7),
            IsCancelled = false,
            CreatedAt = DateTime.UtcNow,
        };

        var rsvp = new RSVP
        {
            RSVPId = Guid.NewGuid(),
            EventId = eventEntity.EventId,
            MemberId = member.MemberId,
            UserId = member.UserId,
            Response = RSVPResponse.Yes,
            AdditionalGuests = 0,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Groups.Add(group);
        _context.Members.Add(member);
        _context.Events.Add(eventEntity);
        _context.RSVPs.Add(rsvp);
        await _context.SaveChangesAsync();

        // Act
        _context.Members.Remove(member);
        await _context.SaveChangesAsync();

        var retrievedRSVP = await _context.RSVPs.FindAsync(rsvp.RSVPId);

        // Assert
        Assert.That(retrievedRSVP, Is.Null);
    }
}
