// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HydrationTracker.Infrastructure.Tests;

/// <summary>
/// Unit tests for the HydrationTrackerContext.
/// </summary>
[TestFixture]
public class HydrationTrackerContextTests
{
    private HydrationTrackerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<HydrationTrackerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new HydrationTrackerContext(options);
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
            DailyGoalMl = 2000m,
            StartDate = DateTime.UtcNow.Date,
            IsActive = true,
            Notes = "Test Goal",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Goals.Add(goal);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Goals.FindAsync(goal.GoalId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.DailyGoalMl, Is.EqualTo(2000m));
        Assert.That(retrieved.IsActive, Is.True);
    }

    /// <summary>
    /// Tests that Intakes can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Intakes_CanAddAndRetrieve()
    {
        // Arrange
        var intake = new Intake
        {
            IntakeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            BeverageType = BeverageType.Water,
            AmountMl = 250m,
            IntakeTime = DateTime.UtcNow,
            Notes = "Morning water",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Intakes.Add(intake);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Intakes.FindAsync(intake.IntakeId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.AmountMl, Is.EqualTo(250m));
        Assert.That(retrieved.BeverageType, Is.EqualTo(BeverageType.Water));
        Assert.That(retrieved.Notes, Is.EqualTo("Morning water"));
    }

    /// <summary>
    /// Tests that Reminders can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Reminders_CanAddAndRetrieve()
    {
        // Arrange
        var reminder = new Reminder
        {
            ReminderId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ReminderTime = new TimeSpan(9, 0, 0),
            Message = "Time to hydrate!",
            IsEnabled = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Reminders.Add(reminder);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Reminders.FindAsync(reminder.ReminderId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.ReminderTime, Is.EqualTo(new TimeSpan(9, 0, 0)));
        Assert.That(retrieved.Message, Is.EqualTo("Time to hydrate!"));
        Assert.That(retrieved.IsEnabled, Is.True);
    }

    /// <summary>
    /// Tests that multiple intakes can be queried by user.
    /// </summary>
    [Test]
    public async Task Intakes_CanQueryByUser()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var intakes = new List<Intake>
        {
            new Intake
            {
                IntakeId = Guid.NewGuid(),
                UserId = userId,
                BeverageType = BeverageType.Water,
                AmountMl = 250m,
                IntakeTime = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
            },
            new Intake
            {
                IntakeId = Guid.NewGuid(),
                UserId = userId,
                BeverageType = BeverageType.Coffee,
                AmountMl = 200m,
                IntakeTime = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
            },
        };

        // Act
        _context.Intakes.AddRange(intakes);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Intakes
            .Where(i => i.UserId == userId)
            .ToListAsync();

        // Assert
        Assert.That(retrieved, Has.Count.EqualTo(2));
        Assert.That(retrieved.Sum(i => i.AmountMl), Is.EqualTo(450m));
    }
}
