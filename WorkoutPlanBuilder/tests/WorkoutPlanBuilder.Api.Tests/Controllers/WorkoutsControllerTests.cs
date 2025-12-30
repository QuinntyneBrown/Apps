// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WorkoutPlanBuilder.Api.Controllers;
using WorkoutPlanBuilder.Api.Features.Workouts;

namespace WorkoutPlanBuilder.Api.Tests.Controllers;

[TestFixture]
public class WorkoutsControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<WorkoutsController>> _loggerMock;
    private WorkoutsController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<WorkoutsController>>();
        _controller = new WorkoutsController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetAll_ReturnsOkWithWorkouts()
    {
        // Arrange
        var workouts = new List<WorkoutDto>
        {
            new WorkoutDto { WorkoutId = Guid.NewGuid(), Name = "Workout 1" },
            new WorkoutDto { WorkoutId = Guid.NewGuid(), Name = "Workout 2" }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllWorkoutsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(workouts);

        // Act
        var result = await _controller.GetAll(CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(workouts));
    }

    [Test]
    public async Task GetById_WithValidId_ReturnsOkWithWorkout()
    {
        // Arrange
        var workoutId = Guid.NewGuid();
        var workout = new WorkoutDto { WorkoutId = workoutId, Name = "Test Workout" };
        _mediatorMock.Setup(m => m.Send(It.Is<GetWorkoutByIdQuery>(q => q.WorkoutId == workoutId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(workout);

        // Act
        var result = await _controller.GetById(workoutId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(workout));
    }

    [Test]
    public async Task GetById_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var workoutId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<GetWorkoutByIdQuery>(q => q.WorkoutId == workoutId), It.IsAny<CancellationToken>()))
            .ReturnsAsync((WorkoutDto?)null);

        // Act
        var result = await _controller.GetById(workoutId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task Create_WithValidCommand_ReturnsCreatedAtAction()
    {
        // Arrange
        var command = new CreateWorkoutCommand { Name = "New Workout" };
        var createdWorkout = new WorkoutDto { WorkoutId = Guid.NewGuid(), Name = "New Workout" };
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdWorkout);

        // Act
        var result = await _controller.Create(command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult!.Value, Is.EqualTo(createdWorkout));
        Assert.That(createdResult.ActionName, Is.EqualTo(nameof(WorkoutsController.GetById)));
    }

    [Test]
    public async Task Update_WithValidCommand_ReturnsOkWithUpdatedWorkout()
    {
        // Arrange
        var workoutId = Guid.NewGuid();
        var command = new UpdateWorkoutCommand { WorkoutId = workoutId, Name = "Updated Workout" };
        var updatedWorkout = new WorkoutDto { WorkoutId = workoutId, Name = "Updated Workout" };
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedWorkout);

        // Act
        var result = await _controller.Update(workoutId, command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(updatedWorkout));
    }

    [Test]
    public async Task Update_WithMismatchedIds_ReturnsBadRequest()
    {
        // Arrange
        var routeId = Guid.NewGuid();
        var commandId = Guid.NewGuid();
        var command = new UpdateWorkoutCommand { WorkoutId = commandId, Name = "Updated Workout" };

        // Act
        var result = await _controller.Update(routeId, command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task Update_WithNonExistentWorkout_ReturnsNotFound()
    {
        // Arrange
        var workoutId = Guid.NewGuid();
        var command = new UpdateWorkoutCommand { WorkoutId = workoutId, Name = "Updated Workout" };
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync((WorkoutDto?)null);

        // Act
        var result = await _controller.Update(workoutId, command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task Delete_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var workoutId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<DeleteWorkoutCommand>(c => c.WorkoutId == workoutId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(workoutId, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task Delete_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var workoutId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<DeleteWorkoutCommand>(c => c.WorkoutId == workoutId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(workoutId, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
