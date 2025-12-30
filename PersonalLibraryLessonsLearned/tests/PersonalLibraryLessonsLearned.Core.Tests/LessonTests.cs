namespace PersonalLibraryLessonsLearned.Core.Tests;

public class LessonTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesLesson()
    {
        // Arrange
        var lessonId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var sourceId = Guid.NewGuid();
        var dateLearned = DateTime.UtcNow.AddDays(-5);

        // Act
        var lesson = new Lesson
        {
            LessonId = lessonId,
            UserId = userId,
            SourceId = sourceId,
            Title = "Always validate user input",
            Content = "Never trust data coming from external sources",
            Category = LessonCategory.Technical,
            Tags = "security,validation",
            DateLearned = dateLearned,
            Application = "Implement input validation in all API endpoints",
            IsApplied = false
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(lesson.LessonId, Is.EqualTo(lessonId));
            Assert.That(lesson.UserId, Is.EqualTo(userId));
            Assert.That(lesson.SourceId, Is.EqualTo(sourceId));
            Assert.That(lesson.Title, Is.EqualTo("Always validate user input"));
            Assert.That(lesson.Content, Is.EqualTo("Never trust data coming from external sources"));
            Assert.That(lesson.Category, Is.EqualTo(LessonCategory.Technical));
            Assert.That(lesson.Tags, Is.EqualTo("security,validation"));
            Assert.That(lesson.DateLearned, Is.EqualTo(dateLearned));
            Assert.That(lesson.Application, Is.EqualTo("Implement input validation in all API endpoints"));
            Assert.That(lesson.IsApplied, Is.False);
            Assert.That(lesson.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void MarkAsApplied_SetsIsAppliedToTrue()
    {
        // Arrange
        var lesson = new Lesson
        {
            LessonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Lesson",
            Content = "Test Content",
            Category = LessonCategory.Technical,
            DateLearned = DateTime.UtcNow,
            IsApplied = false
        };

        // Act
        lesson.MarkAsApplied();

        // Assert
        Assert.That(lesson.IsApplied, Is.True);
    }

    [Test]
    public void MarkAsApplied_AlreadyApplied_RemainsTrue()
    {
        // Arrange
        var lesson = new Lesson
        {
            LessonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Lesson",
            Content = "Test Content",
            Category = LessonCategory.Technical,
            DateLearned = DateTime.UtcNow,
            IsApplied = true
        };

        // Act
        lesson.MarkAsApplied();

        // Assert
        Assert.That(lesson.IsApplied, Is.True);
    }

    [Test]
    public void HasReminders_WithReminders_ReturnsTrue()
    {
        // Arrange
        var lesson = new Lesson
        {
            LessonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Lesson",
            Content = "Test Content",
            Category = LessonCategory.Technical,
            DateLearned = DateTime.UtcNow,
            Reminders = new List<LessonReminder>
            {
                new LessonReminder { LessonReminderId = Guid.NewGuid(), ReminderDateTime = DateTime.UtcNow.AddDays(1) }
            }
        };

        // Act
        var result = lesson.HasReminders();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void HasReminders_WithoutReminders_ReturnsFalse()
    {
        // Arrange
        var lesson = new Lesson
        {
            LessonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Lesson",
            Content = "Test Content",
            Category = LessonCategory.Technical,
            DateLearned = DateTime.UtcNow,
            Reminders = new List<LessonReminder>()
        };

        // Act
        var result = lesson.HasReminders();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void HasReminders_MultipleReminders_ReturnsTrue()
    {
        // Arrange
        var lesson = new Lesson
        {
            LessonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Lesson",
            Content = "Test Content",
            Category = LessonCategory.Technical,
            DateLearned = DateTime.UtcNow,
            Reminders = new List<LessonReminder>
            {
                new LessonReminder { LessonReminderId = Guid.NewGuid(), ReminderDateTime = DateTime.UtcNow.AddDays(1) },
                new LessonReminder { LessonReminderId = Guid.NewGuid(), ReminderDateTime = DateTime.UtcNow.AddDays(7) }
            }
        };

        // Act
        var result = lesson.HasReminders();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void Lesson_AllCategories_CanBeAssigned()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(() => new Lesson { Category = LessonCategory.PersonalDevelopment }, Throws.Nothing);
            Assert.That(() => new Lesson { Category = LessonCategory.Professional }, Throws.Nothing);
            Assert.That(() => new Lesson { Category = LessonCategory.Health }, Throws.Nothing);
            Assert.That(() => new Lesson { Category = LessonCategory.Relationships }, Throws.Nothing);
            Assert.That(() => new Lesson { Category = LessonCategory.Finance }, Throws.Nothing);
            Assert.That(() => new Lesson { Category = LessonCategory.Leadership }, Throws.Nothing);
            Assert.That(() => new Lesson { Category = LessonCategory.Technical }, Throws.Nothing);
            Assert.That(() => new Lesson { Category = LessonCategory.LifeWisdom }, Throws.Nothing);
            Assert.That(() => new Lesson { Category = LessonCategory.Other }, Throws.Nothing);
        });
    }
}
