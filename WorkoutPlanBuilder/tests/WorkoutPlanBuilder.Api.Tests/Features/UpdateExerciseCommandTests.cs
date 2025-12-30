// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using WorkoutPlanBuilder.Api.Features.Exercises;
using WorkoutPlanBuilder.Core;

namespace WorkoutPlanBuilder.Api.Tests.Features;

[TestFixture]
public class UpdateExerciseCommandTests
{
    private IWorkoutPlanBuilderContext _context;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<WorkoutPlanBuilderContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new WorkoutPlanBuilderContext(options);
    }

    [TearDown]
    public void TearDown()
    {
        if (_context is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }

    [Test]
    public async Task Handle_UpdatesExerciseSuccessfully()
    {
        // Arrange
        var exercise = new Exercise
        {
            ExerciseId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Original Name",
            ExerciseType = ExerciseType.Strength,
            PrimaryMuscleGroup = MuscleGroup.Chest,
            DifficultyLevel = 2
        };
        _context.Exercises.Add(exercise);
        await _context.SaveChangesAsync();

        var handler = new UpdateExerciseCommandHandler(_context);
        var command = new UpdateExerciseCommand
        {
            ExerciseId = exercise.ExerciseId,
            Name = "Updated Name",
            Description = "Updated Description",
            ExerciseType = ExerciseType.Cardio,
            PrimaryMuscleGroup = MuscleGroup.Legs,
            DifficultyLevel = 3
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Name, Is.EqualTo(command.Name));
        Assert.That(result.Description, Is.EqualTo(command.Description));
        Assert.That(result.ExerciseType, Is.EqualTo(command.ExerciseType));
        Assert.That(result.PrimaryMuscleGroup, Is.EqualTo(command.PrimaryMuscleGroup));
    }

    [Test]
    public async Task Handle_WithNonExistentExercise_ReturnsNull()
    {
        // Arrange
        var handler = new UpdateExerciseCommandHandler(_context);
        var command = new UpdateExerciseCommand
        {
            ExerciseId = Guid.NewGuid(),
            Name = "Updated Name",
            ExerciseType = ExerciseType.Strength,
            PrimaryMuscleGroup = MuscleGroup.Chest,
            DifficultyLevel = 3
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Null);
    }
}
