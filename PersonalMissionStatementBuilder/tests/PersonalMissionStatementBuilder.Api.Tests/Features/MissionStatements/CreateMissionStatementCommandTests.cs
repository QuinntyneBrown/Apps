using PersonalMissionStatementBuilder.Api.Features.MissionStatements;
using PersonalMissionStatementBuilder.Core;
using Microsoft.Extensions.Logging;
using Moq;

namespace PersonalMissionStatementBuilder.Api.Tests.Features.MissionStatements;

[TestFixture]
public class CreateMissionStatementCommandTests
{
    private Mock<IPersonalMissionStatementBuilderContext> _contextMock = null!;
    private Mock<ILogger<CreateMissionStatementCommandHandler>> _loggerMock = null!;
    private CreateMissionStatementCommandHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<IPersonalMissionStatementBuilderContext>();
        _loggerMock = new Mock<ILogger<CreateMissionStatementCommandHandler>>();
        _handler = new CreateMissionStatementCommandHandler(_contextMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_ValidCommand_CreatesMissionStatement()
    {
        // Arrange
        var command = new CreateMissionStatementCommand
        {
            UserId = Guid.NewGuid(),
            Title = "My Personal Mission",
            Text = "To inspire and empower others through technology",
            StatementDate = DateTime.UtcNow,
        };

        var missionStatements = new List<MissionStatement>();
        var mockDbSet = TestHelpers.CreateMockDbSet(missionStatements);
        _contextMock.Setup(c => c.MissionStatements).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Title, Is.EqualTo(command.Title));
        Assert.That(result.Text, Is.EqualTo(command.Text));
        Assert.That(result.UserId, Is.EqualTo(command.UserId));
        Assert.That(result.Version, Is.EqualTo(1));
        Assert.That(result.IsCurrentVersion, Is.True);
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Handle_CommandWithoutStatementDate_UsesCurrentDate()
    {
        // Arrange
        var command = new CreateMissionStatementCommand
        {
            UserId = Guid.NewGuid(),
            Title = "Simple Mission",
            Text = "Make a difference",
        };

        var missionStatements = new List<MissionStatement>();
        var mockDbSet = TestHelpers.CreateMockDbSet(missionStatements);
        _contextMock.Setup(c => c.MissionStatements).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.StatementDate, Is.Not.Null);
        Assert.That(result.StatementDate, Is.LessThanOrEqualTo(DateTime.UtcNow));
    }
}
