using TravelDestinationWishlist.Api.Features.Destinations;
using Microsoft.Extensions.Logging;
using Moq;

namespace TravelDestinationWishlist.Api.Tests.Features.Destinations;

[TestFixture]
public class CreateDestinationCommandTests
{
    private Mock<ITravelDestinationWishlistContext> _contextMock = null!;
    private Mock<ILogger<CreateDestinationCommandHandler>> _loggerMock = null!;
    private CreateDestinationCommandHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<ITravelDestinationWishlistContext>();
        _loggerMock = new Mock<ILogger<CreateDestinationCommandHandler>>();
        _handler = new CreateDestinationCommandHandler(_contextMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_ValidCommand_CreatesDestination()
    {
        // Arrange
        var command = new CreateDestinationCommand
        {
            UserId = Guid.NewGuid(),
            Name = "Paris",
            Country = "France",
            DestinationType = DestinationType.City,
            Description = "City of lights",
            Priority = 5,
            Notes = "Must visit the Eiffel Tower",
        };

        var destinations = new List<Destination>();
        var mockDbSet = TestHelpers.CreateMockDbSet(destinations);
        _contextMock.Setup(c => c.Destinations).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(command.Name));
        Assert.That(result.Country, Is.EqualTo(command.Country));
        Assert.That(result.DestinationType, Is.EqualTo(command.DestinationType));
        Assert.That(result.Priority, Is.EqualTo(command.Priority));
        Assert.That(result.IsVisited, Is.False);
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Handle_WithDescription_IncludesDescriptionInDestination()
    {
        // Arrange
        var command = new CreateDestinationCommand
        {
            UserId = Guid.NewGuid(),
            Name = "Tokyo",
            Country = "Japan",
            DestinationType = DestinationType.City,
            Description = "Vibrant city with rich culture",
            Priority = 4,
        };

        var destinations = new List<Destination>();
        var mockDbSet = TestHelpers.CreateMockDbSet(destinations);
        _contextMock.Setup(c => c.Destinations).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.Description, Is.EqualTo(command.Description));
    }

    [Test]
    public async Task Handle_WithDefaultPriority_UsesDefaultValue()
    {
        // Arrange
        var command = new CreateDestinationCommand
        {
            UserId = Guid.NewGuid(),
            Name = "Bali",
            Country = "Indonesia",
            DestinationType = DestinationType.Beach,
            Priority = 3,
        };

        var destinations = new List<Destination>();
        var mockDbSet = TestHelpers.CreateMockDbSet(destinations);
        _contextMock.Setup(c => c.Destinations).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.Priority, Is.EqualTo(3));
    }
}
