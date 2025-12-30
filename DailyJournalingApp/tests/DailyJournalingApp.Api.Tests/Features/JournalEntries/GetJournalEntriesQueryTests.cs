using DailyJournalingApp.Api.Features.JournalEntries;
using DailyJournalingApp.Core;
using Microsoft.Extensions.Logging;
using Moq;

namespace DailyJournalingApp.Api.Tests.Features.JournalEntries;

[TestFixture]
public class GetJournalEntriesQueryTests
{
    private Mock<IDailyJournalingAppContext> _contextMock = null!;
    private Mock<ILogger<GetJournalEntriesQueryHandler>> _loggerMock = null!;
    private GetJournalEntriesQueryHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<IDailyJournalingAppContext>();
        _loggerMock = new Mock<ILogger<GetJournalEntriesQueryHandler>>();
        _handler = new GetJournalEntriesQueryHandler(_contextMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_NoFilters_ReturnsAllEntries()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var entries = new List<JournalEntry>
        {
            new JournalEntry
            {
                JournalEntryId = Guid.NewGuid(),
                UserId = userId,
                Title = "Entry 1",
                Content = "Content 1",
                EntryDate = DateTime.UtcNow,
                Mood = Mood.Happy,
            },
            new JournalEntry
            {
                JournalEntryId = Guid.NewGuid(),
                UserId = userId,
                Title = "Entry 2",
                Content = "Content 2",
                EntryDate = DateTime.UtcNow.AddDays(-1),
                Mood = Mood.Calm,
            },
        };

        var mockDbSet = TestHelpers.CreateMockDbSet(entries);
        _contextMock.Setup(c => c.JournalEntries).Returns(mockDbSet.Object);

        var query = new GetJournalEntriesQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task Handle_FilterByUserId_ReturnsUserEntries()
    {
        // Arrange
        var userId1 = Guid.NewGuid();
        var userId2 = Guid.NewGuid();
        var entries = new List<JournalEntry>
        {
            new JournalEntry
            {
                JournalEntryId = Guid.NewGuid(),
                UserId = userId1,
                Title = "User1 Entry",
                Content = "Content",
                EntryDate = DateTime.UtcNow,
                Mood = Mood.Happy,
            },
            new JournalEntry
            {
                JournalEntryId = Guid.NewGuid(),
                UserId = userId2,
                Title = "User2 Entry",
                Content = "Content",
                EntryDate = DateTime.UtcNow,
                Mood = Mood.Calm,
            },
        };

        var mockDbSet = TestHelpers.CreateMockDbSet(entries);
        _contextMock.Setup(c => c.JournalEntries).Returns(mockDbSet.Object);

        var query = new GetJournalEntriesQuery { UserId = userId1 };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.That(result.First().UserId, Is.EqualTo(userId1));
    }

    [Test]
    public async Task Handle_FilterByMood_ReturnsMoodEntries()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var entries = new List<JournalEntry>
        {
            new JournalEntry
            {
                JournalEntryId = Guid.NewGuid(),
                UserId = userId,
                Title = "Happy Entry",
                Content = "Content",
                EntryDate = DateTime.UtcNow,
                Mood = Mood.Happy,
            },
            new JournalEntry
            {
                JournalEntryId = Guid.NewGuid(),
                UserId = userId,
                Title = "Sad Entry",
                Content = "Content",
                EntryDate = DateTime.UtcNow,
                Mood = Mood.Sad,
            },
        };

        var mockDbSet = TestHelpers.CreateMockDbSet(entries);
        _contextMock.Setup(c => c.JournalEntries).Returns(mockDbSet.Object);

        var query = new GetJournalEntriesQuery { Mood = Mood.Happy };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.That(result.First().Mood, Is.EqualTo(Mood.Happy));
    }

    [Test]
    public async Task Handle_FavoritesOnly_ReturnsFavoriteEntries()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var entries = new List<JournalEntry>
        {
            new JournalEntry
            {
                JournalEntryId = Guid.NewGuid(),
                UserId = userId,
                Title = "Favorite Entry",
                Content = "Content",
                EntryDate = DateTime.UtcNow,
                Mood = Mood.Happy,
                IsFavorite = true,
            },
            new JournalEntry
            {
                JournalEntryId = Guid.NewGuid(),
                UserId = userId,
                Title = "Regular Entry",
                Content = "Content",
                EntryDate = DateTime.UtcNow,
                Mood = Mood.Calm,
                IsFavorite = false,
            },
        };

        var mockDbSet = TestHelpers.CreateMockDbSet(entries);
        _contextMock.Setup(c => c.JournalEntries).Returns(mockDbSet.Object);

        var query = new GetJournalEntriesQuery { FavoritesOnly = true };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.That(result.First().IsFavorite, Is.True);
    }
}
