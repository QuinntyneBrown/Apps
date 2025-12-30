// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using WorkoutPlanBuilder.Api.Features.Workouts;
using WorkoutPlanBuilder.Core;

namespace WorkoutPlanBuilder.Api.Tests.Features;

[TestFixture]
public class CreateWorkoutCommandTests
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
    public async Task Handle_CreatesWorkoutSuccessfully()
    {
        // Arrange
        var handler = new CreateWorkoutCommandHandler(_context);
        var command = new CreateWorkoutCommand
        {
            UserId = Guid.NewGuid(),
            Name = "Test Workout",
            Description = "Test Description",
            TargetDurationMinutes = 60,
            DifficultyLevel = 3,
            Goal = "Strength",
            IsTemplate = true,
            ScheduledDays = "Mon,Wed,Fri"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.WorkoutId, Is.Not.EqualTo(Guid.Empty));
        Assert.That(result.Name, Is.EqualTo(command.Name));
        Assert.That(result.Description, Is.EqualTo(command.Description));
        Assert.That(result.TargetDurationMinutes, Is.EqualTo(command.TargetDurationMinutes));
        Assert.That(result.DifficultyLevel, Is.EqualTo(command.DifficultyLevel));
        Assert.That(result.IsActive, Is.True);

        var workout = await _context.Workouts.FindAsync(result.WorkoutId);
        Assert.That(workout, Is.Not.Null);
        Assert.That(workout!.Name, Is.EqualTo(command.Name));
    }
}

public class WorkoutPlanBuilderContext : DbContext, IWorkoutPlanBuilderContext
{
    public WorkoutPlanBuilderContext(DbContextOptions<WorkoutPlanBuilderContext> options)
        : base(options)
    {
    }

    public DbSet<Workout> Workouts { get; set; } = null!;
    public DbSet<Exercise> Exercises { get; set; } = null!;
    public DbSet<ProgressRecord> ProgressRecords { get; set; } = null!;
}
