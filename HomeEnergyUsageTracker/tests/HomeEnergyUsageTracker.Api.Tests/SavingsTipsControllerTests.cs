// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeEnergyUsageTracker.Api.Controllers;
using HomeEnergyUsageTracker.Api.Features.SavingsTips;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace HomeEnergyUsageTracker.Api.Tests;

[TestFixture]
public class SavingsTipsControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<SavingsTipsController>> _loggerMock;
    private SavingsTipsController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<SavingsTipsController>>();
        _controller = new SavingsTipsController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetSavingsTips_ReturnsOkResult_WithListOfSavingsTips()
    {
        // Arrange
        var savingsTips = new List<SavingsTipDto>
        {
            new SavingsTipDto { SavingsTipId = Guid.NewGuid(), Title = "Save Energy", Description = "Turn off lights" }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetSavingsTipsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(savingsTips);

        // Act
        var result = await _controller.GetSavingsTips();

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(savingsTips));
    }

    [Test]
    public async Task GetSavingsTipById_ReturnsOkResult_WithSavingsTip()
    {
        // Arrange
        var savingsTipId = Guid.NewGuid();
        var savingsTip = new SavingsTipDto { SavingsTipId = savingsTipId, Title = "Use LED Bulbs", Description = "Replace incandescent bulbs" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetSavingsTipByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(savingsTip);

        // Act
        var result = await _controller.GetSavingsTipById(savingsTipId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(savingsTip));
    }

    [Test]
    public async Task GetSavingsTipById_ReturnsNotFound_WhenSavingsTipDoesNotExist()
    {
        // Arrange
        var savingsTipId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetSavingsTipByIdQuery>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new KeyNotFoundException());

        // Act
        var result = await _controller.GetSavingsTipById(savingsTipId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundObjectResult>());
    }

    [Test]
    public async Task CreateSavingsTip_ReturnsCreatedAtActionResult_WithSavingsTip()
    {
        // Arrange
        var command = new CreateSavingsTipCommand { Title = "Unplug Devices", Description = "Unplug when not in use" };
        var savingsTip = new SavingsTipDto { SavingsTipId = Guid.NewGuid(), Title = command.Title, Description = command.Description };
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(savingsTip);

        // Act
        var result = await _controller.CreateSavingsTip(command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult?.Value, Is.EqualTo(savingsTip));
    }

    [Test]
    public async Task UpdateSavingsTip_ReturnsOkResult_WithUpdatedSavingsTip()
    {
        // Arrange
        var savingsTipId = Guid.NewGuid();
        var command = new UpdateSavingsTipCommand { SavingsTipId = savingsTipId, Title = "Use Smart Thermostat", Description = "Automate temperature control" };
        var savingsTip = new SavingsTipDto { SavingsTipId = savingsTipId, Title = command.Title, Description = command.Description };
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(savingsTip);

        // Act
        var result = await _controller.UpdateSavingsTip(savingsTipId, command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(savingsTip));
    }

    [Test]
    public async Task UpdateSavingsTip_ReturnsBadRequest_WhenIdMismatch()
    {
        // Arrange
        var savingsTipId = Guid.NewGuid();
        var command = new UpdateSavingsTipCommand { SavingsTipId = Guid.NewGuid(), Title = "Test", Description = "Test" };

        // Act
        var result = await _controller.UpdateSavingsTip(savingsTipId, command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task DeleteSavingsTip_ReturnsNoContent_WhenSuccessful()
    {
        // Arrange
        var savingsTipId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteSavingsTipCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Unit.Value);

        // Act
        var result = await _controller.DeleteSavingsTip(savingsTipId);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task DeleteSavingsTip_ReturnsNotFound_WhenSavingsTipDoesNotExist()
    {
        // Arrange
        var savingsTipId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteSavingsTipCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new KeyNotFoundException());

        // Act
        var result = await _controller.DeleteSavingsTip(savingsTipId);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
    }
}
