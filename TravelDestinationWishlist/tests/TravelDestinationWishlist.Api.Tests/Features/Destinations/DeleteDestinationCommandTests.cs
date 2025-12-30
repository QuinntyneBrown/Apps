using TravelDestinationWishlist.Api.Features.Destinations;
using Microsoft.Extensions.Logging;
using Moq;

namespace TravelDestinationWishlist.Api.Tests.Features.Destinations;

[TestFixture]
public class DeleteDestinationCommandTests
{
    private Mock<ITravelDestinationWishlistContext> _contextMock = null!;
    private Mock<ILogger<DeleteDestinationCommandHandler>> _loggerMock = null!;
    private DeleteDestinationCommandHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<ITravelDestinationWishlistContext>();
        _loggerMock = new Mock<ILogger<DeleteDestinationCommandHandler>>();
        _handler = new DeleteDestinationCommandHandler(_contextMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_ExistingDestination_DeletesDestination()
    {
        // Arrange
        var destinationId = Guid.NewGuid();
        var destinations = new List<Destination>
        {
            new()
            {
                DestinationId = destinationId,
                UserId = Guid.NewGuid(),
                Name = "Paris",
                Country = "France",
                DestinationType = DestinationType.City,
                Priority = 3,
            }
        };

        var mockDbSet = TestHelpers.CreateMockDbSet(destinations);
        _contextMock.Setup(c => c.Destinations).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var command = new DeleteDestinationCommand { DestinationId = destinationId };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.True);
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Handle_NonExistingDestination_ReturnsFalse()
    {
        // Arrange
        var destinations = new List<Destination>();
        var mockDbSet = TestHelpers.CreateMockDbSet(destinations);
        _contextMock.Setup(c => c.Destinations).Returns(mockDbSet.Object);

        var command = new DeleteDestinationCommand { DestinationId = Guid.NewGuid() };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.False);
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
