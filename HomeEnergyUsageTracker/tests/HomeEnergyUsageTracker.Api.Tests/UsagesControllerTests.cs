// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeEnergyUsageTracker.Api.Controllers;
using HomeEnergyUsageTracker.Api.Features.Usages;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace HomeEnergyUsageTracker.Api.Tests;

[TestFixture]
public class UsagesControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<UsagesController>> _loggerMock;
    private UsagesController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<UsagesController>>();
        _controller = new UsagesController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetUsages_ReturnsOkResult_WithListOfUsages()
    {
        // Arrange
        var usages = new List<UsageDto>
        {
            new UsageDto { UsageId = Guid.NewGuid(), UtilityBillId = Guid.NewGuid(), Date = DateTime.UtcNow, Amount = 100 }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetUsagesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(usages);

        // Act
        var result = await _controller.GetUsages(null);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(usages));
    }

    [Test]
    public async Task GetUsageById_ReturnsOkResult_WithUsage()
    {
        // Arrange
        var usageId = Guid.NewGuid();
        var usage = new UsageDto { UsageId = usageId, UtilityBillId = Guid.NewGuid(), Date = DateTime.UtcNow, Amount = 75 };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetUsageByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(usage);

        // Act
        var result = await _controller.GetUsageById(usageId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(usage));
    }

    [Test]
    public async Task GetUsageById_ReturnsNotFound_WhenUsageDoesNotExist()
    {
        // Arrange
        var usageId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetUsageByIdQuery>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new KeyNotFoundException());

        // Act
        var result = await _controller.GetUsageById(usageId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundObjectResult>());
    }

    [Test]
    public async Task CreateUsage_ReturnsCreatedAtActionResult_WithUsage()
    {
        // Arrange
        var command = new CreateUsageCommand { UtilityBillId = Guid.NewGuid(), Date = DateTime.UtcNow, Amount = 50 };
        var usage = new UsageDto { UsageId = Guid.NewGuid(), UtilityBillId = command.UtilityBillId, Date = command.Date, Amount = command.Amount };
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(usage);

        // Act
        var result = await _controller.CreateUsage(command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult?.Value, Is.EqualTo(usage));
    }

    [Test]
    public async Task UpdateUsage_ReturnsOkResult_WithUpdatedUsage()
    {
        // Arrange
        var usageId = Guid.NewGuid();
        var command = new UpdateUsageCommand { UsageId = usageId, UtilityBillId = Guid.NewGuid(), Date = DateTime.UtcNow, Amount = 60 };
        var usage = new UsageDto { UsageId = usageId, UtilityBillId = command.UtilityBillId, Date = command.Date, Amount = command.Amount };
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(usage);

        // Act
        var result = await _controller.UpdateUsage(usageId, command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(usage));
    }

    [Test]
    public async Task UpdateUsage_ReturnsBadRequest_WhenIdMismatch()
    {
        // Arrange
        var usageId = Guid.NewGuid();
        var command = new UpdateUsageCommand { UsageId = Guid.NewGuid(), UtilityBillId = Guid.NewGuid(), Date = DateTime.UtcNow, Amount = 40 };

        // Act
        var result = await _controller.UpdateUsage(usageId, command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task DeleteUsage_ReturnsNoContent_WhenSuccessful()
    {
        // Arrange
        var usageId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteUsageCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Unit.Value);

        // Act
        var result = await _controller.DeleteUsage(usageId);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task DeleteUsage_ReturnsNotFound_WhenUsageDoesNotExist()
    {
        // Arrange
        var usageId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteUsageCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new KeyNotFoundException());

        // Act
        var result = await _controller.DeleteUsage(usageId);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
    }
}
