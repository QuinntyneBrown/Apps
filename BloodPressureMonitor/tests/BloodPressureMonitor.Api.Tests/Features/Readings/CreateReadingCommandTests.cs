// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BloodPressureMonitor.Core;
using BloodPressureMonitor.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BloodPressureMonitor.Api.Tests;

/// <summary>
/// Unit tests for CreateReadingCommandHandler.
/// </summary>
[TestFixture]
public class CreateReadingCommandTests
{
    private BloodPressureMonitorContext _context = null!;
    private Mock<ILogger<CreateReadingCommandHandler>> _loggerMock = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<BloodPressureMonitorContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new BloodPressureMonitorContext(options);
        _loggerMock = new Mock<ILogger<CreateReadingCommandHandler>>();
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
    /// Tests that Handle creates a reading successfully.
    /// </summary>
    [Test]
    public async Task Handle_CreatesReading()
    {
        // Arrange
        var handler = new CreateReadingCommandHandler(_context, _loggerMock.Object);
        var command = new CreateReadingCommand
        {
            UserId = Guid.NewGuid(),
            Systolic = 120,
            Diastolic = 80,
            Pulse = 70,
            Position = "Sitting",
            Arm = "Left",
            Notes = "Morning reading",
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Systolic, Is.EqualTo(120));
        Assert.That(result.Diastolic, Is.EqualTo(80));
        Assert.That(result.Pulse, Is.EqualTo(70));
        Assert.That(result.Category, Is.EqualTo(BloodPressureCategory.Normal));

        var savedEntity = await _context.Readings.FindAsync(result.ReadingId);
        Assert.That(savedEntity, Is.Not.Null);
    }

    /// <summary>
    /// Tests that Handle determines category correctly.
    /// </summary>
    [Test]
    public async Task Handle_DeterminesCategoryCorrectly()
    {
        // Arrange
        var handler = new CreateReadingCommandHandler(_context, _loggerMock.Object);
        var command = new CreateReadingCommand
        {
            UserId = Guid.NewGuid(),
            Systolic = 145,
            Diastolic = 95,
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.Category, Is.EqualTo(BloodPressureCategory.HypertensionStage2));
    }

    /// <summary>
    /// Tests that Handle uses provided MeasuredAt timestamp.
    /// </summary>
    [Test]
    public async Task Handle_UsesProvidedMeasuredAt()
    {
        // Arrange
        var handler = new CreateReadingCommandHandler(_context, _loggerMock.Object);
        var measuredAt = new DateTime(2024, 1, 15, 10, 30, 0, DateTimeKind.Utc);
        var command = new CreateReadingCommand
        {
            UserId = Guid.NewGuid(),
            Systolic = 120,
            Diastolic = 80,
            MeasuredAt = measuredAt,
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.MeasuredAt, Is.EqualTo(measuredAt));
    }
}
