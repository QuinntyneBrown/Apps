using DailyJournalingApp.Api.Features.JournalEntries;
using DailyJournalingApp.Core;
using Microsoft.Extensions.Logging;
using Moq;

namespace DailyJournalingApp.Api.Tests.Features.JournalEntries;

[TestFixture]
public class CreateJournalEntryCommandTests
{
    private Mock<IDailyJournalingAppContext> _contextMock = null!;
    private Mock<ILogger<CreateJournalEntryCommandHandler>> _loggerMock = null!;
    private CreateJournalEntryCommandHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<IDailyJournalingAppContext>();
        _loggerMock = new Mock<ILogger<CreateJournalEntryCommandHandler>>();
        _handler = new CreateJournalEntryCommandHandler(_contextMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_ValidCommand_CreatesJournalEntry()
    {
        // Arrange
        var command = new CreateJournalEntryCommand
        {
            UserId = Guid.NewGuid(),
            Title = "Test Entry",
            Content = "Test content for journal entry",
            EntryDate = DateTime.UtcNow,
            Mood = Mood.Happy,
            Tags = "Test,Entry",
        };

        var entries = new List<JournalEntry>();
        var mockDbSet = TestHelpers.CreateMockDbSet(entries);
        _contextMock.Setup(c => c.JournalEntries).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Title, Is.EqualTo(command.Title));
        Assert.That(result.Content, Is.EqualTo(command.Content));
        Assert.That(result.Mood, Is.EqualTo(command.Mood));
        Assert.That(result.UserId, Is.EqualTo(command.UserId));
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Handle_WithPromptId_IncludesPromptIdInEntry()
    {
        // Arrange
        var promptId = Guid.NewGuid();
        var command = new CreateJournalEntryCommand
        {
            UserId = Guid.NewGuid(),
            Title = "Prompted Entry",
            Content = "Entry from a prompt",
            EntryDate = DateTime.UtcNow,
            Mood = Mood.Calm,
            PromptId = promptId,
        };

        var entries = new List<JournalEntry>();
        var mockDbSet = TestHelpers.CreateMockDbSet(entries);
        _contextMock.Setup(c => c.JournalEntries).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.PromptId, Is.EqualTo(promptId));
    }
}
