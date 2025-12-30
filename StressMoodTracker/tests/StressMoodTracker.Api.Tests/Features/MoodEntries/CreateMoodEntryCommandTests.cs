using StressMoodTracker.Api.Features.MoodEntries;
using Microsoft.Extensions.Logging;
using Moq;

namespace StressMoodTracker.Api.Tests.Features.MoodEntries;

[TestFixture]
public class CreateMoodEntryCommandTests
{
    private Mock<IStressMoodTrackerContext> _contextMock = null!;
    private Mock<ILogger<CreateMoodEntryCommandHandler>> _loggerMock = null!;
    private CreateMoodEntryCommandHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<IStressMoodTrackerContext>();
        _loggerMock = new Mock<ILogger<CreateMoodEntryCommandHandler>>();
        _handler = new CreateMoodEntryCommandHandler(_contextMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_ValidCommand_CreatesMoodEntry()
    {
        // Arrange
        var command = new CreateMoodEntryCommand
        {
            UserId = Guid.NewGuid(),
            MoodLevel = MoodLevel.Good,
            StressLevel = StressLevel.Low,
            EntryTime = DateTime.UtcNow,
            Notes = "Feeling great today",
            Activities = "Exercise, Work",
        };

        var entries = new List<MoodEntry>();
        var mockDbSet = TestHelpers.CreateMockDbSet(entries);
        _contextMock.Setup(c => c.MoodEntries).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.MoodLevel, Is.EqualTo(command.MoodLevel));
        Assert.That(result.StressLevel, Is.EqualTo(command.StressLevel));
        Assert.That(result.Notes, Is.EqualTo(command.Notes));
        Assert.That(result.Activities, Is.EqualTo(command.Activities));
        Assert.That(result.UserId, Is.EqualTo(command.UserId));
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Handle_WithHighStress_CreatesEntryWithCorrectStressLevel()
    {
        // Arrange
        var command = new CreateMoodEntryCommand
        {
            UserId = Guid.NewGuid(),
            MoodLevel = MoodLevel.Low,
            StressLevel = StressLevel.VeryHigh,
            EntryTime = DateTime.UtcNow,
            Notes = "Very stressful day",
        };

        var entries = new List<MoodEntry>();
        var mockDbSet = TestHelpers.CreateMockDbSet(entries);
        _contextMock.Setup(c => c.MoodEntries).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.StressLevel, Is.EqualTo(StressLevel.VeryHigh));
        Assert.That(result.MoodLevel, Is.EqualTo(MoodLevel.Low));
    }
}
