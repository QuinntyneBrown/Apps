using StressMoodTracker.Api.Features.Triggers;
using Microsoft.Extensions.Logging;
using Moq;

namespace StressMoodTracker.Api.Tests.Features.Triggers;

[TestFixture]
public class GetTriggersQueryTests
{
    private Mock<IStressMoodTrackerContext> _contextMock = null!;
    private Mock<ILogger<GetTriggersQueryHandler>> _loggerMock = null!;
    private GetTriggersQueryHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<IStressMoodTrackerContext>();
        _loggerMock = new Mock<ILogger<GetTriggersQueryHandler>>();
        _handler = new GetTriggersQueryHandler(_contextMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_ReturnsAllTriggers_WhenNoFiltersApplied()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var triggers = new List<Trigger>
        {
            new Trigger
            {
                TriggerId = Guid.NewGuid(),
                UserId = userId,
                Name = "Work Stress",
                TriggerType = "Work",
                ImpactLevel = 4,
            },
            new Trigger
            {
                TriggerId = Guid.NewGuid(),
                UserId = userId,
                Name = "Sleep Issues",
                TriggerType = "Health",
                ImpactLevel = 5,
            },
        };

        var mockDbSet = TestHelpers.CreateMockDbSet(triggers);
        _contextMock.Setup(c => c.Triggers).Returns(mockDbSet.Object);

        var query = new GetTriggersQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task Handle_FiltersByUserId_WhenUserIdProvided()
    {
        // Arrange
        var userId1 = Guid.NewGuid();
        var userId2 = Guid.NewGuid();
        var triggers = new List<Trigger>
        {
            new Trigger
            {
                TriggerId = Guid.NewGuid(),
                UserId = userId1,
                Name = "Work Stress",
                TriggerType = "Work",
                ImpactLevel = 4,
            },
            new Trigger
            {
                TriggerId = Guid.NewGuid(),
                UserId = userId2,
                Name = "Family Issues",
                TriggerType = "Personal",
                ImpactLevel = 3,
            },
        };

        var mockDbSet = TestHelpers.CreateMockDbSet(triggers);
        _contextMock.Setup(c => c.Triggers).Returns(mockDbSet.Object);

        var query = new GetTriggersQuery { UserId = userId1 };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.That(result.First().UserId, Is.EqualTo(userId1));
    }

    [Test]
    public async Task Handle_FiltersByMinImpactLevel_WhenMinImpactLevelProvided()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var triggers = new List<Trigger>
        {
            new Trigger
            {
                TriggerId = Guid.NewGuid(),
                UserId = userId,
                Name = "Minor Annoyance",
                TriggerType = "Work",
                ImpactLevel = 2,
            },
            new Trigger
            {
                TriggerId = Guid.NewGuid(),
                UserId = userId,
                Name = "Major Stress",
                TriggerType = "Work",
                ImpactLevel = 5,
            },
        };

        var mockDbSet = TestHelpers.CreateMockDbSet(triggers);
        _contextMock.Setup(c => c.Triggers).Returns(mockDbSet.Object);

        var query = new GetTriggersQuery { MinImpactLevel = 4 };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.That(result.First().ImpactLevel, Is.EqualTo(5));
    }
}
