using StressMoodTracker.Api.Features.Triggers;
using Microsoft.Extensions.Logging;
using Moq;

namespace StressMoodTracker.Api.Tests.Features.Triggers;

[TestFixture]
public class CreateTriggerCommandTests
{
    private Mock<IStressMoodTrackerContext> _contextMock = null!;
    private Mock<ILogger<CreateTriggerCommandHandler>> _loggerMock = null!;
    private CreateTriggerCommandHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<IStressMoodTrackerContext>();
        _loggerMock = new Mock<ILogger<CreateTriggerCommandHandler>>();
        _handler = new CreateTriggerCommandHandler(_contextMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_ValidCommand_CreatesTrigger()
    {
        // Arrange
        var command = new CreateTriggerCommand
        {
            UserId = Guid.NewGuid(),
            Name = "Work Deadlines",
            Description = "Tight deadlines at work",
            TriggerType = "Work",
            ImpactLevel = 4,
        };

        var triggers = new List<Trigger>();
        var mockDbSet = TestHelpers.CreateMockDbSet(triggers);
        _contextMock.Setup(c => c.Triggers).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(command.Name));
        Assert.That(result.Description, Is.EqualTo(command.Description));
        Assert.That(result.TriggerType, Is.EqualTo(command.TriggerType));
        Assert.That(result.ImpactLevel, Is.EqualTo(command.ImpactLevel));
        Assert.That(result.UserId, Is.EqualTo(command.UserId));
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Handle_HighImpactTrigger_CreatesWithCorrectImpactLevel()
    {
        // Arrange
        var command = new CreateTriggerCommand
        {
            UserId = Guid.NewGuid(),
            Name = "Lack of Sleep",
            Description = "Not getting enough sleep",
            TriggerType = "Health",
            ImpactLevel = 5,
        };

        var triggers = new List<Trigger>();
        var mockDbSet = TestHelpers.CreateMockDbSet(triggers);
        _contextMock.Setup(c => c.Triggers).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.ImpactLevel, Is.EqualTo(5));
        Assert.That(result.TriggerType, Is.EqualTo("Health"));
    }
}
