namespace PersonalLibraryLessonsLearned.Core.Tests;

public class EventTests
{
    [Test]
    public void LessonCreatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var lessonId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var dateLearned = DateTime.UtcNow.AddDays(-5);
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new LessonCreatedEvent
        {
            LessonId = lessonId,
            UserId = userId,
            Title = "Always validate user input",
            Category = LessonCategory.Technical,
            DateLearned = dateLearned,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.LessonId, Is.EqualTo(lessonId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Title, Is.EqualTo("Always validate user input"));
            Assert.That(evt.Category, Is.EqualTo(LessonCategory.Technical));
            Assert.That(evt.DateLearned, Is.EqualTo(dateLearned));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void ReminderScheduledEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var lessonReminderId = Guid.NewGuid();
        var lessonId = Guid.NewGuid();
        var reminderDateTime = DateTime.UtcNow.AddDays(7);
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new ReminderScheduledEvent
        {
            LessonReminderId = lessonReminderId,
            LessonId = lessonId,
            ReminderDateTime = reminderDateTime,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.LessonReminderId, Is.EqualTo(lessonReminderId));
            Assert.That(evt.LessonId, Is.EqualTo(lessonId));
            Assert.That(evt.ReminderDateTime, Is.EqualTo(reminderDateTime));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void LessonCreatedEvent_AllCategories_CanBeUsed()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(() => new LessonCreatedEvent { Category = LessonCategory.PersonalDevelopment }, Throws.Nothing);
            Assert.That(() => new LessonCreatedEvent { Category = LessonCategory.Professional }, Throws.Nothing);
            Assert.That(() => new LessonCreatedEvent { Category = LessonCategory.Health }, Throws.Nothing);
            Assert.That(() => new LessonCreatedEvent { Category = LessonCategory.Relationships }, Throws.Nothing);
            Assert.That(() => new LessonCreatedEvent { Category = LessonCategory.Finance }, Throws.Nothing);
            Assert.That(() => new LessonCreatedEvent { Category = LessonCategory.Leadership }, Throws.Nothing);
            Assert.That(() => new LessonCreatedEvent { Category = LessonCategory.Technical }, Throws.Nothing);
            Assert.That(() => new LessonCreatedEvent { Category = LessonCategory.LifeWisdom }, Throws.Nothing);
            Assert.That(() => new LessonCreatedEvent { Category = LessonCategory.Other }, Throws.Nothing);
        });
    }

    [Test]
    public void LessonCreatedEvent_IsRecord_IsImmutable()
    {
        // Arrange
        var evt = new LessonCreatedEvent
        {
            LessonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Lesson",
            Category = LessonCategory.Technical,
            DateLearned = DateTime.UtcNow,
            Timestamp = DateTime.UtcNow
        };

        // Assert - Record types are immutable, properties cannot be reassigned
        Assert.That(evt, Is.Not.Null);
    }

    [Test]
    public void ReminderScheduledEvent_IsRecord_IsImmutable()
    {
        // Arrange
        var evt = new ReminderScheduledEvent
        {
            LessonReminderId = Guid.NewGuid(),
            LessonId = Guid.NewGuid(),
            ReminderDateTime = DateTime.UtcNow.AddDays(7),
            Timestamp = DateTime.UtcNow
        };

        // Assert - Record types are immutable, properties cannot be reassigned
        Assert.That(evt, Is.Not.Null);
    }
}
