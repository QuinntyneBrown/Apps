// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace InjuryPreventionRecoveryTracker.Infrastructure.Tests;

/// <summary>
/// Unit tests for the InjuryPreventionRecoveryTrackerContext.
/// </summary>
[TestFixture]
public class InjuryPreventionRecoveryTrackerContextTests
{
    private InjuryPreventionRecoveryTrackerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<InjuryPreventionRecoveryTrackerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new InjuryPreventionRecoveryTrackerContext(options);
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
    /// Tests that Injuries can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Injuries_CanAddAndRetrieve()
    {
        // Arrange
        var injury = new Injury
        {
            InjuryId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            InjuryType = InjuryType.Sprain,
            Severity = InjurySeverity.Moderate,
            BodyPart = "Right Ankle",
            InjuryDate = DateTime.UtcNow,
            Description = "Test injury",
            Status = "Active",
            ProgressPercentage = 0,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Injuries.Add(injury);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Injuries.FindAsync(injury.InjuryId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.BodyPart, Is.EqualTo("Right Ankle"));
        Assert.That(retrieved.InjuryType, Is.EqualTo(InjuryType.Sprain));
        Assert.That(retrieved.Severity, Is.EqualTo(InjurySeverity.Moderate));
    }

    /// <summary>
    /// Tests that RecoveryExercises can be added and retrieved.
    /// </summary>
    [Test]
    public async Task RecoveryExercises_CanAddAndRetrieve()
    {
        // Arrange
        var injury = new Injury
        {
            InjuryId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            InjuryType = InjuryType.Sprain,
            Severity = InjurySeverity.Moderate,
            BodyPart = "Right Ankle",
            InjuryDate = DateTime.UtcNow,
            Status = "Active",
            CreatedAt = DateTime.UtcNow,
        };

        var exercise = new RecoveryExercise
        {
            RecoveryExerciseId = Guid.NewGuid(),
            UserId = injury.UserId,
            InjuryId = injury.InjuryId,
            Name = "Ankle Circles",
            Description = "Rotate ankle slowly",
            Frequency = "3 times daily",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Injuries.Add(injury);
        _context.RecoveryExercises.Add(exercise);
        await _context.SaveChangesAsync();

        var retrieved = await _context.RecoveryExercises.FindAsync(exercise.RecoveryExerciseId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Ankle Circles"));
        Assert.That(retrieved.Frequency, Is.EqualTo("3 times daily"));
        Assert.That(retrieved.IsActive, Is.True);
    }

    /// <summary>
    /// Tests that Milestones can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Milestones_CanAddAndRetrieve()
    {
        // Arrange
        var injury = new Injury
        {
            InjuryId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            InjuryType = InjuryType.Sprain,
            Severity = InjurySeverity.Moderate,
            BodyPart = "Right Ankle",
            InjuryDate = DateTime.UtcNow,
            Status = "Active",
            CreatedAt = DateTime.UtcNow,
        };

        var milestone = new Milestone
        {
            MilestoneId = Guid.NewGuid(),
            UserId = injury.UserId,
            InjuryId = injury.InjuryId,
            Name = "Walk without limping",
            Description = "Able to walk normally",
            TargetDate = DateTime.UtcNow.AddDays(14),
            IsAchieved = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Injuries.Add(injury);
        _context.Milestones.Add(milestone);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Milestones.FindAsync(milestone.MilestoneId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Walk without limping"));
        Assert.That(retrieved.IsAchieved, Is.False);
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedEntities()
    {
        // Arrange
        var injury = new Injury
        {
            InjuryId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            InjuryType = InjuryType.Sprain,
            Severity = InjurySeverity.Moderate,
            BodyPart = "Right Ankle",
            InjuryDate = DateTime.UtcNow,
            Status = "Active",
            CreatedAt = DateTime.UtcNow,
        };

        var exercise = new RecoveryExercise
        {
            RecoveryExerciseId = Guid.NewGuid(),
            UserId = injury.UserId,
            InjuryId = injury.InjuryId,
            Name = "Test Exercise",
            Frequency = "Daily",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Injuries.Add(injury);
        _context.RecoveryExercises.Add(exercise);
        await _context.SaveChangesAsync();

        // Act
        _context.Injuries.Remove(injury);
        await _context.SaveChangesAsync();

        var retrievedExercise = await _context.RecoveryExercises.FindAsync(exercise.RecoveryExerciseId);

        // Assert
        Assert.That(retrievedExercise, Is.Null);
    }
}
