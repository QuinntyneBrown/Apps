// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DateNightIdeaGenerator.Api.Controllers;
using DateNightIdeaGenerator.Api.Features.DateIdeas;
using DateNightIdeaGenerator.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace DateNightIdeaGenerator.Api.Tests.Controllers;

/// <summary>
/// Tests for the DateIdeasController.
/// </summary>
[TestFixture]
public class DateIdeasControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<DateIdeasController>> _loggerMock;
    private DateIdeasController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<DateIdeasController>>();
        _controller = new DateIdeasController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetAll_ReturnsOkResultWithDateIdeas()
    {
        // Arrange
        var dateIdeas = new List<DateIdeaDto>
        {
            new DateIdeaDto { DateIdeaId = Guid.NewGuid(), Title = "Test Idea 1" },
            new DateIdeaDto { DateIdeaId = Guid.NewGuid(), Title = "Test Idea 2" }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetAllDateIdeasQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(dateIdeas);

        // Act
        var result = await _controller.GetAll();

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(dateIdeas));
    }

    [Test]
    public async Task GetById_WithValidId_ReturnsOkResultWithDateIdea()
    {
        // Arrange
        var dateIdeaId = Guid.NewGuid();
        var dateIdea = new DateIdeaDto { DateIdeaId = dateIdeaId, Title = "Test Idea" };

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetDateIdeaByIdQuery>(q => q.DateIdeaId == dateIdeaId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(dateIdea);

        // Act
        var result = await _controller.GetById(dateIdeaId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(dateIdea));
    }

    [Test]
    public async Task GetById_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var dateIdeaId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetDateIdeaByIdQuery>(q => q.DateIdeaId == dateIdeaId), It.IsAny<CancellationToken>()))
            .ReturnsAsync((DateIdeaDto?)null);

        // Act
        var result = await _controller.GetById(dateIdeaId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task Create_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var command = new CreateDateIdeaCommand
        {
            UserId = Guid.NewGuid(),
            Title = "New Idea",
            Description = "Description",
            Category = Category.Romantic,
            BudgetRange = BudgetRange.Medium
        };

        var createdDateIdea = new DateIdeaDto
        {
            DateIdeaId = Guid.NewGuid(),
            UserId = command.UserId,
            Title = command.Title,
            Description = command.Description
        };

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdDateIdea);

        // Act
        var result = await _controller.Create(command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult?.Value, Is.EqualTo(createdDateIdea));
        Assert.That(createdResult?.ActionName, Is.EqualTo(nameof(DateIdeasController.GetById)));
    }

    [Test]
    public async Task Update_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var dateIdeaId = Guid.NewGuid();
        var command = new UpdateDateIdeaCommand
        {
            DateIdeaId = dateIdeaId,
            Title = "Updated Idea",
            Description = "Updated Description",
            Category = Category.Romantic,
            BudgetRange = BudgetRange.High
        };

        var updatedDateIdea = new DateIdeaDto
        {
            DateIdeaId = dateIdeaId,
            Title = command.Title,
            Description = command.Description
        };

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedDateIdea);

        // Act
        var result = await _controller.Update(dateIdeaId, command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(updatedDateIdea));
    }

    [Test]
    public async Task Update_WithMismatchedId_ReturnsBadRequest()
    {
        // Arrange
        var dateIdeaId = Guid.NewGuid();
        var command = new UpdateDateIdeaCommand
        {
            DateIdeaId = Guid.NewGuid(), // Different ID
            Title = "Updated Idea"
        };

        // Act
        var result = await _controller.Update(dateIdeaId, command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task Delete_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var dateIdeaId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.Is<DeleteDateIdeaCommand>(c => c.DateIdeaId == dateIdeaId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(dateIdeaId);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task Delete_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var dateIdeaId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.Is<DeleteDateIdeaCommand>(c => c.DateIdeaId == dateIdeaId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(dateIdeaId);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
