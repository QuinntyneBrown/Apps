// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using WorkoutPlanBuilder.Api.Features.ProgressRecords;
using WorkoutPlanBuilder.Core;

namespace WorkoutPlanBuilder.Api.Tests.Features;

[TestFixture]
public class DeleteProgressRecordCommandTests
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
    public async Task Handle_DeletesProgressRecordSuccessfully()
    {
        // Arrange
        var progressRecord = new ProgressRecord
        {
            ProgressRecordId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WorkoutId = Guid.NewGuid(),
            ActualDurationMinutes = 30
        };
        _context.ProgressRecords.Add(progressRecord);
        await _context.SaveChangesAsync();

        var handler = new DeleteProgressRecordCommandHandler(_context);
        var command = new DeleteProgressRecordCommand { ProgressRecordId = progressRecord.ProgressRecordId };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.True);
        var deletedRecord = await _context.ProgressRecords.FindAsync(progressRecord.ProgressRecordId);
        Assert.That(deletedRecord, Is.Null);
    }

    [Test]
    public async Task Handle_WithNonExistentRecord_ReturnsFalse()
    {
        // Arrange
        var handler = new DeleteProgressRecordCommandHandler(_context);
        var command = new DeleteProgressRecordCommand { ProgressRecordId = Guid.NewGuid() };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.False);
    }
}
