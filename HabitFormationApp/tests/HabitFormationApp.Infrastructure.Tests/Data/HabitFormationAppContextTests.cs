// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HabitFormationApp.Infrastructure.Tests;

/// <summary>
/// Unit tests for the HabitFormationAppContext.
/// </summary>
[TestFixture]
public class HabitFormationAppContextTests
{
    private HabitFormationAppContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<HabitFormationAppContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new HabitFormationAppContext(options);
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
    /// Tests that Habits can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Habits_CanAddAndRetrieve()
    {
        // Arrange
        var habit = new Habit
        {
            HabitId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Morning Exercise",
            Description = "30 minutes of cardio",
            Frequency = HabitFrequency.Daily,
            TargetDaysPerWeek = 7,
            StartDate = DateTime.UtcNow,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Habits.Add(habit);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Habits.FindAsync(habit.HabitId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Morning Exercise"));
        Assert.That(retrieved.Frequency, Is.EqualTo(HabitFrequency.Daily));
    }

    /// <summary>
    /// Tests that Streaks can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Streaks_CanAddAndRetrieve()
    {
        // Arrange
        var habit = new Habit
        {
            HabitId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Habit",
            Frequency = HabitFrequency.Daily,
            TargetDaysPerWeek = 7,
            StartDate = DateTime.UtcNow,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        var streak = new Streak
        {
            StreakId = Guid.NewGuid(),
            HabitId = habit.HabitId,
            CurrentStreak = 15,
            LongestStreak = 20,
            LastCompletedDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Habits.Add(habit);
        _context.Streaks.Add(streak);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Streaks.FindAsync(streak.StreakId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.CurrentStreak, Is.EqualTo(15));
        Assert.That(retrieved.LongestStreak, Is.EqualTo(20));
    }

    /// <summary>
    /// Tests that Reminders can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Reminders_CanAddAndRetrieve()
    {
        // Arrange
        var habit = new Habit
        {
            HabitId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Habit",
            Frequency = HabitFrequency.Daily,
            TargetDaysPerWeek = 7,
            StartDate = DateTime.UtcNow,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        var reminder = new Reminder
        {
            ReminderId = Guid.NewGuid(),
            UserId = habit.UserId,
            HabitId = habit.HabitId,
            ReminderTime = new TimeSpan(6, 0, 0),
            Message = "Time to exercise!",
            IsEnabled = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Habits.Add(habit);
        _context.Reminders.Add(reminder);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Reminders.FindAsync(reminder.ReminderId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.ReminderTime, Is.EqualTo(new TimeSpan(6, 0, 0)));
        Assert.That(retrieved.Message, Is.EqualTo("Time to exercise!"));
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedStreaks()
    {
        // Arrange
        var habit = new Habit
        {
            HabitId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Habit",
            Frequency = HabitFrequency.Daily,
            TargetDaysPerWeek = 7,
            StartDate = DateTime.UtcNow,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        var streak = new Streak
        {
            StreakId = Guid.NewGuid(),
            HabitId = habit.HabitId,
            CurrentStreak = 15,
            LongestStreak = 20,
            LastCompletedDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Habits.Add(habit);
        _context.Streaks.Add(streak);
        await _context.SaveChangesAsync();

        // Act
        _context.Habits.Remove(habit);
        await _context.SaveChangesAsync();

        var retrievedStreak = await _context.Streaks.FindAsync(streak.StreakId);

        // Assert
        Assert.That(retrievedStreak, Is.Null);
    }

    /// <summary>
    /// Tests that Habits can be updated.
    /// </summary>
    [Test]
    public async Task Habits_CanUpdate()
    {
        // Arrange
        var habit = new Habit
        {
            HabitId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Morning Exercise",
            Frequency = HabitFrequency.Daily,
            TargetDaysPerWeek = 7,
            StartDate = DateTime.UtcNow,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Habits.Add(habit);
        await _context.SaveChangesAsync();

        // Act
        habit.IsActive = false;
        habit.TargetDaysPerWeek = 5;
        await _context.SaveChangesAsync();

        var retrieved = await _context.Habits.FindAsync(habit.HabitId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.IsActive, Is.False);
        Assert.That(retrieved.TargetDaysPerWeek, Is.EqualTo(5));
    }
}
