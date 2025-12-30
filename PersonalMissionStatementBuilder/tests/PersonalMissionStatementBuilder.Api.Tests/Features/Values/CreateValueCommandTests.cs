using PersonalMissionStatementBuilder.Api.Features.Values;
using PersonalMissionStatementBuilder.Core;
using Microsoft.Extensions.Logging;
using Moq;

namespace PersonalMissionStatementBuilder.Api.Tests.Features.Values;

[TestFixture]
public class CreateValueCommandTests
{
    private Mock<IPersonalMissionStatementBuilderContext> _contextMock = null!;
    private Mock<ILogger<CreateValueCommandHandler>> _loggerMock = null!;
    private CreateValueCommandHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<IPersonalMissionStatementBuilderContext>();
        _loggerMock = new Mock<ILogger<CreateValueCommandHandler>>();
        _handler = new CreateValueCommandHandler(_contextMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_ValidCommand_CreatesValue()
    {
        // Arrange
        var command = new CreateValueCommand
        {
            UserId = Guid.NewGuid(),
            MissionStatementId = Guid.NewGuid(),
            Name = "Excellence",
            Description = "Striving for the highest quality",
            Priority = 1,
        };

        var values = new List<Value>();
        var mockDbSet = TestHelpers.CreateMockDbSet(values);
        _contextMock.Setup(c => c.Values).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(command.Name));
        Assert.That(result.Description, Is.EqualTo(command.Description));
        Assert.That(result.Priority, Is.EqualTo(command.Priority));
        Assert.That(result.UserId, Is.EqualTo(command.UserId));
        Assert.That(result.MissionStatementId, Is.EqualTo(command.MissionStatementId));
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
