// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Api.Controllers;
using ClassicCarRestorationLog.Api.Features.Projects;
using ClassicCarRestorationLog.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace ClassicCarRestorationLog.Api.Tests.Controllers;

[TestFixture]
public class ProjectsControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<ProjectsController>> _loggerMock;
    private ProjectsController _controller;

    [SetUp]
    public void SetUp()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<ProjectsController>>();
        _controller = new ProjectsController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetProjects_ReturnsOkResult_WithListOfProjects()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var projects = new List<ProjectDto>
        {
            new ProjectDto { ProjectId = Guid.NewGuid(), UserId = userId, CarMake = "Ford", CarModel = "Mustang" }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetProjects.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(projects);

        // Act
        var result = await _controller.GetProjects(userId);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(projects, okResult.Value);
    }

    [Test]
    public async Task GetProject_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var project = new ProjectDto { ProjectId = projectId, CarMake = "Ford", CarModel = "Mustang" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetProjectById.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(project);

        // Act
        var result = await _controller.GetProject(projectId);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(project, okResult.Value);
    }

    [Test]
    public async Task GetProject_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetProjectById.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((ProjectDto?)null);

        // Act
        var result = await _controller.GetProject(projectId);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result.Result);
    }

    [Test]
    public async Task CreateProject_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var command = new CreateProject.Command
        {
            UserId = Guid.NewGuid(),
            CarMake = "Ford",
            CarModel = "Mustang",
            Phase = ProjectPhase.Planning
        };
        var project = new ProjectDto { ProjectId = Guid.NewGuid(), CarMake = "Ford", CarModel = "Mustang" };
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(project);

        // Act
        var result = await _controller.CreateProject(command);

        // Assert
        Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.IsNotNull(createdResult);
        Assert.AreEqual(nameof(ProjectsController.GetProject), createdResult.ActionName);
        Assert.AreEqual(project, createdResult.Value);
    }

    [Test]
    public async Task UpdateProject_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var command = new UpdateProject.Command
        {
            ProjectId = projectId,
            CarMake = "Ford",
            CarModel = "Mustang",
            Phase = ProjectPhase.Planning,
            StartDate = DateTime.UtcNow
        };
        var project = new ProjectDto { ProjectId = projectId, CarMake = "Ford", CarModel = "Mustang" };
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(project);

        // Act
        var result = await _controller.UpdateProject(projectId, command);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(project, okResult.Value);
    }

    [Test]
    public async Task UpdateProject_WithMismatchedId_ReturnsBadRequest()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var command = new UpdateProject.Command
        {
            ProjectId = Guid.NewGuid(),
            CarMake = "Ford",
            CarModel = "Mustang",
            Phase = ProjectPhase.Planning,
            StartDate = DateTime.UtcNow
        };

        // Act
        var result = await _controller.UpdateProject(projectId, command);

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
    }

    [Test]
    public async Task DeleteProject_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteProject.Command>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteProject(projectId);

        // Assert
        Assert.IsInstanceOf<NoContentResult>(result);
    }

    [Test]
    public async Task DeleteProject_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteProject.Command>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteProject(projectId);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }
}
