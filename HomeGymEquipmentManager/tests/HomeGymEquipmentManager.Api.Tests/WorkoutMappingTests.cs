// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeGymEquipmentManager.Api.Features.WorkoutMapping;
using HomeGymEquipmentManager.Api.Features.WorkoutMapping.Commands;
using HomeGymEquipmentManager.Api.Features.WorkoutMapping.Queries;
using HomeGymEquipmentManager.Core;
using Microsoft.EntityFrameworkCore;

namespace HomeGymEquipmentManager.Api.Tests;

public class WorkoutMappingTests
{
    private IHomeGymEquipmentManagerContext _context = null!;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<HomeGymEquipmentManager.Infrastructure.HomeGymEquipmentManagerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new HomeGymEquipmentManager.Infrastructure.HomeGymEquipmentManagerContext(options);
    }

    [Test]
    public async Task CreateWorkoutMappingCommand_ShouldCreateWorkoutMapping()
    {
        // Arrange
        var handler = new CreateWorkoutMappingCommandHandler(_context);
        var command = new CreateWorkoutMappingCommand
        {
            UserId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            ExerciseName = "Bench Press",
            MuscleGroup = "Chest",
            Instructions = "Lie on bench and press weight upward",
            IsFavorite = true
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.ExerciseName, Is.EqualTo("Bench Press"));
        Assert.That(result.MuscleGroup, Is.EqualTo("Chest"));
        Assert.That(result.IsFavorite, Is.True);
    }

    [Test]
    public async Task GetWorkoutMappingByIdQuery_ShouldReturnWorkoutMapping()
    {
        // Arrange
        var workoutMappingId = Guid.NewGuid();
        var workoutMapping = new WorkoutMapping
        {
            WorkoutMappingId = workoutMappingId,
            UserId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            ExerciseName = "Squats",
            MuscleGroup = "Legs",
            IsFavorite = false
        };
        _context.WorkoutMappings.Add(workoutMapping);
        await _context.SaveChangesAsync();

        var handler = new GetWorkoutMappingByIdQueryHandler(_context);
        var query = new GetWorkoutMappingByIdQuery { WorkoutMappingId = workoutMappingId };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.WorkoutMappingId, Is.EqualTo(workoutMappingId));
        Assert.That(result.ExerciseName, Is.EqualTo("Squats"));
    }

    [Test]
    public async Task UpdateWorkoutMappingCommand_ShouldUpdateWorkoutMapping()
    {
        // Arrange
        var workoutMappingId = Guid.NewGuid();
        var workoutMapping = new WorkoutMapping
        {
            WorkoutMappingId = workoutMappingId,
            UserId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            ExerciseName = "Old Exercise",
            MuscleGroup = "Arms",
            IsFavorite = false
        };
        _context.WorkoutMappings.Add(workoutMapping);
        await _context.SaveChangesAsync();

        var handler = new UpdateWorkoutMappingCommandHandler(_context);
        var command = new UpdateWorkoutMappingCommand
        {
            WorkoutMappingId = workoutMappingId,
            ExerciseName = "Updated Exercise",
            MuscleGroup = "Back",
            IsFavorite = true
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.ExerciseName, Is.EqualTo("Updated Exercise"));
        Assert.That(result.MuscleGroup, Is.EqualTo("Back"));
        Assert.That(result.IsFavorite, Is.True);
    }

    [Test]
    public async Task DeleteWorkoutMappingCommand_ShouldDeleteWorkoutMapping()
    {
        // Arrange
        var workoutMappingId = Guid.NewGuid();
        var workoutMapping = new WorkoutMapping
        {
            WorkoutMappingId = workoutMappingId,
            UserId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            ExerciseName = "To Delete",
            IsFavorite = false
        };
        _context.WorkoutMappings.Add(workoutMapping);
        await _context.SaveChangesAsync();

        var handler = new DeleteWorkoutMappingCommandHandler(_context);
        var command = new DeleteWorkoutMappingCommand { WorkoutMappingId = workoutMappingId };

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var deletedWorkoutMapping = await _context.WorkoutMappings.FirstOrDefaultAsync(w => w.WorkoutMappingId == workoutMappingId);
        Assert.That(deletedWorkoutMapping, Is.Null);
    }

    [Test]
    public async Task GetWorkoutMappingListQuery_ShouldReturnFilteredList()
    {
        // Arrange
        var equipmentId = Guid.NewGuid();
        _context.WorkoutMappings.Add(new WorkoutMapping
        {
            WorkoutMappingId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            EquipmentId = equipmentId,
            ExerciseName = "Exercise 1",
            IsFavorite = true
        });
        _context.WorkoutMappings.Add(new WorkoutMapping
        {
            WorkoutMappingId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            ExerciseName = "Exercise 2",
            IsFavorite = false
        });
        await _context.SaveChangesAsync();

        var handler = new GetWorkoutMappingListQueryHandler(_context);
        var query = new GetWorkoutMappingListQuery { EquipmentId = equipmentId, IsFavorite = true };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].ExerciseName, Is.EqualTo("Exercise 1"));
    }
}
