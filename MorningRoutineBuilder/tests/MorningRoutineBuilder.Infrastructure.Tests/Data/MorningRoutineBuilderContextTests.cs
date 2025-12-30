// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MorningRoutineBuilder.Infrastructure.Tests;

/// <summary>
/// Unit tests for the MorningRoutineBuilderContext.
/// </summary>
[TestFixture]
public class MorningRoutineBuilderContextTests
{
    private MorningRoutineBuilderContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<MorningRoutineBuilderContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new MorningRoutineBuilderContext(options);
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
    /// Tests that Routines can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Routines_CanAddAndRetrieve()
    {
        // Arrange
        var routine = new Routine
        {
            RoutineId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Routine",
            Description = "Test Description",
            TargetStartTime = new TimeSpan(6, 0, 0),
            EstimatedDurationMinutes = 60,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Routines.Add(routine);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Routines.FindAsync(routine.RoutineId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Routine"));
        Assert.That(retrieved.EstimatedDurationMinutes, Is.EqualTo(60));
    }

    /// <summary>
    /// Tests that RoutineTasks can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Tasks_CanAddAndRetrieve()
    {
        // Arrange
        var routine = new Routine
        {
            RoutineId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Routine",
            TargetStartTime = new TimeSpan(6, 0, 0),
            EstimatedDurationMinutes = 30,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        var task = new RoutineTask
        {
            RoutineTaskId = Guid.NewGuid(),
            RoutineId = routine.RoutineId,
            Name = "Test Task",
            TaskType = TaskType.Exercise,
            EstimatedDurationMinutes = 20,
            SortOrder = 1,
            IsOptional = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Routines.Add(routine);
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Tasks.FindAsync(task.RoutineTaskId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Task"));
        Assert.That(retrieved.TaskType, Is.EqualTo(TaskType.Exercise));
    }

    /// <summary>
    /// Tests that CompletionLogs can be added and retrieved.
    /// </summary>
    [Test]
    public async Task CompletionLogs_CanAddAndRetrieve()
    {
        // Arrange
        var routine = new Routine
        {
            RoutineId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Routine",
            TargetStartTime = new TimeSpan(6, 0, 0),
            EstimatedDurationMinutes = 30,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        var completionLog = new CompletionLog
        {
            CompletionLogId = Guid.NewGuid(),
            RoutineId = routine.RoutineId,
            UserId = routine.UserId,
            CompletionDate = DateTime.UtcNow.Date,
            TasksCompleted = 5,
            TotalTasks = 5,
            MoodRating = 9,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Routines.Add(routine);
        _context.CompletionLogs.Add(completionLog);
        await _context.SaveChangesAsync();

        var retrieved = await _context.CompletionLogs.FindAsync(completionLog.CompletionLogId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.TasksCompleted, Is.EqualTo(5));
        Assert.That(retrieved.MoodRating, Is.EqualTo(9));
    }

    /// <summary>
    /// Tests that Streaks can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Streaks_CanAddAndRetrieve()
    {
        // Arrange
        var routine = new Routine
        {
            RoutineId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Routine",
            TargetStartTime = new TimeSpan(6, 0, 0),
            EstimatedDurationMinutes = 30,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        var streak = new Streak
        {
            StreakId = Guid.NewGuid(),
            RoutineId = routine.RoutineId,
            UserId = routine.UserId,
            CurrentStreak = 5,
            LongestStreak = 10,
            LastCompletionDate = DateTime.UtcNow.Date,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Routines.Add(routine);
        _context.Streaks.Add(streak);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Streaks.FindAsync(streak.StreakId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.CurrentStreak, Is.EqualTo(5));
        Assert.That(retrieved.LongestStreak, Is.EqualTo(10));
    }

    /// <summary>
    /// Tests cascade delete behavior.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedEntities()
    {
        // Arrange
        var routine = new Routine
        {
            RoutineId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Routine",
            TargetStartTime = new TimeSpan(6, 0, 0),
            EstimatedDurationMinutes = 30,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        var task = new RoutineTask
        {
            RoutineTaskId = Guid.NewGuid(),
            RoutineId = routine.RoutineId,
            Name = "Test Task",
            TaskType = TaskType.Mindfulness,
            EstimatedDurationMinutes = 10,
            SortOrder = 1,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Routines.Add(routine);
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        // Act
        _context.Routines.Remove(routine);
        await _context.SaveChangesAsync();

        var retrievedTask = await _context.Tasks.FindAsync(task.RoutineTaskId);

        // Assert
        Assert.That(retrievedTask, Is.Null);
    }
}
