using TravelDestinationWishlist.Api.Controllers;
using TravelDestinationWishlist.Api.Features.Destinations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace TravelDestinationWishlist.Api.Tests.Controllers;

[TestFixture]
public class DestinationsControllerTests
{
    private Mock<IMediator> _mediatorMock = null!;
    private Mock<ILogger<DestinationsController>> _loggerMock = null!;
    private DestinationsController _controller = null!;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<DestinationsController>>();
        _controller = new DestinationsController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetDestinations_ReturnsOkResult()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var destinations = new List<DestinationDto>
        {
            new() { DestinationId = Guid.NewGuid(), UserId = userId, Name = "Paris", Country = "France", DestinationType = DestinationType.City, Priority = 5, IsVisited = false, CreatedAt = DateTime.UtcNow },
            new() { DestinationId = Guid.NewGuid(), UserId = userId, Name = "Tokyo", Country = "Japan", DestinationType = DestinationType.City, Priority = 4, IsVisited = false, CreatedAt = DateTime.UtcNow },
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetDestinationsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(destinations);

        // Act
        var result = await _controller.GetDestinations(userId, null, null, null);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(destinations));
    }

    [Test]
    public async Task GetDestinationById_ExistingId_ReturnsOkResult()
    {
        // Arrange
        var destinationId = Guid.NewGuid();
        var destination = new DestinationDto
        {
            DestinationId = destinationId,
            UserId = Guid.NewGuid(),
            Name = "Paris",
            Country = "France",
            DestinationType = DestinationType.City,
            Priority = 5,
            IsVisited = false,
            CreatedAt = DateTime.UtcNow,
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetDestinationByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(destination);

        // Act
        var result = await _controller.GetDestinationById(destinationId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(destination));
    }

    [Test]
    public async Task GetDestinationById_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetDestinationByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((DestinationDto?)null);

        // Act
        var result = await _controller.GetDestinationById(Guid.NewGuid());

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task CreateDestination_ValidCommand_ReturnsCreatedResult()
    {
        // Arrange
        var command = new CreateDestinationCommand
        {
            UserId = Guid.NewGuid(),
            Name = "Paris",
            Country = "France",
            DestinationType = DestinationType.City,
            Priority = 5,
        };

        var createdDestination = new DestinationDto
        {
            DestinationId = Guid.NewGuid(),
            UserId = command.UserId,
            Name = command.Name,
            Country = command.Country,
            DestinationType = command.DestinationType,
            Priority = command.Priority,
            IsVisited = false,
            CreatedAt = DateTime.UtcNow,
        };

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdDestination);

        // Act
        var result = await _controller.CreateDestination(command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedResult>());
        var createdResult = result.Result as CreatedResult;
        Assert.That(createdResult!.Value, Is.EqualTo(createdDestination));
    }

    [Test]
    public async Task DeleteDestination_ExistingId_ReturnsNoContent()
    {
        // Arrange
        var destinationId = Guid.NewGuid();
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<DeleteDestinationCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteDestination(destinationId);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task DeleteDestination_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<DeleteDestinationCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteDestination(Guid.NewGuid());

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
