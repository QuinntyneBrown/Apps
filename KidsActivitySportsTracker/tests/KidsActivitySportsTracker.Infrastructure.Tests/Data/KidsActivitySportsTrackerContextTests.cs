// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace KidsActivitySportsTracker.Infrastructure.Tests;

/// <summary>
/// Unit tests for the KidsActivitySportsTrackerContext.
/// </summary>
[TestFixture]
public class KidsActivitySportsTrackerContextTests
{
    private KidsActivitySportsTrackerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<KidsActivitySportsTrackerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new KidsActivitySportsTrackerContext(options);
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
    /// Tests that Activities can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Activities_CanAddAndRetrieve()
    {
        // Arrange
        var activity = new Activity
        {
            ActivityId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ChildName = "Test Child",
            Name = "Soccer",
            ActivityType = ActivityType.Team,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Activities.Add(activity);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Activities.FindAsync(activity.ActivityId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.ChildName, Is.EqualTo("Test Child"));
        Assert.That(retrieved.Name, Is.EqualTo("Soccer"));
        Assert.That(retrieved.ActivityType, Is.EqualTo(ActivityType.Team));
    }

    /// <summary>
    /// Tests that Schedules can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Schedules_CanAddAndRetrieve()
    {
        // Arrange
        var activity = new Activity
        {
            ActivityId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ChildName = "Test Child",
            Name = "Soccer",
            ActivityType = ActivityType.Team,
            CreatedAt = DateTime.UtcNow,
        };

        var schedule = new Schedule
        {
            ScheduleId = Guid.NewGuid(),
            ActivityId = activity.ActivityId,
            EventType = "Practice",
            DateTime = DateTime.UtcNow.AddDays(1),
            IsConfirmed = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Activities.Add(activity);
        _context.Schedules.Add(schedule);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Schedules.FindAsync(schedule.ScheduleId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.EventType, Is.EqualTo("Practice"));
        Assert.That(retrieved.ActivityId, Is.EqualTo(activity.ActivityId));
        Assert.That(retrieved.IsConfirmed, Is.True);
    }

    /// <summary>
    /// Tests that Carpools can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Carpools_CanAddAndRetrieve()
    {
        // Arrange
        var carpool = new Carpool
        {
            CarpoolId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Soccer Carpool",
            DriverName = "Test Driver",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Carpools.Add(carpool);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Carpools.FindAsync(carpool.CarpoolId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Soccer Carpool"));
        Assert.That(retrieved.DriverName, Is.EqualTo("Test Driver"));
    }

    /// <summary>
    /// Tests that cascade delete works for related schedules.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedSchedules()
    {
        // Arrange
        var activity = new Activity
        {
            ActivityId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ChildName = "Test Child",
            Name = "Soccer",
            ActivityType = ActivityType.Team,
            CreatedAt = DateTime.UtcNow,
        };

        var schedule = new Schedule
        {
            ScheduleId = Guid.NewGuid(),
            ActivityId = activity.ActivityId,
            EventType = "Practice",
            DateTime = DateTime.UtcNow.AddDays(1),
            IsConfirmed = true,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Activities.Add(activity);
        _context.Schedules.Add(schedule);
        await _context.SaveChangesAsync();

        // Act
        _context.Activities.Remove(activity);
        await _context.SaveChangesAsync();

        var retrievedSchedule = await _context.Schedules.FindAsync(schedule.ScheduleId);

        // Assert
        Assert.That(retrievedSchedule, Is.Null);
    }
}
