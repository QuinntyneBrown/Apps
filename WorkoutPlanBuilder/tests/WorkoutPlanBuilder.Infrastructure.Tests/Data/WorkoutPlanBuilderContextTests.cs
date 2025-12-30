// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WorkoutPlanBuilder.Infrastructure.Tests;

/// <summary>
/// Unit tests for the WorkoutPlanBuilderContext.
/// </summary>
[TestFixture]
public class WorkoutPlanBuilderContextTests
{
    private WorkoutPlanBuilderContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<WorkoutPlanBuilderContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new WorkoutPlanBuilderContext(options);
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
    /// Tests that Workouts can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Workouts_CanAddAndRetrieve()
    {
        // Arrange
        var workout = new Workout
        {
            WorkoutId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Workout",
            Description = "Test Description",
            TargetDurationMinutes = 45,
            DifficultyLevel = 3,
            Goal = "Strength",
            IsTemplate = true,
            IsActive = true,
            ScheduledDays = "[\"Monday\",\"Wednesday\"]",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Workouts.Add(workout);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Workouts.FindAsync(workout.WorkoutId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Workout"));
        Assert.That(retrieved.TargetDurationMinutes, Is.EqualTo(45));
        Assert.That(retrieved.DifficultyLevel, Is.EqualTo(3));
    }

    /// <summary>
    /// Tests that Exercises can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Exercises_CanAddAndRetrieve()
    {
        // Arrange
        var exercise = new Exercise
        {
            ExerciseId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Exercise",
            Description = "Test Description",
            ExerciseType = ExerciseType.Strength,
            PrimaryMuscleGroup = MuscleGroup.Chest,
            SecondaryMuscleGroups = "Shoulders, Triceps",
            Equipment = "Barbell",
            DifficultyLevel = 3,
            VideoUrl = "https://example.com/video",
            IsCustom = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Exercises.Add(exercise);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Exercises.FindAsync(exercise.ExerciseId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Exercise"));
        Assert.That(retrieved.ExerciseType, Is.EqualTo(ExerciseType.Strength));
        Assert.That(retrieved.PrimaryMuscleGroup, Is.EqualTo(MuscleGroup.Chest));
    }

    /// <summary>
    /// Tests that ProgressRecords can be added and retrieved.
    /// </summary>
    [Test]
    public async Task ProgressRecords_CanAddAndRetrieve()
    {
        // Arrange
        var workout = new Workout
        {
            WorkoutId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Workout",
            Description = "Test Description",
            TargetDurationMinutes = 45,
            DifficultyLevel = 3,
            CreatedAt = DateTime.UtcNow,
        };

        var progressRecord = new ProgressRecord
        {
            ProgressRecordId = Guid.NewGuid(),
            UserId = workout.UserId,
            WorkoutId = workout.WorkoutId,
            ActualDurationMinutes = 50,
            CaloriesBurned = 400,
            PerformanceRating = 4,
            Notes = "Great workout!",
            ExerciseDetails = "{\"sets\": 3, \"reps\": 10}",
            CompletedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Workouts.Add(workout);
        _context.ProgressRecords.Add(progressRecord);
        await _context.SaveChangesAsync();

        var retrieved = await _context.ProgressRecords.FindAsync(progressRecord.ProgressRecordId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.WorkoutId, Is.EqualTo(workout.WorkoutId));
        Assert.That(retrieved.ActualDurationMinutes, Is.EqualTo(50));
        Assert.That(retrieved.CaloriesBurned, Is.EqualTo(400));
        Assert.That(retrieved.PerformanceRating, Is.EqualTo(4));
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedEntities()
    {
        // Arrange
        var workout = new Workout
        {
            WorkoutId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Workout",
            Description = "Test Description",
            TargetDurationMinutes = 45,
            DifficultyLevel = 3,
            CreatedAt = DateTime.UtcNow,
        };

        var progressRecord = new ProgressRecord
        {
            ProgressRecordId = Guid.NewGuid(),
            UserId = workout.UserId,
            WorkoutId = workout.WorkoutId,
            ActualDurationMinutes = 50,
            CompletedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Workouts.Add(workout);
        _context.ProgressRecords.Add(progressRecord);
        await _context.SaveChangesAsync();

        // Act
        _context.Workouts.Remove(workout);
        await _context.SaveChangesAsync();

        var retrievedProgressRecord = await _context.ProgressRecords.FindAsync(progressRecord.ProgressRecordId);

        // Assert
        Assert.That(retrievedProgressRecord, Is.Null);
    }
}
