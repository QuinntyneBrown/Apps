// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CouplesGoalTracker.Infrastructure.Tests;

/// <summary>
/// Unit tests for the CouplesGoalTrackerContext.
/// </summary>
[TestFixture]
public class CouplesGoalTrackerContextTests
{
    private CouplesGoalTrackerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<CouplesGoalTrackerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new CouplesGoalTrackerContext(options);
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
    /// Tests that Goals can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Goals_CanAddAndRetrieve()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Goal",
            Description = "Test Description",
            Category = GoalCategory.Communication,
            Status = GoalStatus.NotStarted,
            Priority = 3,
            IsShared = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Goals.Add(goal);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Goals.FindAsync(goal.GoalId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Test Goal"));
        Assert.That(retrieved.Category, Is.EqualTo(GoalCategory.Communication));
    }

    /// <summary>
    /// Tests that Milestones can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Milestones_CanAddAndRetrieve()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Goal",
            Description = "Test Description",
            Category = GoalCategory.Financial,
            Status = GoalStatus.InProgress,
            CreatedAt = DateTime.UtcNow,
        };

        var milestone = new Milestone
        {
            MilestoneId = Guid.NewGuid(),
            GoalId = goal.GoalId,
            UserId = goal.UserId,
            Title = "Test Milestone",
            Description = "Test milestone description",
            IsCompleted = false,
            SortOrder = 1,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Goals.Add(goal);
        _context.Milestones.Add(milestone);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Milestones.FindAsync(milestone.MilestoneId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Test Milestone"));
        Assert.That(retrieved.GoalId, Is.EqualTo(goal.GoalId));
    }

    /// <summary>
    /// Tests that Progresses can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Progresses_CanAddAndRetrieve()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Goal",
            Description = "Test Description",
            Category = GoalCategory.HealthAndWellness,
            Status = GoalStatus.InProgress,
            CreatedAt = DateTime.UtcNow,
        };

        var progress = new Progress
        {
            ProgressId = Guid.NewGuid(),
            GoalId = goal.GoalId,
            UserId = goal.UserId,
            ProgressDate = DateTime.UtcNow,
            Notes = "Made significant progress today",
            CompletionPercentage = 50,
            IsSignificant = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Goals.Add(goal);
        _context.Progresses.Add(progress);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Progresses.FindAsync(progress.ProgressId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Notes, Is.EqualTo("Made significant progress today"));
        Assert.That(retrieved.CompletionPercentage, Is.EqualTo(50));
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedEntities()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Goal",
            Description = "Test Description",
            Category = GoalCategory.AdventureAndTravel,
            Status = GoalStatus.NotStarted,
            CreatedAt = DateTime.UtcNow,
        };

        var milestone = new Milestone
        {
            MilestoneId = Guid.NewGuid(),
            GoalId = goal.GoalId,
            UserId = goal.UserId,
            Title = "Test Milestone",
            IsCompleted = false,
            SortOrder = 1,
            CreatedAt = DateTime.UtcNow,
        };

        var progress = new Progress
        {
            ProgressId = Guid.NewGuid(),
            GoalId = goal.GoalId,
            UserId = goal.UserId,
            ProgressDate = DateTime.UtcNow,
            Notes = "Test progress",
            CompletionPercentage = 25,
            IsSignificant = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Goals.Add(goal);
        _context.Milestones.Add(milestone);
        _context.Progresses.Add(progress);
        await _context.SaveChangesAsync();

        // Act
        _context.Goals.Remove(goal);
        await _context.SaveChangesAsync();

        var retrievedMilestone = await _context.Milestones.FindAsync(milestone.MilestoneId);
        var retrievedProgress = await _context.Progresses.FindAsync(progress.ProgressId);

        // Assert
        Assert.That(retrievedMilestone, Is.Null);
        Assert.That(retrievedProgress, Is.Null);
    }

    /// <summary>
    /// Tests that multiple milestones can be associated with a goal.
    /// </summary>
    [Test]
    public async Task Goal_CanHaveMultipleMilestones()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Goal",
            Description = "Test Description",
            Category = GoalCategory.QualityTime,
            Status = GoalStatus.InProgress,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Goals.Add(goal);
        await _context.SaveChangesAsync();

        var milestone1 = new Milestone
        {
            MilestoneId = Guid.NewGuid(),
            GoalId = goal.GoalId,
            UserId = goal.UserId,
            Title = "Milestone 1",
            IsCompleted = true,
            SortOrder = 1,
            CreatedAt = DateTime.UtcNow,
        };

        var milestone2 = new Milestone
        {
            MilestoneId = Guid.NewGuid(),
            GoalId = goal.GoalId,
            UserId = goal.UserId,
            Title = "Milestone 2",
            IsCompleted = false,
            SortOrder = 2,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Milestones.AddRange(milestone1, milestone2);
        await _context.SaveChangesAsync();

        var milestones = await _context.Milestones
            .Where(m => m.GoalId == goal.GoalId)
            .ToListAsync();

        // Assert
        Assert.That(milestones, Has.Count.EqualTo(2));
        Assert.That(milestones.Any(m => m.Title == "Milestone 1"), Is.True);
        Assert.That(milestones.Any(m => m.Title == "Milestone 2"), Is.True);
    }
}
