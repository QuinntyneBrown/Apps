using PersonalMissionStatementBuilder.Api.Features.Goals;
using PersonalMissionStatementBuilder.Core;
using Microsoft.Extensions.Logging;
using Moq;

namespace PersonalMissionStatementBuilder.Api.Tests.Features.Goals;

[TestFixture]
public class CreateGoalCommandTests
{
    private Mock<IPersonalMissionStatementBuilderContext> _contextMock = null!;
    private Mock<ILogger<CreateGoalCommandHandler>> _loggerMock = null!;
    private CreateGoalCommandHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<IPersonalMissionStatementBuilderContext>();
        _loggerMock = new Mock<ILogger<CreateGoalCommandHandler>>();
        _handler = new CreateGoalCommandHandler(_contextMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_ValidCommand_CreatesGoal()
    {
        // Arrange
        var targetDate = DateTime.UtcNow.AddMonths(3);
        var command = new CreateGoalCommand
        {
            UserId = Guid.NewGuid(),
            MissionStatementId = Guid.NewGuid(),
            Title = "Run a marathon",
            Description = "Complete a full 26.2 mile marathon",
            Status = GoalStatus.InProgress,
            TargetDate = targetDate,
        };

        var goals = new List<Goal>();
        var mockDbSet = TestHelpers.CreateMockDbSet(goals);
        _contextMock.Setup(c => c.Goals).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Title, Is.EqualTo(command.Title));
        Assert.That(result.Description, Is.EqualTo(command.Description));
        Assert.That(result.Status, Is.EqualTo(command.Status));
        Assert.That(result.TargetDate, Is.EqualTo(command.TargetDate));
        Assert.That(result.UserId, Is.EqualTo(command.UserId));
        Assert.That(result.MissionStatementId, Is.EqualTo(command.MissionStatementId));
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Handle_MinimalCommand_CreatesGoalWithDefaults()
    {
        // Arrange
        var command = new CreateGoalCommand
        {
            UserId = Guid.NewGuid(),
            Title = "Read more books",
        };

        var goals = new List<Goal>();
        var mockDbSet = TestHelpers.CreateMockDbSet(goals);
        _contextMock.Setup(c => c.Goals).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Title, Is.EqualTo(command.Title));
        Assert.That(result.Status, Is.EqualTo(GoalStatus.NotStarted));
        Assert.That(result.Description, Is.Null);
        Assert.That(result.TargetDate, Is.Null);
    }
}
