using FamilyVacationPlanner.Api.Controllers;
using FamilyVacationPlanner.Api.Features.Trips;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace FamilyVacationPlanner.Api.Tests.Controllers;

[TestFixture]
public class TripsControllerTests
{
    private Mock<IMediator> _mockMediator;
    private Mock<ILogger<TripsController>> _mockLogger;
    private TripsController _controller;

    [SetUp]
    public void Setup()
    {
        _mockMediator = new Mock<IMediator>();
        _mockLogger = new Mock<ILogger<TripsController>>();
        _controller = new TripsController(_mockMediator.Object, _mockLogger.Object);
    }

    [Test]
    public async Task GetTrips_ShouldReturnOk_WithListOfTrips()
    {
        // Arrange
        var trips = new List<TripDto>
        {
            new TripDto
            {
                TripId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Name = "Summer Vacation",
                Destination = "Hawaii",
                CreatedAt = DateTime.UtcNow
            }
        };

        _mockMediator.Setup(m => m.Send(It.IsAny<GetTripsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(trips);

        // Act
        var result = await _controller.GetTrips(null);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(trips));
    }

    [Test]
    public async Task GetTripById_ShouldReturnOk_WhenTripExists()
    {
        // Arrange
        var tripId = Guid.NewGuid();
        var trip = new TripDto
        {
            TripId = tripId,
            UserId = Guid.NewGuid(),
            Name = "Summer Vacation",
            Destination = "Hawaii",
            CreatedAt = DateTime.UtcNow
        };

        _mockMediator.Setup(m => m.Send(It.IsAny<GetTripByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(trip);

        // Act
        var result = await _controller.GetTripById(tripId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(trip));
    }

    [Test]
    public async Task GetTripById_ShouldReturnNotFound_WhenTripDoesNotExist()
    {
        // Arrange
        var tripId = Guid.NewGuid();

        _mockMediator.Setup(m => m.Send(It.IsAny<GetTripByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((TripDto?)null);

        // Act
        var result = await _controller.GetTripById(tripId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task CreateTrip_ShouldReturnCreated_WhenTripIsCreated()
    {
        // Arrange
        var command = new CreateTripCommand
        {
            UserId = Guid.NewGuid(),
            Name = "Summer Vacation",
            Destination = "Hawaii"
        };

        var trip = new TripDto
        {
            TripId = Guid.NewGuid(),
            UserId = command.UserId,
            Name = command.Name,
            Destination = command.Destination,
            CreatedAt = DateTime.UtcNow
        };

        _mockMediator.Setup(m => m.Send(It.IsAny<CreateTripCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(trip);

        // Act
        var result = await _controller.CreateTrip(command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedResult>());
        var createdResult = result.Result as CreatedResult;
        Assert.That(createdResult?.Value, Is.EqualTo(trip));
    }

    [Test]
    public async Task DeleteTrip_ShouldReturnNoContent_WhenTripIsDeleted()
    {
        // Arrange
        var tripId = Guid.NewGuid();

        _mockMediator.Setup(m => m.Send(It.IsAny<DeleteTripCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteTrip(tripId);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task DeleteTrip_ShouldReturnNotFound_WhenTripDoesNotExist()
    {
        // Arrange
        var tripId = Guid.NewGuid();

        _mockMediator.Setup(m => m.Send(It.IsAny<DeleteTripCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteTrip(tripId);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
