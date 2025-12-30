// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using WorkoutPlanBuilder.Api.Features.Exercises;
using WorkoutPlanBuilder.Core;

namespace WorkoutPlanBuilder.Api.Tests.Features;

[TestFixture]
public class CreateExerciseCommandTests
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
    public async Task Handle_CreatesExerciseSuccessfully()
    {
        // Arrange
        var handler = new CreateExerciseCommandHandler(_context);
        var command = new CreateExerciseCommand
        {
            UserId = Guid.NewGuid(),
            Name = "Bench Press",
            Description = "Chest exercise",
            ExerciseType = ExerciseType.Strength,
            PrimaryMuscleGroup = MuscleGroup.Chest,
            Equipment = "Barbell",
            DifficultyLevel = 3,
            IsCustom = true
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.ExerciseId, Is.Not.EqualTo(Guid.Empty));
        Assert.That(result.Name, Is.EqualTo(command.Name));
        Assert.That(result.ExerciseType, Is.EqualTo(command.ExerciseType));
        Assert.That(result.PrimaryMuscleGroup, Is.EqualTo(command.PrimaryMuscleGroup));

        var exercise = await _context.Exercises.FindAsync(result.ExerciseId);
        Assert.That(exercise, Is.Not.Null);
        Assert.That(exercise!.Name, Is.EqualTo(command.Name));
    }
}
