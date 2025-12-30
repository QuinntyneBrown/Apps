using TravelDestinationWishlist.Api.Features.Trips;
using Microsoft.Extensions.Logging;
using Moq;

namespace TravelDestinationWishlist.Api.Tests.Features.Trips;

[TestFixture]
public class CreateTripCommandTests
{
    private Mock<ITravelDestinationWishlistContext> _contextMock = null!;
    private Mock<ILogger<CreateTripCommandHandler>> _loggerMock = null!;
    private CreateTripCommandHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<ITravelDestinationWishlistContext>();
        _loggerMock = new Mock<ILogger<CreateTripCommandHandler>>();
        _handler = new CreateTripCommandHandler(_contextMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_ValidCommand_CreatesTrip()
    {
        // Arrange
        var command = new CreateTripCommand
        {
            UserId = Guid.NewGuid(),
            DestinationId = Guid.NewGuid(),
            StartDate = DateTime.UtcNow.AddDays(30),
            EndDate = DateTime.UtcNow.AddDays(37),
            TotalCost = 2500.00m,
            Accommodation = "Hotel Paris",
            Transportation = "Flight + Metro",
            Notes = "Week in Paris",
        };

        var trips = new List<Trip>();
        var mockDbSet = TestHelpers.CreateMockDbSet(trips);
        _contextMock.Setup(c => c.Trips).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.UserId, Is.EqualTo(command.UserId));
        Assert.That(result.DestinationId, Is.EqualTo(command.DestinationId));
        Assert.That(result.TotalCost, Is.EqualTo(command.TotalCost));
        Assert.That(result.Accommodation, Is.EqualTo(command.Accommodation));
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Handle_WithNullTotalCost_CreatesTrip()
    {
        // Arrange
        var command = new CreateTripCommand
        {
            UserId = Guid.NewGuid(),
            DestinationId = Guid.NewGuid(),
            StartDate = DateTime.UtcNow.AddDays(30),
            EndDate = DateTime.UtcNow.AddDays(37),
            TotalCost = null,
        };

        var trips = new List<Trip>();
        var mockDbSet = TestHelpers.CreateMockDbSet(trips);
        _contextMock.Setup(c => c.Trips).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.TotalCost, Is.Null);
    }
}
