using PersonalMissionStatementBuilder.Api.Features.Progresses;
using PersonalMissionStatementBuilder.Core;
using Microsoft.Extensions.Logging;
using Moq;

namespace PersonalMissionStatementBuilder.Api.Tests.Features.Progresses;

[TestFixture]
public class CreateProgressCommandTests
{
    private Mock<IPersonalMissionStatementBuilderContext> _contextMock = null!;
    private Mock<ILogger<CreateProgressCommandHandler>> _loggerMock = null!;
    private CreateProgressCommandHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<IPersonalMissionStatementBuilderContext>();
        _loggerMock = new Mock<ILogger<CreateProgressCommandHandler>>();
        _handler = new CreateProgressCommandHandler(_contextMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_ValidCommand_CreatesProgress()
    {
        // Arrange
        var progressDate = DateTime.UtcNow.AddDays(-1);
        var command = new CreateProgressCommand
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ProgressDate = progressDate,
            Notes = "Completed 5 miles today",
            CompletionPercentage = 50.0,
        };

        var progresses = new List<Progress>();
        var mockDbSet = TestHelpers.CreateMockDbSet(progresses);
        _contextMock.Setup(c => c.Progresses).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.GoalId, Is.EqualTo(command.GoalId));
        Assert.That(result.UserId, Is.EqualTo(command.UserId));
        Assert.That(result.ProgressDate, Is.EqualTo(command.ProgressDate));
        Assert.That(result.Notes, Is.EqualTo(command.Notes));
        Assert.That(result.CompletionPercentage, Is.EqualTo(command.CompletionPercentage));
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Handle_CommandWithoutProgressDate_UsesCurrentDate()
    {
        // Arrange
        var command = new CreateProgressCommand
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Notes = "Started working on goal",
            CompletionPercentage = 5.0,
        };

        var progresses = new List<Progress>();
        var mockDbSet = TestHelpers.CreateMockDbSet(progresses);
        _contextMock.Setup(c => c.Progresses).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.ProgressDate, Is.Not.Null);
        Assert.That(result.ProgressDate, Is.LessThanOrEqualTo(DateTime.UtcNow));
    }
}
