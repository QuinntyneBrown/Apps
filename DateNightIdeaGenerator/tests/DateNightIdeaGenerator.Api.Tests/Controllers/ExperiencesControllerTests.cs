// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DateNightIdeaGenerator.Api.Controllers;
using DateNightIdeaGenerator.Api.Features.Experiences;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace DateNightIdeaGenerator.Api.Tests.Controllers;

/// <summary>
/// Tests for the ExperiencesController.
/// </summary>
[TestFixture]
public class ExperiencesControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<ExperiencesController>> _loggerMock;
    private ExperiencesController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<ExperiencesController>>();
        _controller = new ExperiencesController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetAll_ReturnsOkResultWithExperiences()
    {
        // Arrange
        var experiences = new List<ExperienceDto>
        {
            new ExperienceDto { ExperienceId = Guid.NewGuid(), Notes = "Great date!" },
            new ExperienceDto { ExperienceId = Guid.NewGuid(), Notes = "Fun experience" }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetAllExperiencesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(experiences);

        // Act
        var result = await _controller.GetAll();

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(experiences));
    }

    [Test]
    public async Task GetById_WithValidId_ReturnsOkResultWithExperience()
    {
        // Arrange
        var experienceId = Guid.NewGuid();
        var experience = new ExperienceDto { ExperienceId = experienceId, Notes = "Great date!" };

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetExperienceByIdQuery>(q => q.ExperienceId == experienceId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(experience);

        // Act
        var result = await _controller.GetById(experienceId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(experience));
    }

    [Test]
    public async Task GetById_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var experienceId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetExperienceByIdQuery>(q => q.ExperienceId == experienceId), It.IsAny<CancellationToken>()))
            .ReturnsAsync((ExperienceDto?)null);

        // Act
        var result = await _controller.GetById(experienceId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task Create_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var command = new CreateExperienceCommand
        {
            DateIdeaId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ExperienceDate = DateTime.UtcNow,
            Notes = "Amazing experience!"
        };

        var createdExperience = new ExperienceDto
        {
            ExperienceId = Guid.NewGuid(),
            DateIdeaId = command.DateIdeaId,
            UserId = command.UserId,
            Notes = command.Notes
        };

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdExperience);

        // Act
        var result = await _controller.Create(command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult?.Value, Is.EqualTo(createdExperience));
        Assert.That(createdResult?.ActionName, Is.EqualTo(nameof(ExperiencesController.GetById)));
    }

    [Test]
    public async Task Update_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var experienceId = Guid.NewGuid();
        var command = new UpdateExperienceCommand
        {
            ExperienceId = experienceId,
            ExperienceDate = DateTime.UtcNow,
            Notes = "Updated notes"
        };

        var updatedExperience = new ExperienceDto
        {
            ExperienceId = experienceId,
            Notes = command.Notes
        };

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedExperience);

        // Act
        var result = await _controller.Update(experienceId, command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(updatedExperience));
    }

    [Test]
    public async Task Delete_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var experienceId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.Is<DeleteExperienceCommand>(c => c.ExperienceId == experienceId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(experienceId);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task Delete_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var experienceId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.Is<DeleteExperienceCommand>(c => c.ExperienceId == experienceId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(experienceId);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
