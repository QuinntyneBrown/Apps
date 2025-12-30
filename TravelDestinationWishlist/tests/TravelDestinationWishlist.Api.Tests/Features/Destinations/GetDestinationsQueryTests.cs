using TravelDestinationWishlist.Api.Features.Destinations;
using Microsoft.Extensions.Logging;
using Moq;

namespace TravelDestinationWishlist.Api.Tests.Features.Destinations;

[TestFixture]
public class GetDestinationsQueryTests
{
    private Mock<ITravelDestinationWishlistContext> _contextMock = null!;
    private Mock<ILogger<GetDestinationsQueryHandler>> _loggerMock = null!;
    private GetDestinationsQueryHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<ITravelDestinationWishlistContext>();
        _loggerMock = new Mock<ILogger<GetDestinationsQueryHandler>>();
        _handler = new GetDestinationsQueryHandler(_contextMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_WithUserId_ReturnsFilteredDestinations()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var otherUserId = Guid.NewGuid();

        var destinations = new List<Destination>
        {
            new() { DestinationId = Guid.NewGuid(), UserId = userId, Name = "Paris", Country = "France", Priority = 5, DestinationType = DestinationType.City },
            new() { DestinationId = Guid.NewGuid(), UserId = userId, Name = "Tokyo", Country = "Japan", Priority = 4, DestinationType = DestinationType.City },
            new() { DestinationId = Guid.NewGuid(), UserId = otherUserId, Name = "London", Country = "UK", Priority = 3, DestinationType = DestinationType.City },
        };

        var mockDbSet = TestHelpers.CreateMockDbSet(destinations);
        _contextMock.Setup(c => c.Destinations).Returns(mockDbSet.Object);

        var query = new GetDestinationsQuery { UserId = userId };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result.Count(), Is.EqualTo(2));
        Assert.That(result.All(d => d.UserId == userId), Is.True);
    }

    [Test]
    public async Task Handle_WithDestinationType_ReturnsFilteredDestinations()
    {
        // Arrange
        var userId = Guid.NewGuid();

        var destinations = new List<Destination>
        {
            new() { DestinationId = Guid.NewGuid(), UserId = userId, Name = "Paris", Country = "France", Priority = 5, DestinationType = DestinationType.City },
            new() { DestinationId = Guid.NewGuid(), UserId = userId, Name = "Bali", Country = "Indonesia", Priority = 4, DestinationType = DestinationType.Beach },
            new() { DestinationId = Guid.NewGuid(), UserId = userId, Name = "Swiss Alps", Country = "Switzerland", Priority = 3, DestinationType = DestinationType.Mountain },
        };

        var mockDbSet = TestHelpers.CreateMockDbSet(destinations);
        _contextMock.Setup(c => c.Destinations).Returns(mockDbSet.Object);

        var query = new GetDestinationsQuery { DestinationType = DestinationType.Beach };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.That(result.First().Name, Is.EqualTo("Bali"));
    }

    [Test]
    public async Task Handle_OrdersByPriorityDescending()
    {
        // Arrange
        var userId = Guid.NewGuid();

        var destinations = new List<Destination>
        {
            new() { DestinationId = Guid.NewGuid(), UserId = userId, Name = "Tokyo", Country = "Japan", Priority = 2, DestinationType = DestinationType.City },
            new() { DestinationId = Guid.NewGuid(), UserId = userId, Name = "Paris", Country = "France", Priority = 5, DestinationType = DestinationType.City },
            new() { DestinationId = Guid.NewGuid(), UserId = userId, Name = "Bali", Country = "Indonesia", Priority = 3, DestinationType = DestinationType.Beach },
        };

        var mockDbSet = TestHelpers.CreateMockDbSet(destinations);
        _contextMock.Setup(c => c.Destinations).Returns(mockDbSet.Object);

        var query = new GetDestinationsQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        var resultList = result.ToList();
        Assert.That(resultList[0].Priority, Is.EqualTo(5));
        Assert.That(resultList[1].Priority, Is.EqualTo(3));
        Assert.That(resultList[2].Priority, Is.EqualTo(2));
    }
}
