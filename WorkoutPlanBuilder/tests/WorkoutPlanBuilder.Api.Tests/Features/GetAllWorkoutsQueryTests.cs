// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using WorkoutPlanBuilder.Api.Features.Workouts;
using WorkoutPlanBuilder.Core;

namespace WorkoutPlanBuilder.Api.Tests.Features;

[TestFixture]
public class GetAllWorkoutsQueryTests
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
    public async Task Handle_ReturnsAllWorkouts()
    {
        // Arrange
        var workout1 = new Workout
        {
            WorkoutId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Workout 1",
            TargetDurationMinutes = 30,
            DifficultyLevel = 2,
            CreatedAt = DateTime.UtcNow.AddDays(-2)
        };
        var workout2 = new Workout
        {
            WorkoutId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Workout 2",
            TargetDurationMinutes = 45,
            DifficultyLevel = 3,
            CreatedAt = DateTime.UtcNow.AddDays(-1)
        };

        _context.Workouts.Add(workout1);
        _context.Workouts.Add(workout2);
        await _context.SaveChangesAsync();

        var handler = new GetAllWorkoutsQueryHandler(_context);
        var query = new GetAllWorkoutsQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result[0].Name, Is.EqualTo("Workout 2")); // Most recent first
        Assert.That(result[1].Name, Is.EqualTo("Workout 1"));
    }

    [Test]
    public async Task Handle_WithNoWorkouts_ReturnsEmptyList()
    {
        // Arrange
        var handler = new GetAllWorkoutsQueryHandler(_context);
        var query = new GetAllWorkoutsQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(0));
    }
}
