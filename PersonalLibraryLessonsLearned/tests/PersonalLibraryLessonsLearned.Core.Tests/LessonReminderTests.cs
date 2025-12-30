namespace PersonalLibraryLessonsLearned.Core.Tests;

public class LessonReminderTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesLessonReminder()
    {
        // Arrange
        var lessonReminderId = Guid.NewGuid();
        var lessonId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var reminderDateTime = DateTime.UtcNow.AddDays(7);

        // Act
        var reminder = new LessonReminder
        {
            LessonReminderId = lessonReminderId,
            LessonId = lessonId,
            UserId = userId,
            ReminderDateTime = reminderDateTime,
            Message = "Review this lesson",
            IsSent = false,
            IsDismissed = false
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(reminder.LessonReminderId, Is.EqualTo(lessonReminderId));
            Assert.That(reminder.LessonId, Is.EqualTo(lessonId));
            Assert.That(reminder.UserId, Is.EqualTo(userId));
            Assert.That(reminder.ReminderDateTime, Is.EqualTo(reminderDateTime));
            Assert.That(reminder.Message, Is.EqualTo("Review this lesson"));
            Assert.That(reminder.IsSent, Is.False);
            Assert.That(reminder.IsDismissed, Is.False);
            Assert.That(reminder.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void MarkAsSent_SetsIsSentToTrue()
    {
        // Arrange
        var reminder = new LessonReminder
        {
            LessonReminderId = Guid.NewGuid(),
            LessonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ReminderDateTime = DateTime.UtcNow.AddDays(7),
            IsSent = false
        };

        // Act
        reminder.MarkAsSent();

        // Assert
        Assert.That(reminder.IsSent, Is.True);
    }

    [Test]
    public void Dismiss_SetsIsDismissedToTrue()
    {
        // Arrange
        var reminder = new LessonReminder
        {
            LessonReminderId = Guid.NewGuid(),
            LessonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ReminderDateTime = DateTime.UtcNow.AddDays(7),
            IsDismissed = false
        };

        // Act
        reminder.Dismiss();

        // Assert
        Assert.That(reminder.IsDismissed, Is.True);
    }

    [Test]
    public void IsDue_NotSentNotDismissedAndPastDue_ReturnsTrue()
    {
        // Arrange
        var reminder = new LessonReminder
        {
            LessonReminderId = Guid.NewGuid(),
            LessonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ReminderDateTime = DateTime.UtcNow.AddHours(-1),
            IsSent = false,
            IsDismissed = false
        };

        // Act
        var result = reminder.IsDue();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsDue_AlreadySent_ReturnsFalse()
    {
        // Arrange
        var reminder = new LessonReminder
        {
            LessonReminderId = Guid.NewGuid(),
            LessonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ReminderDateTime = DateTime.UtcNow.AddHours(-1),
            IsSent = true,
            IsDismissed = false
        };

        // Act
        var result = reminder.IsDue();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsDue_AlreadyDismissed_ReturnsFalse()
    {
        // Arrange
        var reminder = new LessonReminder
        {
            LessonReminderId = Guid.NewGuid(),
            LessonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ReminderDateTime = DateTime.UtcNow.AddHours(-1),
            IsSent = false,
            IsDismissed = true
        };

        // Act
        var result = reminder.IsDue();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsDue_NotYetDue_ReturnsFalse()
    {
        // Arrange
        var reminder = new LessonReminder
        {
            LessonReminderId = Guid.NewGuid(),
            LessonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ReminderDateTime = DateTime.UtcNow.AddDays(1),
            IsSent = false,
            IsDismissed = false
        };

        // Act
        var result = reminder.IsDue();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsDue_ExactlyAtReminderTime_ReturnsTrue()
    {
        // Arrange
        var reminder = new LessonReminder
        {
            LessonReminderId = Guid.NewGuid(),
            LessonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ReminderDateTime = DateTime.UtcNow,
            IsSent = false,
            IsDismissed = false
        };

        // Act
        var result = reminder.IsDue();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsDue_BothSentAndDismissed_ReturnsFalse()
    {
        // Arrange
        var reminder = new LessonReminder
        {
            LessonReminderId = Guid.NewGuid(),
            LessonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ReminderDateTime = DateTime.UtcNow.AddHours(-1),
            IsSent = true,
            IsDismissed = true
        };

        // Act
        var result = reminder.IsDue();

        // Assert
        Assert.That(result, Is.False);
    }
}
