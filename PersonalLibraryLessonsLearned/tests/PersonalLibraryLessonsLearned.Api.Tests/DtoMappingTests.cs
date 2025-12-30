using PersonalLibraryLessonsLearned.Api.Features.Lesson;
using PersonalLibraryLessonsLearned.Api.Features.Source;
using PersonalLibraryLessonsLearned.Api.Features.LessonReminder;

namespace PersonalLibraryLessonsLearned.Api.Tests;

[TestFixture]
public class DtoMappingTests
{
    [Test]
    public void LessonDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var lesson = new Core.Lesson
        {
            LessonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            SourceId = Guid.NewGuid(),
            Title = "Test Lesson",
            Content = "Test Content",
            Category = Core.LessonCategory.PersonalDevelopment,
            Tags = "test,lesson",
            DateLearned = DateTime.UtcNow.AddDays(-1),
            Application = "Test Application",
            IsApplied = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = lesson.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.LessonId, Is.EqualTo(lesson.LessonId));
            Assert.That(dto.UserId, Is.EqualTo(lesson.UserId));
            Assert.That(dto.SourceId, Is.EqualTo(lesson.SourceId));
            Assert.That(dto.Title, Is.EqualTo(lesson.Title));
            Assert.That(dto.Content, Is.EqualTo(lesson.Content));
            Assert.That(dto.Category, Is.EqualTo(lesson.Category));
            Assert.That(dto.Tags, Is.EqualTo(lesson.Tags));
            Assert.That(dto.DateLearned, Is.EqualTo(lesson.DateLearned));
            Assert.That(dto.Application, Is.EqualTo(lesson.Application));
            Assert.That(dto.IsApplied, Is.EqualTo(lesson.IsApplied));
            Assert.That(dto.CreatedAt, Is.EqualTo(lesson.CreatedAt));
        });
    }

    [Test]
    public void SourceDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var source = new Core.Source
        {
            SourceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Book",
            Author = "Test Author",
            SourceType = "Book",
            Url = "https://example.com",
            DateConsumed = DateTime.UtcNow.AddDays(-7),
            Notes = "Test Notes",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = source.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.SourceId, Is.EqualTo(source.SourceId));
            Assert.That(dto.UserId, Is.EqualTo(source.UserId));
            Assert.That(dto.Title, Is.EqualTo(source.Title));
            Assert.That(dto.Author, Is.EqualTo(source.Author));
            Assert.That(dto.SourceType, Is.EqualTo(source.SourceType));
            Assert.That(dto.Url, Is.EqualTo(source.Url));
            Assert.That(dto.DateConsumed, Is.EqualTo(source.DateConsumed));
            Assert.That(dto.Notes, Is.EqualTo(source.Notes));
            Assert.That(dto.CreatedAt, Is.EqualTo(source.CreatedAt));
        });
    }

    [Test]
    public void LessonReminderDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var reminder = new Core.LessonReminder
        {
            LessonReminderId = Guid.NewGuid(),
            LessonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ReminderDateTime = DateTime.UtcNow.AddDays(7),
            Message = "Review this lesson",
            IsSent = false,
            IsDismissed = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = reminder.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.LessonReminderId, Is.EqualTo(reminder.LessonReminderId));
            Assert.That(dto.LessonId, Is.EqualTo(reminder.LessonId));
            Assert.That(dto.UserId, Is.EqualTo(reminder.UserId));
            Assert.That(dto.ReminderDateTime, Is.EqualTo(reminder.ReminderDateTime));
            Assert.That(dto.Message, Is.EqualTo(reminder.Message));
            Assert.That(dto.IsSent, Is.EqualTo(reminder.IsSent));
            Assert.That(dto.IsDismissed, Is.EqualTo(reminder.IsDismissed));
            Assert.That(dto.CreatedAt, Is.EqualTo(reminder.CreatedAt));
        });
    }
}
