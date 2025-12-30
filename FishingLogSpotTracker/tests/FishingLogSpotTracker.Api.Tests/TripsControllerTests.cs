// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FishingLogSpotTracker.Api.Controllers;
using FishingLogSpotTracker.Api.Features.Trips;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace FishingLogSpotTracker.Api.Tests;

/// <summary>
/// Tests for TripsController.
/// </summary>
[TestFixture]
public class TripsControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<TripsController>> _loggerMock;
    private TripsController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<TripsController>>();
        _controller = new TripsController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetTrips_ReturnsOkResultWithTrips()
    {
        // Arrange
        var trips = new List<TripDto>
        {
            new TripDto
            {
                TripId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                SpotId = Guid.NewGuid(),
                TripDate = DateTime.UtcNow,
                StartTime = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetTripsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(trips);

        // Act
        var result = await _controller.GetTrips(null, null);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(trips));
    }

    [Test]
    public async Task GetTrip_WithValidId_ReturnsTrip()
    {
        // Arrange
        var tripId = Guid.NewGuid();
        var trip = new TripDto
        {
            TripId = tripId,
            UserId = Guid.NewGuid(),
            SpotId = Guid.NewGuid(),
            TripDate = DateTime.UtcNow,
            StartTime = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetTripByIdQuery>(q => q.TripId == tripId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(trip);

        // Act
        var result = await _controller.GetTrip(tripId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(trip));
    }

    [Test]
    public async Task GetTrip_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var tripId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetTripByIdQuery>(q => q.TripId == tripId), It.IsAny<CancellationToken>()))
            .ReturnsAsync((TripDto?)null);

        // Act
        var result = await _controller.GetTrip(tripId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task CreateTrip_ReturnsCreatedAtAction()
    {
        // Arrange
        var command = new CreateTripCommand
        {
            UserId = Guid.NewGuid(),
            SpotId = Guid.NewGuid(),
            TripDate = DateTime.UtcNow,
            StartTime = DateTime.UtcNow
        };

        var createdTrip = new TripDto
        {
            TripId = Guid.NewGuid(),
            UserId = command.UserId,
            SpotId = command.SpotId,
            TripDate = command.TripDate,
            StartTime = command.StartTime,
            CreatedAt = DateTime.UtcNow
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<CreateTripCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdTrip);

        // Act
        var result = await _controller.CreateTrip(command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult!.Value, Is.EqualTo(createdTrip));
    }

    [Test]
    public async Task DeleteTrip_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var tripId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.Is<DeleteTripCommand>(c => c.TripId == tripId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteTrip(tripId);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task DeleteTrip_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var tripId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.Is<DeleteTripCommand>(c => c.TripId == tripId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteTrip(tripId);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
