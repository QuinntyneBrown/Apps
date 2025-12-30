using TravelDestinationWishlist.Api.Features.Memories;
using Microsoft.Extensions.Logging;
using Moq;

namespace TravelDestinationWishlist.Api.Tests.Features.Memories;

[TestFixture]
public class CreateMemoryCommandTests
{
    private Mock<ITravelDestinationWishlistContext> _contextMock = null!;
    private Mock<ILogger<CreateMemoryCommandHandler>> _loggerMock = null!;
    private CreateMemoryCommandHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<ITravelDestinationWishlistContext>();
        _loggerMock = new Mock<ILogger<CreateMemoryCommandHandler>>();
        _handler = new CreateMemoryCommandHandler(_contextMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_ValidCommand_CreatesMemory()
    {
        // Arrange
        var command = new CreateMemoryCommand
        {
            UserId = Guid.NewGuid(),
            TripId = Guid.NewGuid(),
            Title = "Eiffel Tower Visit",
            Description = "Amazing view from the top",
            MemoryDate = DateTime.UtcNow,
            PhotoUrl = "https://example.com/photo.jpg",
        };

        var memories = new List<Memory>();
        var mockDbSet = TestHelpers.CreateMockDbSet(memories);
        _contextMock.Setup(c => c.Memories).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Title, Is.EqualTo(command.Title));
        Assert.That(result.Description, Is.EqualTo(command.Description));
        Assert.That(result.PhotoUrl, Is.EqualTo(command.PhotoUrl));
        Assert.That(result.UserId, Is.EqualTo(command.UserId));
        Assert.That(result.TripId, Is.EqualTo(command.TripId));
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Handle_WithoutPhotoUrl_CreatesMemory()
    {
        // Arrange
        var command = new CreateMemoryCommand
        {
            UserId = Guid.NewGuid(),
            TripId = Guid.NewGuid(),
            Title = "Louvre Museum",
            Description = "Saw the Mona Lisa",
            MemoryDate = DateTime.UtcNow,
            PhotoUrl = null,
        };

        var memories = new List<Memory>();
        var mockDbSet = TestHelpers.CreateMockDbSet(memories);
        _contextMock.Setup(c => c.Memories).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.PhotoUrl, Is.Null);
    }
}
