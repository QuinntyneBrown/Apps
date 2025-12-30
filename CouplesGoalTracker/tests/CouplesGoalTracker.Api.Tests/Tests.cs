// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CouplesGoalTracker.Api.Controllers;
using CouplesGoalTracker.Api.Features.Goals;
using CouplesGoalTracker.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CouplesGoalTracker.Api.Tests;

/// <summary>
/// Tests for GoalsController.
/// </summary>
public class GoalsControllerTests
{
    private Mock<IMediator> _mediatorMock = null!;
    private Mock<ILogger<GoalsController>> _loggerMock = null!;
    private GoalsController _controller = null!;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<GoalsController>>();
        _controller = new GoalsController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetAll_ReturnsOkResultWithGoals()
    {
        // Arrange
        var expectedGoals = new List<GoalDto>
        {
            new GoalDto
            {
                GoalId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Title = "Test Goal",
                Description = "Test Description",
                Category = GoalCategory.Communication,
                Status = GoalStatus.InProgress,
                Priority = 3,
                IsShared = true,
                CreatedAt = DateTime.UtcNow,
            },
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetAllGoalsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedGoals);

        // Act
        var result = await _controller.GetAll(null, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult!.Value, Is.EqualTo(expectedGoals));
    }

    [Test]
    public async Task GetById_ExistingGoal_ReturnsOkResultWithGoal()
    {
        // Arrange
        var goalId = Guid.NewGuid();
        var expectedGoal = new GoalDto
        {
            GoalId = goalId,
            UserId = Guid.NewGuid(),
            Title = "Test Goal",
            Description = "Test Description",
            Category = GoalCategory.Communication,
            Status = GoalStatus.InProgress,
            Priority = 3,
            IsShared = true,
            CreatedAt = DateTime.UtcNow,
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetGoalByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedGoal);

        // Act
        var result = await _controller.GetById(goalId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult!.Value, Is.EqualTo(expectedGoal));
    }

    [Test]
    public async Task GetById_NonExistingGoal_ReturnsNotFound()
    {
        // Arrange
        var goalId = Guid.NewGuid();
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetGoalByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((GoalDto?)null);

        // Act
        var result = await _controller.GetById(goalId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task Create_ValidCommand_ReturnsCreatedAtAction()
    {
        // Arrange
        var command = new CreateGoalCommand
        {
            UserId = Guid.NewGuid(),
            Title = "New Goal",
            Description = "New Description",
            Category = GoalCategory.Financial,
            Priority = 4,
            IsShared = true,
        };

        var createdGoal = new GoalDto
        {
            GoalId = Guid.NewGuid(),
            UserId = command.UserId,
            Title = command.Title,
            Description = command.Description,
            Category = command.Category,
            Status = GoalStatus.NotStarted,
            Priority = command.Priority,
            IsShared = command.IsShared,
            CreatedAt = DateTime.UtcNow,
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<CreateGoalCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdGoal);

        // Act
        var result = await _controller.Create(command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult, Is.Not.Null);
        Assert.That(createdResult!.Value, Is.EqualTo(createdGoal));
    }

    [Test]
    public async Task Update_ExistingGoal_ReturnsOkResult()
    {
        // Arrange
        var goalId = Guid.NewGuid();
        var command = new UpdateGoalCommand
        {
            GoalId = goalId,
            Title = "Updated Goal",
            Description = "Updated Description",
            Category = GoalCategory.HealthAndWellness,
            Status = GoalStatus.Completed,
            Priority = 5,
            IsShared = true,
        };

        var updatedGoal = new GoalDto
        {
            GoalId = goalId,
            Title = command.Title,
            Description = command.Description,
            Category = command.Category,
            Status = command.Status,
            Priority = command.Priority,
            IsShared = command.IsShared,
            UpdatedAt = DateTime.UtcNow,
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<UpdateGoalCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedGoal);

        // Act
        var result = await _controller.Update(goalId, command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult!.Value, Is.EqualTo(updatedGoal));
    }

    [Test]
    public async Task Update_NonExistingGoal_ReturnsNotFound()
    {
        // Arrange
        var goalId = Guid.NewGuid();
        var command = new UpdateGoalCommand
        {
            GoalId = goalId,
            Title = "Updated Goal",
            Description = "Updated Description",
            Category = GoalCategory.HealthAndWellness,
            Status = GoalStatus.Completed,
            Priority = 5,
            IsShared = true,
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<UpdateGoalCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((GoalDto?)null);

        // Act
        var result = await _controller.Update(goalId, command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task Delete_ExistingGoal_ReturnsNoContent()
    {
        // Arrange
        var goalId = Guid.NewGuid();
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<DeleteGoalCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(goalId, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task Delete_NonExistingGoal_ReturnsNotFound()
    {
        // Arrange
        var goalId = Guid.NewGuid();
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<DeleteGoalCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(goalId, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
