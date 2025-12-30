namespace PersonalLibraryLessonsLearned.Core.Tests;

public class SourceTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesSource()
    {
        // Arrange
        var sourceId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var dateConsumed = DateTime.UtcNow.AddDays(-10);

        // Act
        var source = new Source
        {
            SourceId = sourceId,
            UserId = userId,
            Title = "Clean Code",
            Author = "Robert C. Martin",
            SourceType = "Book",
            Url = "https://example.com/clean-code",
            DateConsumed = dateConsumed,
            Notes = "Excellent book on software craftsmanship"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(source.SourceId, Is.EqualTo(sourceId));
            Assert.That(source.UserId, Is.EqualTo(userId));
            Assert.That(source.Title, Is.EqualTo("Clean Code"));
            Assert.That(source.Author, Is.EqualTo("Robert C. Martin"));
            Assert.That(source.SourceType, Is.EqualTo("Book"));
            Assert.That(source.Url, Is.EqualTo("https://example.com/clean-code"));
            Assert.That(source.DateConsumed, Is.EqualTo(dateConsumed));
            Assert.That(source.Notes, Is.EqualTo("Excellent book on software craftsmanship"));
            Assert.That(source.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void GetLessonCount_NoLessons_ReturnsZero()
    {
        // Arrange
        var source = new Source
        {
            SourceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Source",
            SourceType = "Book",
            Lessons = new List<Lesson>()
        };

        // Act
        var count = source.GetLessonCount();

        // Assert
        Assert.That(count, Is.EqualTo(0));
    }

    [Test]
    public void GetLessonCount_WithOnLesson_ReturnsOne()
    {
        // Arrange
        var source = new Source
        {
            SourceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Source",
            SourceType = "Book",
            Lessons = new List<Lesson>
            {
                new Lesson { LessonId = Guid.NewGuid(), Title = "Lesson 1", Content = "Content 1" }
            }
        };

        // Act
        var count = source.GetLessonCount();

        // Assert
        Assert.That(count, Is.EqualTo(1));
    }

    [Test]
    public void GetLessonCount_WithMultipleLessons_ReturnsCorrectCount()
    {
        // Arrange
        var source = new Source
        {
            SourceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Source",
            SourceType = "Book",
            Lessons = new List<Lesson>
            {
                new Lesson { LessonId = Guid.NewGuid(), Title = "Lesson 1", Content = "Content 1" },
                new Lesson { LessonId = Guid.NewGuid(), Title = "Lesson 2", Content = "Content 2" },
                new Lesson { LessonId = Guid.NewGuid(), Title = "Lesson 3", Content = "Content 3" }
            }
        };

        // Act
        var count = source.GetLessonCount();

        // Assert
        Assert.That(count, Is.EqualTo(3));
    }

    [Test]
    public void Source_DifferentSourceTypes_CanBeCreated()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(() => new Source { SourceType = "Book" }, Throws.Nothing);
            Assert.That(() => new Source { SourceType = "Article" }, Throws.Nothing);
            Assert.That(() => new Source { SourceType = "Video" }, Throws.Nothing);
            Assert.That(() => new Source { SourceType = "Podcast" }, Throws.Nothing);
            Assert.That(() => new Source { SourceType = "Course" }, Throws.Nothing);
            Assert.That(() => new Source { SourceType = "Blog" }, Throws.Nothing);
        });
    }

    [Test]
    public void Source_WithoutAuthor_CanBeCreated()
    {
        // Arrange & Act
        var source = new Source
        {
            SourceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Anonymous Article",
            SourceType = "Article",
            Author = null
        };

        // Assert
        Assert.That(source.Author, Is.Null);
    }

    [Test]
    public void Source_WithoutUrl_CanBeCreated()
    {
        // Arrange & Act
        var source = new Source
        {
            SourceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Physical Book",
            SourceType = "Book",
            Url = null
        };

        // Assert
        Assert.That(source.Url, Is.Null);
    }

    [Test]
    public void Source_WithoutDateConsumed_CanBeCreated()
    {
        // Arrange & Act
        var source = new Source
        {
            SourceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Future Reading",
            SourceType = "Book",
            DateConsumed = null
        };

        // Assert
        Assert.That(source.DateConsumed, Is.Null);
    }
}
