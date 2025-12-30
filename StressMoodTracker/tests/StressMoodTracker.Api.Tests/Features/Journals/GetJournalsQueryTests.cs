using StressMoodTracker.Api.Features.Journals;
using Microsoft.Extensions.Logging;
using Moq;

namespace StressMoodTracker.Api.Tests.Features.Journals;

[TestFixture]
public class GetJournalsQueryTests
{
    private Mock<IStressMoodTrackerContext> _contextMock = null!;
    private Mock<ILogger<GetJournalsQueryHandler>> _loggerMock = null!;
    private GetJournalsQueryHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<IStressMoodTrackerContext>();
        _loggerMock = new Mock<ILogger<GetJournalsQueryHandler>>();
        _handler = new GetJournalsQueryHandler(_contextMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_ReturnsAllJournals_WhenNoFiltersApplied()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var journals = new List<Journal>
        {
            new Journal
            {
                JournalId = Guid.NewGuid(),
                UserId = userId,
                Title = "Morning Reflection",
                Content = "Started the day with meditation",
                EntryDate = DateTime.UtcNow.AddDays(-1),
            },
            new Journal
            {
                JournalId = Guid.NewGuid(),
                UserId = userId,
                Title = "Evening Thoughts",
                Content = "Reflecting on the day's events",
                EntryDate = DateTime.UtcNow,
            },
        };

        var mockDbSet = TestHelpers.CreateMockDbSet(journals);
        _contextMock.Setup(c => c.Journals).Returns(mockDbSet.Object);

        var query = new GetJournalsQuery();

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
        var journals = new List<Journal>
        {
            new Journal
            {
                JournalId = Guid.NewGuid(),
                UserId = userId1,
                Title = "User 1 Entry",
                Content = "Content for user 1",
                EntryDate = DateTime.UtcNow,
            },
            new Journal
            {
                JournalId = Guid.NewGuid(),
                UserId = userId2,
                Title = "User 2 Entry",
                Content = "Content for user 2",
                EntryDate = DateTime.UtcNow,
            },
        };

        var mockDbSet = TestHelpers.CreateMockDbSet(journals);
        _contextMock.Setup(c => c.Journals).Returns(mockDbSet.Object);

        var query = new GetJournalsQuery { UserId = userId1 };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.That(result.First().UserId, Is.EqualTo(userId1));
    }

    [Test]
    public async Task Handle_FiltersByTag_WhenTagProvided()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var journals = new List<Journal>
        {
            new Journal
            {
                JournalId = Guid.NewGuid(),
                UserId = userId,
                Title = "Gratitude Entry",
                Content = "Things I'm grateful for",
                EntryDate = DateTime.UtcNow,
                Tags = "gratitude, positivity",
            },
            new Journal
            {
                JournalId = Guid.NewGuid(),
                UserId = userId,
                Title = "Work Reflection",
                Content = "Thoughts about work",
                EntryDate = DateTime.UtcNow,
                Tags = "work, productivity",
            },
        };

        var mockDbSet = TestHelpers.CreateMockDbSet(journals);
        _contextMock.Setup(c => c.Journals).Returns(mockDbSet.Object);

        var query = new GetJournalsQuery { Tag = "gratitude" };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.That(result.First().Tags, Does.Contain("gratitude"));
    }
}
