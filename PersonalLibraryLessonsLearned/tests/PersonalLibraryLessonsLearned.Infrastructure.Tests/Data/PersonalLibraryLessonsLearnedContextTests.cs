// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalLibraryLessonsLearned.Infrastructure.Tests;

/// <summary>
/// Unit tests for the PersonalLibraryLessonsLearnedContext.
/// </summary>
[TestFixture]
public class PersonalLibraryLessonsLearnedContextTests
{
    private PersonalLibraryLessonsLearnedContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<PersonalLibraryLessonsLearnedContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new PersonalLibraryLessonsLearnedContext(options);
    }

    /// <summary>
    /// Tears down the test context.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    /// <summary>
    /// Tests that Lessons can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Lessons_CanAddAndRetrieve()
    {
        // Arrange
        var lesson = new Lesson
        {
            LessonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Lesson",
            Content = "This is a test lesson content",
            Category = LessonCategory.Technical,
            Tags = "test, learning",
            DateLearned = DateTime.UtcNow,
            Application = "Apply in daily work",
            IsApplied = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Lessons.Add(lesson);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Lessons.FindAsync(lesson.LessonId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Test Lesson"));
        Assert.That(retrieved.Category, Is.EqualTo(LessonCategory.Technical));
        Assert.That(retrieved.IsApplied, Is.False);
    }

    /// <summary>
    /// Tests that Sources can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Sources_CanAddAndRetrieve()
    {
        // Arrange
        var source = new Source
        {
            SourceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Clean Code",
            Author = "Robert C. Martin",
            SourceType = "Book",
            Url = "https://example.com/clean-code",
            DateConsumed = DateTime.UtcNow.AddDays(-30),
            Notes = "Excellent book on software craftsmanship",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Sources.Add(source);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Sources.FindAsync(source.SourceId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Clean Code"));
        Assert.That(retrieved.Author, Is.EqualTo("Robert C. Martin"));
        Assert.That(retrieved.SourceType, Is.EqualTo("Book"));
    }

    /// <summary>
    /// Tests that Reminders can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Reminders_CanAddAndRetrieve()
    {
        // Arrange
        var lesson = new Lesson
        {
            LessonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Lesson",
            Content = "Test content",
            Category = LessonCategory.Personal,
            DateLearned = DateTime.UtcNow,
            IsApplied = false,
            CreatedAt = DateTime.UtcNow,
        };

        var reminder = new LessonReminder
        {
            LessonReminderId = Guid.NewGuid(),
            LessonId = lesson.LessonId,
            UserId = lesson.UserId,
            ReminderDateTime = DateTime.UtcNow.AddDays(7),
            Message = "Review this lesson",
            IsSent = false,
            IsDismissed = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Lessons.Add(lesson);
        _context.Reminders.Add(reminder);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Reminders.FindAsync(reminder.LessonReminderId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.LessonId, Is.EqualTo(lesson.LessonId));
        Assert.That(retrieved.Message, Is.EqualTo("Review this lesson"));
        Assert.That(retrieved.IsSent, Is.False);
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedEntities()
    {
        // Arrange
        var lesson = new Lesson
        {
            LessonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Lesson",
            Content = "Test content",
            Category = LessonCategory.Technical,
            DateLearned = DateTime.UtcNow,
            IsApplied = false,
            CreatedAt = DateTime.UtcNow,
        };

        var reminder = new LessonReminder
        {
            LessonReminderId = Guid.NewGuid(),
            LessonId = lesson.LessonId,
            UserId = lesson.UserId,
            ReminderDateTime = DateTime.UtcNow.AddDays(7),
            IsSent = false,
            IsDismissed = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Lessons.Add(lesson);
        _context.Reminders.Add(reminder);
        await _context.SaveChangesAsync();

        // Act
        _context.Lessons.Remove(lesson);
        await _context.SaveChangesAsync();

        var retrievedReminder = await _context.Reminders.FindAsync(reminder.LessonReminderId);

        // Assert
        Assert.That(retrievedReminder, Is.Null);
    }
}
