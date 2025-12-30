// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using WorkoutPlanBuilder.Api.Features.ProgressRecords;
using WorkoutPlanBuilder.Core;

namespace WorkoutPlanBuilder.Api.Tests.Features;

[TestFixture]
public class CreateProgressRecordCommandTests
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
    public async Task Handle_CreatesProgressRecordSuccessfully()
    {
        // Arrange
        var handler = new CreateProgressRecordCommandHandler(_context);
        var command = new CreateProgressRecordCommand
        {
            UserId = Guid.NewGuid(),
            WorkoutId = Guid.NewGuid(),
            ActualDurationMinutes = 45,
            CaloriesBurned = 300,
            PerformanceRating = 4,
            Notes = "Great workout!"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.ProgressRecordId, Is.Not.EqualTo(Guid.Empty));
        Assert.That(result.WorkoutId, Is.EqualTo(command.WorkoutId));
        Assert.That(result.ActualDurationMinutes, Is.EqualTo(command.ActualDurationMinutes));
        Assert.That(result.CaloriesBurned, Is.EqualTo(command.CaloriesBurned));
        Assert.That(result.PerformanceRating, Is.EqualTo(command.PerformanceRating));

        var progressRecord = await _context.ProgressRecords.FindAsync(result.ProgressRecordId);
        Assert.That(progressRecord, Is.Not.Null);
        Assert.That(progressRecord!.Notes, Is.EqualTo(command.Notes));
    }

    [Test]
    public async Task Handle_WithNullCompletedAt_UsesCurrentTime()
    {
        // Arrange
        var handler = new CreateProgressRecordCommandHandler(_context);
        var command = new CreateProgressRecordCommand
        {
            UserId = Guid.NewGuid(),
            WorkoutId = Guid.NewGuid(),
            ActualDurationMinutes = 30,
            CompletedAt = null
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.CompletedAt, Is.Not.EqualTo(default(DateTime)));
        Assert.That(result.CompletedAt, Is.LessThanOrEqualTo(DateTime.UtcNow));
        Assert.That(result.CompletedAt, Is.GreaterThan(DateTime.UtcNow.AddMinutes(-1)));
    }
}
