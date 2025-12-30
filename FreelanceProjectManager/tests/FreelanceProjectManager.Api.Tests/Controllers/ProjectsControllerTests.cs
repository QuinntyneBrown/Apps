// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FreelanceProjectManager.Api.Controllers;
using FreelanceProjectManager.Api.Features.Projects;
using FreelanceProjectManager.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace FreelanceProjectManager.Api.Tests.Controllers;

/// <summary>
/// Tests for the ProjectsController.
/// </summary>
[TestFixture]
public class ProjectsControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private ProjectsController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new ProjectsController(_mediatorMock.Object);
    }

    [Test]
    public async Task GetProjects_ReturnsOkResult_WithListOfProjects()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var projects = new List<ProjectDto>
        {
            new ProjectDto { ProjectId = Guid.NewGuid(), UserId = userId, Name = "Project 1", Status = ProjectStatus.InProgress },
            new ProjectDto { ProjectId = Guid.NewGuid(), UserId = userId, Name = "Project 2", Status = ProjectStatus.Planning }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetProjectsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(projects);

        // Act
        var result = await _controller.GetProjects(userId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(projects));
    }

    [Test]
    public async Task GetProject_ReturnsOkResult_WhenProjectExists()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var project = new ProjectDto { ProjectId = projectId, UserId = userId, Name = "Test Project", Status = ProjectStatus.InProgress };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetProjectByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(project);

        // Act
        var result = await _controller.GetProject(projectId, userId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(project));
    }

    [Test]
    public async Task GetProject_ReturnsNotFound_WhenProjectDoesNotExist()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetProjectByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((ProjectDto?)null);

        // Act
        var result = await _controller.GetProject(projectId, userId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task CreateProject_ReturnsCreatedResult_WithProject()
    {
        // Arrange
        var command = new CreateProjectCommand
        {
            UserId = Guid.NewGuid(),
            ClientId = Guid.NewGuid(),
            Name = "New Project",
            Description = "Test description",
            StartDate = DateTime.UtcNow
        };
        var createdProject = new ProjectDto
        {
            ProjectId = Guid.NewGuid(),
            UserId = command.UserId,
            ClientId = command.ClientId,
            Name = command.Name,
            Description = command.Description,
            Status = ProjectStatus.Planning
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<CreateProjectCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdProject);

        // Act
        var result = await _controller.CreateProject(command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult?.Value, Is.EqualTo(createdProject));
    }

    [Test]
    public async Task UpdateProject_ReturnsOkResult_WhenProjectExists()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var command = new UpdateProjectCommand
        {
            ProjectId = projectId,
            UserId = Guid.NewGuid(),
            ClientId = Guid.NewGuid(),
            Name = "Updated Project",
            Description = "Updated description",
            Status = ProjectStatus.InProgress,
            StartDate = DateTime.UtcNow
        };
        var updatedProject = new ProjectDto
        {
            ProjectId = projectId,
            UserId = command.UserId,
            ClientId = command.ClientId,
            Name = command.Name,
            Status = command.Status
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<UpdateProjectCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedProject);

        // Act
        var result = await _controller.UpdateProject(projectId, command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(updatedProject));
    }

    [Test]
    public async Task DeleteProject_ReturnsNoContent_WhenProjectExists()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<DeleteProjectCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteProject(projectId, userId, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }
}
