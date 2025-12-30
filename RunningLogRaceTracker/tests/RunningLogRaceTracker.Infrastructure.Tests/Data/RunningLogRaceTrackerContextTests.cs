// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RunningLogRaceTracker.Infrastructure.Tests;

/// <summary>
/// Unit tests for the RunningLogRaceTrackerContext.
/// </summary>
[TestFixture]
public class RunningLogRaceTrackerContextTests
{
    private RunningLogRaceTrackerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<RunningLogRaceTrackerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new RunningLogRaceTrackerContext(options);
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
    /// Tests that Runs can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Runs_CanAddAndRetrieve()
    {
        // Arrange
        var run = new Run
        {
            RunId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Distance = 10.5m,
            DurationMinutes = 55,
            CompletedAt = DateTime.UtcNow,
            AveragePace = 5.24m,
            AverageHeartRate = 158,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Runs.Add(run);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Runs.FindAsync(run.RunId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Distance, Is.EqualTo(10.5m));
        Assert.That(retrieved.DurationMinutes, Is.EqualTo(55));
    }

    /// <summary>
    /// Tests that Races can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Races_CanAddAndRetrieve()
    {
        // Arrange
        var race = new Race
        {
            RaceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Marathon",
            RaceType = RaceType.Marathon,
            RaceDate = DateTime.UtcNow.AddMonths(3),
            Location = "Test City",
            Distance = 42.2m,
            GoalTimeMinutes = 240,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Races.Add(race);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Races.FindAsync(race.RaceId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Marathon"));
        Assert.That(retrieved.RaceType, Is.EqualTo(RaceType.Marathon));
    }

    /// <summary>
    /// Tests that TrainingPlans can be added and retrieved.
    /// </summary>
    [Test]
    public async Task TrainingPlans_CanAddAndRetrieve()
    {
        // Arrange
        var plan = new TrainingPlan
        {
            TrainingPlanId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "16-Week Marathon Plan",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(4),
            WeeklyMileageGoal = 50,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.TrainingPlans.Add(plan);
        await _context.SaveChangesAsync();

        var retrieved = await _context.TrainingPlans.FindAsync(plan.TrainingPlanId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("16-Week Marathon Plan"));
        Assert.That(retrieved.WeeklyMileageGoal, Is.EqualTo(50));
    }

    /// <summary>
    /// Tests that TrainingPlan with Race relationship works.
    /// </summary>
    [Test]
    public async Task TrainingPlan_WithRace_LoadsRelationship()
    {
        // Arrange
        var race = new Race
        {
            RaceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Target Race",
            RaceType = RaceType.HalfMarathon,
            RaceDate = DateTime.UtcNow.AddMonths(3),
            Location = "Test City",
            Distance = 21.1m,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        var plan = new TrainingPlan
        {
            TrainingPlanId = Guid.NewGuid(),
            UserId = race.UserId,
            Name = "Half Marathon Prep",
            RaceId = race.RaceId,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(3),
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Races.Add(race);
        _context.TrainingPlans.Add(plan);
        await _context.SaveChangesAsync();

        var retrieved = await _context.TrainingPlans
            .Include(x => x.Race)
            .FirstOrDefaultAsync(x => x.TrainingPlanId == plan.TrainingPlanId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Race, Is.Not.Null);
        Assert.That(retrieved.Race!.Name, Is.EqualTo("Target Race"));
    }
}
