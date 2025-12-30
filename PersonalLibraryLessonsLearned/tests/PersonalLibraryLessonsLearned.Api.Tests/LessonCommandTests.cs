using Microsoft.Extensions.Logging;
using Moq;
using PersonalLibraryLessonsLearned.Api.Features.Lesson;
using PersonalLibraryLessonsLearned.Core;

namespace PersonalLibraryLessonsLearned.Api.Tests;

[TestFixture]
public class LessonCommandTests
{
    private Mock<IPersonalLibraryLessonsLearnedContext> _mockContext = null!;
    private Mock<ILogger<CreateLessonCommandHandler>> _mockLogger = null!;

    [SetUp]
    public void Setup()
    {
        _mockContext = new Mock<IPersonalLibraryLessonsLearnedContext>();
        _mockLogger = new Mock<ILogger<CreateLessonCommandHandler>>();
    }

    [Test]
    public async Task CreateLessonCommand_CreatesLesson_ReturnsDto()
    {
        // Arrange
        var lessons = new List<Core.Lesson>();
        var mockDbSet = TestHelpers.CreateMockDbSet(lessons);
        _mockContext.Setup(c => c.Lessons).Returns(mockDbSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = new CreateLessonCommandHandler(_mockContext.Object, _mockLogger.Object);
        var command = new CreateLessonCommand
        {
            UserId = Guid.NewGuid(),
            SourceId = Guid.NewGuid(),
            Title = "Test Lesson",
            Content = "Test Content",
            Category = LessonCategory.Professional,
            Tags = "test",
            DateLearned = DateTime.UtcNow,
            Application = "Test Application",
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Title, Is.EqualTo(command.Title));
            Assert.That(result.Content, Is.EqualTo(command.Content));
            Assert.That(result.Category, Is.EqualTo(command.Category));
            Assert.That(result.IsApplied, Is.False);
        });

        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetLessonsQuery_FiltersCorrectly_ReturnsFilteredLessons()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var lessons = new List<Core.Lesson>
        {
            new Core.Lesson
            {
                LessonId = Guid.NewGuid(),
                UserId = userId,
                Title = "Lesson 1",
                Content = "Content 1",
                Category = LessonCategory.PersonalDevelopment,
                DateLearned = DateTime.UtcNow,
                IsApplied = true,
            },
            new Core.Lesson
            {
                LessonId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Title = "Lesson 2",
                Content = "Content 2",
                Category = LessonCategory.Professional,
                DateLearned = DateTime.UtcNow,
                IsApplied = false,
            },
        };

        var mockDbSet = TestHelpers.CreateMockDbSet(lessons);
        _mockContext.Setup(c => c.Lessons).Returns(mockDbSet.Object);

        var mockQueryLogger = new Mock<ILogger<GetLessonsQueryHandler>>();
        var handler = new GetLessonsQueryHandler(_mockContext.Object, mockQueryLogger.Object);
        var query = new GetLessonsQuery { UserId = userId };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.That(result.First().UserId, Is.EqualTo(userId));
    }
}
