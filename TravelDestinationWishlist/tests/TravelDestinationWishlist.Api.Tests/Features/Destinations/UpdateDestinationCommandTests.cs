using TravelDestinationWishlist.Api.Features.Destinations;
using Microsoft.Extensions.Logging;
using Moq;

namespace TravelDestinationWishlist.Api.Tests.Features.Destinations;

[TestFixture]
public class UpdateDestinationCommandTests
{
    private Mock<ITravelDestinationWishlistContext> _contextMock = null!;
    private Mock<ILogger<UpdateDestinationCommandHandler>> _loggerMock = null!;
    private UpdateDestinationCommandHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<ITravelDestinationWishlistContext>();
        _loggerMock = new Mock<ILogger<UpdateDestinationCommandHandler>>();
        _handler = new UpdateDestinationCommandHandler(_contextMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_ExistingDestination_UpdatesDestination()
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

        var command = new UpdateDestinationCommand
        {
            DestinationId = destinationId,
            Name = "Paris Updated",
            Country = "France",
            DestinationType = DestinationType.City,
            Description = "Updated description",
            Priority = 5,
            Notes = "New notes",
        };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Name, Is.EqualTo("Paris Updated"));
        Assert.That(result.Priority, Is.EqualTo(5));
        Assert.That(result.Description, Is.EqualTo("Updated description"));
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Handle_NonExistingDestination_ReturnsNull()
    {
        // Arrange
        var destinations = new List<Destination>();
        var mockDbSet = TestHelpers.CreateMockDbSet(destinations);
        _contextMock.Setup(c => c.Destinations).Returns(mockDbSet.Object);

        var command = new UpdateDestinationCommand
        {
            DestinationId = Guid.NewGuid(),
            Name = "Paris",
            Country = "France",
            DestinationType = DestinationType.City,
            Priority = 5,
        };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Null);
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
