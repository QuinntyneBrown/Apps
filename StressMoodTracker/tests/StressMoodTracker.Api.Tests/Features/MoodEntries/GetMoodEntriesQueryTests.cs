using StressMoodTracker.Api.Features.MoodEntries;
using Microsoft.Extensions.Logging;
using Moq;

namespace StressMoodTracker.Api.Tests.Features.MoodEntries;

[TestFixture]
public class GetMoodEntriesQueryTests
{
    private Mock<IStressMoodTrackerContext> _contextMock = null!;
    private Mock<ILogger<GetMoodEntriesQueryHandler>> _loggerMock = null!;
    private GetMoodEntriesQueryHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<IStressMoodTrackerContext>();
        _loggerMock = new Mock<ILogger<GetMoodEntriesQueryHandler>>();
        _handler = new GetMoodEntriesQueryHandler(_contextMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_ReturnsAllEntries_WhenNoFiltersApplied()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var entries = new List<MoodEntry>
        {
            new MoodEntry
            {
                MoodEntryId = Guid.NewGuid(),
                UserId = userId,
                MoodLevel = MoodLevel.Good,
                StressLevel = StressLevel.Low,
                EntryTime = DateTime.UtcNow.AddDays(-1),
            },
            new MoodEntry
            {
                MoodEntryId = Guid.NewGuid(),
                UserId = userId,
                MoodLevel = MoodLevel.Neutral,
                StressLevel = StressLevel.Moderate,
                EntryTime = DateTime.UtcNow,
            },
        };

        var mockDbSet = TestHelpers.CreateMockDbSet(entries);
        _contextMock.Setup(c => c.MoodEntries).Returns(mockDbSet.Object);

        var query = new GetMoodEntriesQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task Handle_FiltersbyUserId_WhenUserIdProvided()
    {
        // Arrange
        var userId1 = Guid.NewGuid();
        var userId2 = Guid.NewGuid();
        var entries = new List<MoodEntry>
        {
            new MoodEntry
            {
                MoodEntryId = Guid.NewGuid(),
                UserId = userId1,
                MoodLevel = MoodLevel.Good,
                StressLevel = StressLevel.Low,
                EntryTime = DateTime.UtcNow,
            },
            new MoodEntry
            {
                MoodEntryId = Guid.NewGuid(),
                UserId = userId2,
                MoodLevel = MoodLevel.Neutral,
                StressLevel = StressLevel.Moderate,
                EntryTime = DateTime.UtcNow,
            },
        };

        var mockDbSet = TestHelpers.CreateMockDbSet(entries);
        _contextMock.Setup(c => c.MoodEntries).Returns(mockDbSet.Object);

        var query = new GetMoodEntriesQuery { UserId = userId1 };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.That(result.First().UserId, Is.EqualTo(userId1));
    }

    [Test]
    public async Task Handle_FiltersByMoodLevel_WhenMoodLevelProvided()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var entries = new List<MoodEntry>
        {
            new MoodEntry
            {
                MoodEntryId = Guid.NewGuid(),
                UserId = userId,
                MoodLevel = MoodLevel.Excellent,
                StressLevel = StressLevel.Low,
                EntryTime = DateTime.UtcNow,
            },
            new MoodEntry
            {
                MoodEntryId = Guid.NewGuid(),
                UserId = userId,
                MoodLevel = MoodLevel.Low,
                StressLevel = StressLevel.High,
                EntryTime = DateTime.UtcNow,
            },
        };

        var mockDbSet = TestHelpers.CreateMockDbSet(entries);
        _contextMock.Setup(c => c.MoodEntries).Returns(mockDbSet.Object);

        var query = new GetMoodEntriesQuery { MoodLevel = MoodLevel.Excellent };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.That(result.First().MoodLevel, Is.EqualTo(MoodLevel.Excellent));
    }
}
