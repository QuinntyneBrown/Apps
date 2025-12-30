// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FishingLogSpotTracker.Api.Controllers;
using FishingLogSpotTracker.Api.Features.Catches;
using FishingLogSpotTracker.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace FishingLogSpotTracker.Api.Tests;

/// <summary>
/// Tests for CatchesController.
/// </summary>
[TestFixture]
public class CatchesControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<CatchesController>> _loggerMock;
    private CatchesController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<CatchesController>>();
        _controller = new CatchesController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetCatches_ReturnsOkResultWithCatches()
    {
        // Arrange
        var catches = new List<CatchDto>
        {
            new CatchDto
            {
                CatchId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                TripId = Guid.NewGuid(),
                Species = FishSpecies.Bass,
                CatchTime = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetCatchesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(catches);

        // Act
        var result = await _controller.GetCatches(null, null, null);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(catches));
    }

    [Test]
    public async Task GetCatch_WithValidId_ReturnsCatch()
    {
        // Arrange
        var catchId = Guid.NewGuid();
        var catchDto = new CatchDto
        {
            CatchId = catchId,
            UserId = Guid.NewGuid(),
            TripId = Guid.NewGuid(),
            Species = FishSpecies.Bass,
            CatchTime = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetCatchByIdQuery>(q => q.CatchId == catchId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(catchDto);

        // Act
        var result = await _controller.GetCatch(catchId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(catchDto));
    }

    [Test]
    public async Task GetCatch_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var catchId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetCatchByIdQuery>(q => q.CatchId == catchId), It.IsAny<CancellationToken>()))
            .ReturnsAsync((CatchDto?)null);

        // Act
        var result = await _controller.GetCatch(catchId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task CreateCatch_ReturnsCreatedAtAction()
    {
        // Arrange
        var command = new CreateCatchCommand
        {
            UserId = Guid.NewGuid(),
            TripId = Guid.NewGuid(),
            Species = FishSpecies.Bass,
            CatchTime = DateTime.UtcNow,
            WasReleased = false
        };

        var createdCatch = new CatchDto
        {
            CatchId = Guid.NewGuid(),
            UserId = command.UserId,
            TripId = command.TripId,
            Species = command.Species,
            CatchTime = command.CatchTime,
            WasReleased = command.WasReleased,
            CreatedAt = DateTime.UtcNow
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<CreateCatchCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdCatch);

        // Act
        var result = await _controller.CreateCatch(command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult!.Value, Is.EqualTo(createdCatch));
    }

    [Test]
    public async Task DeleteCatch_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var catchId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.Is<DeleteCatchCommand>(c => c.CatchId == catchId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteCatch(catchId);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task DeleteCatch_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var catchId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.Is<DeleteCatchCommand>(c => c.CatchId == catchId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteCatch(catchId);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
