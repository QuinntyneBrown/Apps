using Microsoft.Extensions.Logging;
using Moq;
using PersonalLibraryLessonsLearned.Api.Features.Source;
using PersonalLibraryLessonsLearned.Core;

namespace PersonalLibraryLessonsLearned.Api.Tests;

[TestFixture]
public class SourceCommandTests
{
    private Mock<IPersonalLibraryLessonsLearnedContext> _mockContext = null!;
    private Mock<ILogger<CreateSourceCommandHandler>> _mockLogger = null!;

    [SetUp]
    public void Setup()
    {
        _mockContext = new Mock<IPersonalLibraryLessonsLearnedContext>();
        _mockLogger = new Mock<ILogger<CreateSourceCommandHandler>>();
    }

    [Test]
    public async Task CreateSourceCommand_CreatesSource_ReturnsDto()
    {
        // Arrange
        var sources = new List<Core.Source>();
        var mockDbSet = TestHelpers.CreateMockDbSet(sources);
        _mockContext.Setup(c => c.Sources).Returns(mockDbSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = new CreateSourceCommandHandler(_mockContext.Object, _mockLogger.Object);
        var command = new CreateSourceCommand
        {
            UserId = Guid.NewGuid(),
            Title = "Test Book",
            Author = "Test Author",
            SourceType = "Book",
            Url = "https://example.com",
            DateConsumed = DateTime.UtcNow,
            Notes = "Test Notes",
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Title, Is.EqualTo(command.Title));
            Assert.That(result.Author, Is.EqualTo(command.Author));
            Assert.That(result.SourceType, Is.EqualTo(command.SourceType));
        });

        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetSourcesQuery_FiltersCorrectly_ReturnsFilteredSources()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var sources = new List<Core.Source>
        {
            new Core.Source
            {
                SourceId = Guid.NewGuid(),
                UserId = userId,
                Title = "Book 1",
                SourceType = "Book",
            },
            new Core.Source
            {
                SourceId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Title = "Article 1",
                SourceType = "Article",
            },
        };

        var mockDbSet = TestHelpers.CreateMockDbSet(sources);
        _mockContext.Setup(c => c.Sources).Returns(mockDbSet.Object);

        var mockQueryLogger = new Mock<ILogger<GetSourcesQueryHandler>>();
        var handler = new GetSourcesQueryHandler(_mockContext.Object, mockQueryLogger.Object);
        var query = new GetSourcesQuery { UserId = userId };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.That(result.First().UserId, Is.EqualTo(userId));
    }
}
