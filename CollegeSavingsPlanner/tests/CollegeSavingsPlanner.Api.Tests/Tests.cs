// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CollegeSavingsPlanner.Api.Controllers;
using CollegeSavingsPlanner.Api.Features.Plans;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CollegeSavingsPlanner.Api.Tests;

/// <summary>
/// Tests for PlansController.
/// </summary>
[TestFixture]
public class PlansControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<PlansController>> _loggerMock;
    private PlansController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<PlansController>>();
        _controller = new PlansController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetPlans_ReturnsOkWithListOfPlans()
    {
        // Arrange
        var plans = new List<PlanDto>
        {
            new PlanDto { PlanId = Guid.NewGuid(), Name = "Test Plan 1", State = "California" },
            new PlanDto { PlanId = Guid.NewGuid(), Name = "Test Plan 2", State = "New York" }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetPlansQuery>(), default))
            .ReturnsAsync(plans);

        // Act
        var result = await _controller.GetPlans();

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(plans));
    }

    [Test]
    public async Task GetPlan_WithValidId_ReturnsOkWithPlan()
    {
        // Arrange
        var planId = Guid.NewGuid();
        var plan = new PlanDto { PlanId = planId, Name = "Test Plan", State = "California" };
        _mediatorMock.Setup(m => m.Send(It.Is<GetPlanByIdQuery>(q => q.PlanId == planId), default))
            .ReturnsAsync(plan);

        // Act
        var result = await _controller.GetPlan(planId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(plan));
    }

    [Test]
    public async Task GetPlan_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var planId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<GetPlanByIdQuery>(q => q.PlanId == planId), default))
            .ReturnsAsync((PlanDto?)null);

        // Act
        var result = await _controller.GetPlan(planId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task CreatePlan_ReturnsCreatedAtAction()
    {
        // Arrange
        var createPlanDto = new CreatePlanDto { Name = "New Plan", State = "Texas" };
        var createdPlan = new PlanDto { PlanId = Guid.NewGuid(), Name = "New Plan", State = "Texas" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<CreatePlanCommand>(), default))
            .ReturnsAsync(createdPlan);

        // Act
        var result = await _controller.CreatePlan(createPlanDto);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult?.Value, Is.EqualTo(createdPlan));
    }

    [Test]
    public async Task UpdatePlan_WithValidId_ReturnsOkWithUpdatedPlan()
    {
        // Arrange
        var planId = Guid.NewGuid();
        var updatePlanDto = new UpdatePlanDto { Name = "Updated Plan", State = "Florida" };
        var updatedPlan = new PlanDto { PlanId = planId, Name = "Updated Plan", State = "Florida" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdatePlanCommand>(), default))
            .ReturnsAsync(updatedPlan);

        // Act
        var result = await _controller.UpdatePlan(planId, updatePlanDto);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(updatedPlan));
    }

    [Test]
    public async Task DeletePlan_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var planId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<DeletePlanCommand>(c => c.PlanId == planId), default))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeletePlan(planId);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task DeletePlan_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var planId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<DeletePlanCommand>(c => c.PlanId == planId), default))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeletePlan(planId);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
