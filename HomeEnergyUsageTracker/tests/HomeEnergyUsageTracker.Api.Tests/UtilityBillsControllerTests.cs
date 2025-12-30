// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeEnergyUsageTracker.Api.Controllers;
using HomeEnergyUsageTracker.Api.Features.UtilityBills;
using HomeEnergyUsageTracker.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace HomeEnergyUsageTracker.Api.Tests;

[TestFixture]
public class UtilityBillsControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<UtilityBillsController>> _loggerMock;
    private UtilityBillsController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<UtilityBillsController>>();
        _controller = new UtilityBillsController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetUtilityBills_ReturnsOkResult_WithListOfUtilityBills()
    {
        // Arrange
        var utilityBills = new List<UtilityBillDto>
        {
            new UtilityBillDto { UtilityBillId = Guid.NewGuid(), UserId = Guid.NewGuid(), UtilityType = UtilityType.Electricity, Amount = 100 }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetUtilityBillsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(utilityBills);

        // Act
        var result = await _controller.GetUtilityBills(null);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(utilityBills));
    }

    [Test]
    public async Task GetUtilityBillById_ReturnsOkResult_WithUtilityBill()
    {
        // Arrange
        var utilityBillId = Guid.NewGuid();
        var utilityBill = new UtilityBillDto { UtilityBillId = utilityBillId, UserId = Guid.NewGuid(), UtilityType = UtilityType.Gas, Amount = 75 };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetUtilityBillByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(utilityBill);

        // Act
        var result = await _controller.GetUtilityBillById(utilityBillId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(utilityBill));
    }

    [Test]
    public async Task GetUtilityBillById_ReturnsNotFound_WhenUtilityBillDoesNotExist()
    {
        // Arrange
        var utilityBillId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetUtilityBillByIdQuery>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new KeyNotFoundException());

        // Act
        var result = await _controller.GetUtilityBillById(utilityBillId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundObjectResult>());
    }

    [Test]
    public async Task CreateUtilityBill_ReturnsCreatedAtActionResult_WithUtilityBill()
    {
        // Arrange
        var command = new CreateUtilityBillCommand { UserId = Guid.NewGuid(), UtilityType = UtilityType.Water, Amount = 50 };
        var utilityBill = new UtilityBillDto { UtilityBillId = Guid.NewGuid(), UserId = command.UserId, UtilityType = command.UtilityType, Amount = command.Amount };
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(utilityBill);

        // Act
        var result = await _controller.CreateUtilityBill(command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult?.Value, Is.EqualTo(utilityBill));
    }

    [Test]
    public async Task UpdateUtilityBill_ReturnsOkResult_WithUpdatedUtilityBill()
    {
        // Arrange
        var utilityBillId = Guid.NewGuid();
        var command = new UpdateUtilityBillCommand { UtilityBillId = utilityBillId, UserId = Guid.NewGuid(), UtilityType = UtilityType.Internet, Amount = 60 };
        var utilityBill = new UtilityBillDto { UtilityBillId = utilityBillId, UserId = command.UserId, UtilityType = command.UtilityType, Amount = command.Amount };
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(utilityBill);

        // Act
        var result = await _controller.UpdateUtilityBill(utilityBillId, command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(utilityBill));
    }

    [Test]
    public async Task UpdateUtilityBill_ReturnsBadRequest_WhenIdMismatch()
    {
        // Arrange
        var utilityBillId = Guid.NewGuid();
        var command = new UpdateUtilityBillCommand { UtilityBillId = Guid.NewGuid(), UserId = Guid.NewGuid(), UtilityType = UtilityType.Other, Amount = 40 };

        // Act
        var result = await _controller.UpdateUtilityBill(utilityBillId, command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task DeleteUtilityBill_ReturnsNoContent_WhenSuccessful()
    {
        // Arrange
        var utilityBillId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteUtilityBillCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Unit.Value);

        // Act
        var result = await _controller.DeleteUtilityBill(utilityBillId);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task DeleteUtilityBill_ReturnsNotFound_WhenUtilityBillDoesNotExist()
    {
        // Arrange
        var utilityBillId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteUtilityBillCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new KeyNotFoundException());

        // Act
        var result = await _controller.DeleteUtilityBill(utilityBillId);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
    }
}
