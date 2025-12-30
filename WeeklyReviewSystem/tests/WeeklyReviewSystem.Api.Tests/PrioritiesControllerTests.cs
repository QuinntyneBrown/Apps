// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using WeeklyReviewSystem.Api.Controllers;
using WeeklyReviewSystem.Api.Features.Priorities;
using WeeklyReviewSystem.Core;

namespace WeeklyReviewSystem.Api.Tests;

[TestFixture]
public class PrioritiesControllerTests
{
    private Mock<IMediator> _mediatorMock = null!;
    private Mock<ILogger<PrioritiesController>> _loggerMock = null!;
    private PrioritiesController _controller = null!;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<PrioritiesController>>();
        _controller = new PrioritiesController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetAll_ReturnsOkResultWithPriorities()
    {
        // Arrange
        var priorities = new List<WeeklyPriorityDto>
        {
            new WeeklyPriorityDto { WeeklyPriorityId = Guid.NewGuid(), Title = "Priority 1", Level = PriorityLevel.High },
            new WeeklyPriorityDto { WeeklyPriorityId = Guid.NewGuid(), Title = "Priority 2", Level = PriorityLevel.Medium }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllWeeklyPrioritiesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(priorities);

        // Act
        var result = await _controller.GetAll(CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult!.Value, Is.EqualTo(priorities));
    }

    [Test]
    public async Task GetById_WithValidId_ReturnsOkResultWithPriority()
    {
        // Arrange
        var priorityId = Guid.NewGuid();
        var priority = new WeeklyPriorityDto { WeeklyPriorityId = priorityId, Title = "Test Priority", Level = PriorityLevel.Critical };
        _mediatorMock.Setup(m => m.Send(It.Is<GetWeeklyPriorityByIdQuery>(q => q.WeeklyPriorityId == priorityId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(priority);

        // Act
        var result = await _controller.GetById(priorityId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult!.Value, Is.EqualTo(priority));
    }

    [Test]
    public async Task GetById_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var priorityId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<GetWeeklyPriorityByIdQuery>(q => q.WeeklyPriorityId == priorityId), It.IsAny<CancellationToken>()))
            .ReturnsAsync((WeeklyPriorityDto?)null);

        // Act
        var result = await _controller.GetById(priorityId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task Create_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var command = new CreateWeeklyPriorityCommand
        {
            WeeklyReviewId = Guid.NewGuid(),
            Title = "New Priority",
            Level = PriorityLevel.High,
            TargetDate = DateTime.UtcNow.AddDays(7)
        };
        var priority = new WeeklyPriorityDto
        {
            WeeklyPriorityId = Guid.NewGuid(),
            WeeklyReviewId = command.WeeklyReviewId,
            Title = command.Title,
            Level = command.Level,
            TargetDate = command.TargetDate
        };
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(priority);

        // Act
        var result = await _controller.Create(command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult, Is.Not.Null);
        Assert.That(createdResult!.Value, Is.EqualTo(priority));
    }

    [Test]
    public async Task Update_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var priorityId = Guid.NewGuid();
        var command = new UpdateWeeklyPriorityCommand
        {
            WeeklyPriorityId = priorityId,
            WeeklyReviewId = Guid.NewGuid(),
            Title = "Updated Priority",
            Level = PriorityLevel.Critical,
            IsCompleted = true
        };
        var priority = new WeeklyPriorityDto
        {
            WeeklyPriorityId = priorityId,
            WeeklyReviewId = command.WeeklyReviewId,
            Title = command.Title,
            Level = command.Level,
            IsCompleted = command.IsCompleted
        };
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(priority);

        // Act
        var result = await _controller.Update(priorityId, command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult!.Value, Is.EqualTo(priority));
    }

    [Test]
    public async Task Update_WithMismatchedId_ReturnsBadRequest()
    {
        // Arrange
        var urlId = Guid.NewGuid();
        var commandId = Guid.NewGuid();
        var command = new UpdateWeeklyPriorityCommand { WeeklyPriorityId = commandId };

        // Act
        var result = await _controller.Update(urlId, command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task Delete_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var priorityId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<DeleteWeeklyPriorityCommand>(c => c.WeeklyPriorityId == priorityId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(priorityId, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task Delete_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var priorityId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<DeleteWeeklyPriorityCommand>(c => c.WeeklyPriorityId == priorityId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(priorityId, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
