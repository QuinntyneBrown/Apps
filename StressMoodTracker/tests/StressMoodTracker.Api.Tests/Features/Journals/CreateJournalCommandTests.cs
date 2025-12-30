using StressMoodTracker.Api.Features.Journals;
using Microsoft.Extensions.Logging;
using Moq;

namespace StressMoodTracker.Api.Tests.Features.Journals;

[TestFixture]
public class CreateJournalCommandTests
{
    private Mock<IStressMoodTrackerContext> _contextMock = null!;
    private Mock<ILogger<CreateJournalCommandHandler>> _loggerMock = null!;
    private CreateJournalCommandHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<IStressMoodTrackerContext>();
        _loggerMock = new Mock<ILogger<CreateJournalCommandHandler>>();
        _handler = new CreateJournalCommandHandler(_contextMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_ValidCommand_CreatesJournal()
    {
        // Arrange
        var command = new CreateJournalCommand
        {
            UserId = Guid.NewGuid(),
            Title = "Daily Reflection",
            Content = "Today was a productive day with good progress on my goals.",
            EntryDate = DateTime.UtcNow,
            Tags = "reflection, productivity",
        };

        var journals = new List<Journal>();
        var mockDbSet = TestHelpers.CreateMockDbSet(journals);
        _contextMock.Setup(c => c.Journals).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Title, Is.EqualTo(command.Title));
        Assert.That(result.Content, Is.EqualTo(command.Content));
        Assert.That(result.Tags, Is.EqualTo(command.Tags));
        Assert.That(result.UserId, Is.EqualTo(command.UserId));
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Handle_WithTags_CreatesJournalWithTags()
    {
        // Arrange
        var command = new CreateJournalCommand
        {
            UserId = Guid.NewGuid(),
            Title = "Gratitude Entry",
            Content = "Three things I'm grateful for today...",
            EntryDate = DateTime.UtcNow,
            Tags = "gratitude, positivity, wellbeing",
        };

        var journals = new List<Journal>();
        var mockDbSet = TestHelpers.CreateMockDbSet(journals);
        _contextMock.Setup(c => c.Journals).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.Tags, Is.EqualTo("gratitude, positivity, wellbeing"));
    }
}
