using Microsoft.Extensions.Logging;
using Moq;
using PersonalLibraryLessonsLearned.Api.Features.LessonReminder;
using PersonalLibraryLessonsLearned.Core;

namespace PersonalLibraryLessonsLearned.Api.Tests;

[TestFixture]
public class LessonReminderCommandTests
{
    private Mock<IPersonalLibraryLessonsLearnedContext> _mockContext = null!;
    private Mock<ILogger<CreateLessonReminderCommandHandler>> _mockLogger = null!;

    [SetUp]
    public void Setup()
    {
        _mockContext = new Mock<IPersonalLibraryLessonsLearnedContext>();
        _mockLogger = new Mock<ILogger<CreateLessonReminderCommandHandler>>();
    }

    [Test]
    public async Task CreateLessonReminderCommand_CreatesReminder_ReturnsDto()
    {
        // Arrange
        var reminders = new List<Core.LessonReminder>();
        var mockDbSet = TestHelpers.CreateMockDbSet(reminders);
        _mockContext.Setup(c => c.Reminders).Returns(mockDbSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = new CreateLessonReminderCommandHandler(_mockContext.Object, _mockLogger.Object);
        var command = new CreateLessonReminderCommand
        {
            LessonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ReminderDateTime = DateTime.UtcNow.AddDays(7),
            Message = "Review this lesson",
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.LessonId, Is.EqualTo(command.LessonId));
            Assert.That(result.Message, Is.EqualTo(command.Message));
            Assert.That(result.IsSent, Is.False);
            Assert.That(result.IsDismissed, Is.False);
        });

        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetLessonRemindersQuery_FiltersCorrectly_ReturnsFilteredReminders()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var lessonId = Guid.NewGuid();
        var reminders = new List<Core.LessonReminder>
        {
            new Core.LessonReminder
            {
                LessonReminderId = Guid.NewGuid(),
                LessonId = lessonId,
                UserId = userId,
                ReminderDateTime = DateTime.UtcNow.AddDays(7),
                IsSent = false,
                IsDismissed = false,
            },
            new Core.LessonReminder
            {
                LessonReminderId = Guid.NewGuid(),
                LessonId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                ReminderDateTime = DateTime.UtcNow.AddDays(14),
                IsSent = true,
                IsDismissed = false,
            },
        };

        var mockDbSet = TestHelpers.CreateMockDbSet(reminders);
        _mockContext.Setup(c => c.Reminders).Returns(mockDbSet.Object);

        var mockQueryLogger = new Mock<ILogger<GetLessonRemindersQueryHandler>>();
        var handler = new GetLessonRemindersQueryHandler(_mockContext.Object, mockQueryLogger.Object);
        var query = new GetLessonRemindersQuery { UserId = userId, IsSent = false };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.That(result.First().UserId, Is.EqualTo(userId));
        Assert.That(result.First().IsSent, Is.False);
    }
}
