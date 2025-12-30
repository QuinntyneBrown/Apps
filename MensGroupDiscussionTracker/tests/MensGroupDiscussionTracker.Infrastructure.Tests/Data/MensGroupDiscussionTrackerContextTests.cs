// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MensGroupDiscussionTracker.Infrastructure.Tests;

/// <summary>
/// Unit tests for the MensGroupDiscussionTrackerContext.
/// </summary>
[TestFixture]
public class MensGroupDiscussionTrackerContextTests
{
    private MensGroupDiscussionTrackerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<MensGroupDiscussionTrackerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new MensGroupDiscussionTrackerContext(options);
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
            Description = "Test Description",
            MeetingSchedule = "Weekly",
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
    /// Tests that Meetings can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Meetings_CanAddAndRetrieve()
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

        var meeting = new Meeting
        {
            MeetingId = Guid.NewGuid(),
            GroupId = group.GroupId,
            Title = "Test Meeting",
            MeetingDateTime = DateTime.UtcNow,
            AttendeeCount = 10,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Groups.Add(group);
        _context.Meetings.Add(meeting);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Meetings.FindAsync(meeting.MeetingId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Test Meeting"));
        Assert.That(retrieved.AttendeeCount, Is.EqualTo(10));
    }

    /// <summary>
    /// Tests that Topics can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Topics_CanAddAndRetrieve()
    {
        // Arrange
        var topic = new Topic
        {
            TopicId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Topic",
            Description = "Test Description",
            Category = TopicCategory.PersonalGrowth,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Topics.Add(topic);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Topics.FindAsync(topic.TopicId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Test Topic"));
        Assert.That(retrieved.Category, Is.EqualTo(TopicCategory.PersonalGrowth));
    }

    /// <summary>
    /// Tests that Resources can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Resources_CanAddAndRetrieve()
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

        var resource = new Resource
        {
            ResourceId = Guid.NewGuid(),
            GroupId = group.GroupId,
            SharedByUserId = group.CreatedByUserId,
            Title = "Test Resource",
            Description = "Test Description",
            Url = "https://example.com",
            ResourceType = "Article",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Groups.Add(group);
        _context.Resources.Add(resource);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Resources.FindAsync(resource.ResourceId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Test Resource"));
        Assert.That(retrieved.ResourceType, Is.EqualTo("Article"));
    }

    /// <summary>
    /// Tests cascade delete behavior.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedEntities()
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

        var meeting = new Meeting
        {
            MeetingId = Guid.NewGuid(),
            GroupId = group.GroupId,
            Title = "Test Meeting",
            MeetingDateTime = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Groups.Add(group);
        _context.Meetings.Add(meeting);
        await _context.SaveChangesAsync();

        // Act
        _context.Groups.Remove(group);
        await _context.SaveChangesAsync();

        var retrievedMeeting = await _context.Meetings.FindAsync(meeting.MeetingId);

        // Assert
        Assert.That(retrievedMeeting, Is.Null);
    }
}
