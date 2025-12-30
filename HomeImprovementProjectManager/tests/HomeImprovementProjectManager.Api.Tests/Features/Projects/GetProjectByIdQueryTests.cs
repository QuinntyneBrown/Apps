// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeImprovementProjectManager.Core;
using HomeImprovementProjectManager.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace HomeImprovementProjectManager.Api.Tests;

/// <summary>
/// Unit tests for GetProjectByIdQueryHandler.
/// </summary>
[TestFixture]
public class GetProjectByIdQueryTests
{
    private HomeImprovementProjectManagerContext _context = null!;
    private Mock<ILogger<GetProjectByIdQueryHandler>> _loggerMock = null!;

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
        _loggerMock = new Mock<ILogger<GetProjectByIdQueryHandler>>();
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
    /// Tests that Handle returns project when found.
    /// </summary>
    [Test]
    public async Task Handle_ReturnsProject_WhenFound()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var project = new Project
        {
            ProjectId = projectId,
            UserId = Guid.NewGuid(),
            Name = "Deck Construction",
            Description = "Build a new deck",
            Status = ProjectStatus.Planning,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        var handler = new GetProjectByIdQueryHandler(_context, _loggerMock.Object);
        var query = new GetProjectByIdQuery { ProjectId = projectId };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.ProjectId, Is.EqualTo(projectId));
        Assert.That(result.Name, Is.EqualTo("Deck Construction"));
    }

    /// <summary>
    /// Tests that Handle returns null when project not found.
    /// </summary>
    [Test]
    public async Task Handle_ReturnsNull_WhenNotFound()
    {
        // Arrange
        var handler = new GetProjectByIdQueryHandler(_context, _loggerMock.Object);
        var query = new GetProjectByIdQuery { ProjectId = Guid.NewGuid() };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Null);
    }
}
