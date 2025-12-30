using FinancialGoalTracker.Api.Controllers;
using FinancialGoalTracker.Api.Features.Goals;
using FinancialGoalTracker.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace FinancialGoalTracker.Api.Tests.Controllers;

[TestFixture]
public class GoalsControllerTests
{
    private Mock<IMediator> _mediatorMock = null!;
    private Mock<ILogger<GoalsController>> _loggerMock = null!;
    private GoalsController _controller = null!;

    [SetUp]
    public void SetUp()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<GoalsController>>();
        _controller = new GoalsController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetGoals_ReturnsOkWithGoals()
    {
        // Arrange
        var goals = new List<GoalDto>
        {
            new GoalDto
            {
                GoalId = Guid.NewGuid(),
                Name = "Emergency Fund",
                Description = "Save for emergencies",
                GoalType = GoalType.Emergency,
                TargetAmount = 10000,
                CurrentAmount = 5000,
                TargetDate = DateTime.UtcNow.AddMonths(6),
                Status = GoalStatus.InProgress,
                Progress = 50,
            }
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetGoalsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(goals);

        // Act
        var result = await _controller.GetGoals(null, null);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(goals));
    }

    [Test]
    public async Task GetGoalById_WithValidId_ReturnsOkWithGoal()
    {
        // Arrange
        var goalId = Guid.NewGuid();
        var goal = new GoalDto
        {
            GoalId = goalId,
            Name = "Emergency Fund",
            Description = "Save for emergencies",
            GoalType = GoalType.Emergency,
            TargetAmount = 10000,
            CurrentAmount = 5000,
            TargetDate = DateTime.UtcNow.AddMonths(6),
            Status = GoalStatus.InProgress,
            Progress = 50,
        };

        _mediatorMock.Setup(m => m.Send(It.Is<GetGoalByIdQuery>(q => q.GoalId == goalId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(goal);

        // Act
        var result = await _controller.GetGoalById(goalId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(goal));
    }

    [Test]
    public async Task GetGoalById_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var goalId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<GetGoalByIdQuery>(q => q.GoalId == goalId), It.IsAny<CancellationToken>()))
            .ReturnsAsync((GoalDto?)null);

        // Act
        var result = await _controller.GetGoalById(goalId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task CreateGoal_WithValidCommand_ReturnsCreatedResult()
    {
        // Arrange
        var command = new CreateGoalCommand
        {
            Name = "Emergency Fund",
            Description = "Save for emergencies",
            GoalType = GoalType.Emergency,
            TargetAmount = 10000,
            TargetDate = DateTime.UtcNow.AddMonths(6),
        };

        var createdGoal = new GoalDto
        {
            GoalId = Guid.NewGuid(),
            Name = command.Name,
            Description = command.Description,
            GoalType = command.GoalType,
            TargetAmount = command.TargetAmount,
            CurrentAmount = 0,
            TargetDate = command.TargetDate,
            Status = GoalStatus.NotStarted,
            Progress = 0,
        };

        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdGoal);

        // Act
        var result = await _controller.CreateGoal(command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedResult>());
        var createdResult = result.Result as CreatedResult;
        Assert.That(createdResult?.Value, Is.EqualTo(createdGoal));
        Assert.That(createdResult?.Location, Is.EqualTo($"/api/goals/{createdGoal.GoalId}"));
    }

    [Test]
    public async Task UpdateGoal_WithValidCommand_ReturnsOkWithUpdatedGoal()
    {
        // Arrange
        var goalId = Guid.NewGuid();
        var command = new UpdateGoalCommand
        {
            GoalId = goalId,
            Name = "Updated Goal",
            Description = "Updated description",
            GoalType = GoalType.Savings,
            TargetAmount = 15000,
            TargetDate = DateTime.UtcNow.AddMonths(12),
            Status = GoalStatus.InProgress,
        };

        var updatedGoal = new GoalDto
        {
            GoalId = goalId,
            Name = command.Name,
            Description = command.Description,
            GoalType = command.GoalType,
            TargetAmount = command.TargetAmount,
            CurrentAmount = 5000,
            TargetDate = command.TargetDate,
            Status = command.Status,
            Progress = 33.33m,
        };

        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedGoal);

        // Act
        var result = await _controller.UpdateGoal(goalId, command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(updatedGoal));
    }

    [Test]
    public async Task UpdateGoal_WithMismatchedId_ReturnsBadRequest()
    {
        // Arrange
        var goalId = Guid.NewGuid();
        var command = new UpdateGoalCommand
        {
            GoalId = Guid.NewGuid(), // Different ID
            Name = "Updated Goal",
            Description = "Updated description",
            GoalType = GoalType.Savings,
            TargetAmount = 15000,
            TargetDate = DateTime.UtcNow.AddMonths(12),
            Status = GoalStatus.InProgress,
        };

        // Act
        var result = await _controller.UpdateGoal(goalId, command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task DeleteGoal_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var goalId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<DeleteGoalCommand>(c => c.GoalId == goalId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteGoal(goalId);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task DeleteGoal_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var goalId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<DeleteGoalCommand>(c => c.GoalId == goalId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteGoal(goalId);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
