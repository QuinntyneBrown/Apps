// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeImprovementProjectManager.Core;
using HomeImprovementProjectManager.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace HomeImprovementProjectManager.Api.Tests;

/// <summary>
/// Unit tests for CreateProjectCommandHandler.
/// </summary>
[TestFixture]
public class CreateProjectCommandTests
{
    private HomeImprovementProjectManagerContext _context = null!;
    private Mock<ILogger<CreateProjectCommandHandler>> _loggerMock = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<HomeImprovementProjectManagerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new HomeImprovementProjectManagerContext(options);
        _loggerMock = new Mock<ILogger<CreateProjectCommandHandler>>();
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
    /// Tests that Handle creates a project successfully.
    /// </summary>
    [Test]
    public async Task Handle_CreatesProject()
    {
        // Arrange
        var handler = new CreateProjectCommandHandler(_context, _loggerMock.Object);
        var command = new CreateProjectCommand
        {
            UserId = Guid.NewGuid(),
            Name = "Kitchen Renovation",
            Description = "Complete kitchen remodel",
            Status = ProjectStatus.Planning,
            StartDate = DateTime.UtcNow.AddDays(7),
            EstimatedCost = 15000m,
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo("Kitchen Renovation"));
        Assert.That(result.Status, Is.EqualTo(ProjectStatus.Planning));
        Assert.That(result.EstimatedCost, Is.EqualTo(15000m));

        var savedEntity = await _context.Projects.FindAsync(result.ProjectId);
        Assert.That(savedEntity, Is.Not.Null);
    }

    /// <summary>
    /// Tests that Handle sets CreatedAt timestamp.
    /// </summary>
    [Test]
    public async Task Handle_SetsCreatedAtTimestamp()
    {
        // Arrange
        var handler = new CreateProjectCommandHandler(_context, _loggerMock.Object);
        var command = new CreateProjectCommand
        {
            UserId = Guid.NewGuid(),
            Name = "Bathroom Remodel",
            Status = ProjectStatus.Planning,
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.CreatedAt, Is.GreaterThan(DateTime.UtcNow.AddMinutes(-1)));
        Assert.That(result.CreatedAt, Is.LessThanOrEqualTo(DateTime.UtcNow));
    }
}
