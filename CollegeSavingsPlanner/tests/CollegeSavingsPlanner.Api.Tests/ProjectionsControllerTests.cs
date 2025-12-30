// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CollegeSavingsPlanner.Api.Controllers;
using CollegeSavingsPlanner.Api.Features.Projections;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CollegeSavingsPlanner.Api.Tests;

/// <summary>
/// Tests for ProjectionsController.
/// </summary>
[TestFixture]
public class ProjectionsControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<ProjectionsController>> _loggerMock;
    private ProjectionsController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<ProjectionsController>>();
        _controller = new ProjectionsController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetProjections_ReturnsOkWithListOfProjections()
    {
        // Arrange
        var projections = new List<ProjectionDto>
        {
            new ProjectionDto { ProjectionId = Guid.NewGuid(), Name = "Projection 1" },
            new ProjectionDto { ProjectionId = Guid.NewGuid(), Name = "Projection 2" }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetProjectionsQuery>(), default))
            .ReturnsAsync(projections);

        // Act
        var result = await _controller.GetProjections();

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(projections));
    }

    [Test]
    public async Task GetProjection_WithValidId_ReturnsOkWithProjection()
    {
        // Arrange
        var projectionId = Guid.NewGuid();
        var projection = new ProjectionDto { ProjectionId = projectionId, Name = "Test Projection" };
        _mediatorMock.Setup(m => m.Send(It.Is<GetProjectionByIdQuery>(q => q.ProjectionId == projectionId), default))
            .ReturnsAsync(projection);

        // Act
        var result = await _controller.GetProjection(projectionId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(projection));
    }

    [Test]
    public async Task GetProjection_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var projectionId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<GetProjectionByIdQuery>(q => q.ProjectionId == projectionId), default))
            .ReturnsAsync((ProjectionDto?)null);

        // Act
        var result = await _controller.GetProjection(projectionId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task CreateProjection_ReturnsCreatedAtAction()
    {
        // Arrange
        var createProjectionDto = new CreateProjectionDto { Name = "New Projection" };
        var createdProjection = new ProjectionDto { ProjectionId = Guid.NewGuid(), Name = "New Projection" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateProjectionCommand>(), default))
            .ReturnsAsync(createdProjection);

        // Act
        var result = await _controller.CreateProjection(createProjectionDto);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult?.Value, Is.EqualTo(createdProjection));
    }

    [Test]
    public async Task UpdateProjection_WithValidId_ReturnsOkWithUpdatedProjection()
    {
        // Arrange
        var projectionId = Guid.NewGuid();
        var updateProjectionDto = new UpdateProjectionDto { Name = "Updated Projection" };
        var updatedProjection = new ProjectionDto { ProjectionId = projectionId, Name = "Updated Projection" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateProjectionCommand>(), default))
            .ReturnsAsync(updatedProjection);

        // Act
        var result = await _controller.UpdateProjection(projectionId, updateProjectionDto);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(updatedProjection));
    }

    [Test]
    public async Task DeleteProjection_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var projectionId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<DeleteProjectionCommand>(c => c.ProjectionId == projectionId), default))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteProjection(projectionId);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task DeleteProjection_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var projectionId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<DeleteProjectionCommand>(c => c.ProjectionId == projectionId), default))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteProjection(projectionId);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
