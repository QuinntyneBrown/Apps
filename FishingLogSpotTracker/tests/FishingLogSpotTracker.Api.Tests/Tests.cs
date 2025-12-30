// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FishingLogSpotTracker.Api.Controllers;
using FishingLogSpotTracker.Api.Features.Spots;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using FishingLogSpotTracker.Core;

namespace FishingLogSpotTracker.Api.Tests;

/// <summary>
/// Tests for SpotsController.
/// </summary>
[TestFixture]
public class SpotsControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<SpotsController>> _loggerMock;
    private SpotsController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<SpotsController>>();
        _controller = new SpotsController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetSpots_ReturnsOkResultWithSpots()
    {
        // Arrange
        var spots = new List<SpotDto>
        {
            new SpotDto
            {
                SpotId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Name = "Test Spot",
                LocationType = LocationType.Lake,
                IsFavorite = false,
                CreatedAt = DateTime.UtcNow
            }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetSpotsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(spots);

        // Act
        var result = await _controller.GetSpots(null, null);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(spots));
    }

    [Test]
    public async Task GetSpot_WithValidId_ReturnsSpot()
    {
        // Arrange
        var spotId = Guid.NewGuid();
        var spot = new SpotDto
        {
            SpotId = spotId,
            UserId = Guid.NewGuid(),
            Name = "Test Spot",
            LocationType = LocationType.Lake,
            IsFavorite = false,
            CreatedAt = DateTime.UtcNow
        };

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetSpotByIdQuery>(q => q.SpotId == spotId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(spot);

        // Act
        var result = await _controller.GetSpot(spotId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(spot));
    }

    [Test]
    public async Task GetSpot_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var spotId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetSpotByIdQuery>(q => q.SpotId == spotId), It.IsAny<CancellationToken>()))
            .ReturnsAsync((SpotDto?)null);

        // Act
        var result = await _controller.GetSpot(spotId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task CreateSpot_ReturnsCreatedAtAction()
    {
        // Arrange
        var command = new CreateSpotCommand
        {
            UserId = Guid.NewGuid(),
            Name = "New Spot",
            LocationType = LocationType.Lake,
            IsFavorite = false
        };

        var createdSpot = new SpotDto
        {
            SpotId = Guid.NewGuid(),
            UserId = command.UserId,
            Name = command.Name,
            LocationType = command.LocationType,
            IsFavorite = command.IsFavorite,
            CreatedAt = DateTime.UtcNow
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<CreateSpotCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdSpot);

        // Act
        var result = await _controller.CreateSpot(command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult!.Value, Is.EqualTo(createdSpot));
    }

    [Test]
    public async Task UpdateSpot_WithValidId_ReturnsUpdatedSpot()
    {
        // Arrange
        var spotId = Guid.NewGuid();
        var command = new UpdateSpotCommand
        {
            SpotId = spotId,
            Name = "Updated Spot",
            LocationType = LocationType.River,
            IsFavorite = true
        };

        var updatedSpot = new SpotDto
        {
            SpotId = spotId,
            UserId = Guid.NewGuid(),
            Name = command.Name,
            LocationType = command.LocationType,
            IsFavorite = command.IsFavorite,
            CreatedAt = DateTime.UtcNow
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<UpdateSpotCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedSpot);

        // Act
        var result = await _controller.UpdateSpot(spotId, command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(updatedSpot));
    }

    [Test]
    public async Task UpdateSpot_WithMismatchedId_ReturnsBadRequest()
    {
        // Arrange
        var spotId = Guid.NewGuid();
        var command = new UpdateSpotCommand
        {
            SpotId = Guid.NewGuid(), // Different ID
            Name = "Updated Spot",
            LocationType = LocationType.River,
            IsFavorite = true
        };

        // Act
        var result = await _controller.UpdateSpot(spotId, command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task DeleteSpot_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var spotId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.Is<DeleteSpotCommand>(c => c.SpotId == spotId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteSpot(spotId);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task DeleteSpot_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var spotId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.Is<DeleteSpotCommand>(c => c.SpotId == spotId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteSpot(spotId);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
