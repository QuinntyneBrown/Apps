namespace WeeklyReviewSystem.Core.Tests;

public class WeeklyPriorityTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesWeeklyPriority()
    {
        // Arrange
        var priorityId = Guid.NewGuid();
        var reviewId = Guid.NewGuid();
        var title = "Complete documentation";
        var description = "Finish API documentation for v2.0";
        var level = PriorityLevel.High;
        var category = "Work";
        var targetDate = new DateTime(2024, 1, 31);

        // Act
        var priority = new WeeklyPriority
        {
            WeeklyPriorityId = priorityId,
            WeeklyReviewId = reviewId,
            Title = title,
            Description = description,
            Level = level,
            Category = category,
            TargetDate = targetDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(priority.WeeklyPriorityId, Is.EqualTo(priorityId));
            Assert.That(priority.WeeklyReviewId, Is.EqualTo(reviewId));
            Assert.That(priority.Title, Is.EqualTo(title));
            Assert.That(priority.Description, Is.EqualTo(description));
            Assert.That(priority.Level, Is.EqualTo(level));
            Assert.That(priority.Category, Is.EqualTo(category));
            Assert.That(priority.TargetDate, Is.EqualTo(targetDate));
            Assert.That(priority.IsCompleted, Is.False);
        });
    }

    [Test]
    public void Title_DefaultValue_IsEmptyString()
    {
        // Arrange & Act
        var priority = new WeeklyPriority();

        // Assert
        Assert.That(priority.Title, Is.EqualTo(string.Empty));
    }

    [Test]
    public void IsCompleted_DefaultValue_IsFalse()
    {
        // Arrange & Act
        var priority = new WeeklyPriority
        {
            WeeklyPriorityId = Guid.NewGuid(),
            WeeklyReviewId = Guid.NewGuid(),
            Title = "Test priority"
        };

        // Assert
        Assert.That(priority.IsCompleted, Is.False);
    }

    [Test]
    public void Complete_SetsIsCompletedToTrue()
    {
        // Arrange
        var priority = new WeeklyPriority
        {
            WeeklyPriorityId = Guid.NewGuid(),
            WeeklyReviewId = Guid.NewGuid(),
            Title = "Finish task",
            IsCompleted = false
        };

        // Act
        priority.Complete();

        // Assert
        Assert.That(priority.IsCompleted, Is.True);
    }

    [Test]
    public void Description_NullValue_IsAllowed()
    {
        // Arrange & Act
        var priority = new WeeklyPriority
        {
            WeeklyPriorityId = Guid.NewGuid(),
            WeeklyReviewId = Guid.NewGuid(),
            Title = "Test",
            Description = null
        };

        // Assert
        Assert.That(priority.Description, Is.Null);
    }

    [Test]
    public void Category_NullValue_IsAllowed()
    {
        // Arrange & Act
        var priority = new WeeklyPriority
        {
            WeeklyPriorityId = Guid.NewGuid(),
            WeeklyReviewId = Guid.NewGuid(),
            Title = "Test",
            Category = null
        };

        // Assert
        Assert.That(priority.Category, Is.Null);
    }

    [Test]
    public void TargetDate_NullValue_IsAllowed()
    {
        // Arrange & Act
        var priority = new WeeklyPriority
        {
            WeeklyPriorityId = Guid.NewGuid(),
            WeeklyReviewId = Guid.NewGuid(),
            Title = "Test",
            TargetDate = null
        };

        // Assert
        Assert.That(priority.TargetDate, Is.Null);
    }

    [Test]
    public void CreatedAt_DefaultValue_IsSetToUtcNow()
    {
        // Arrange & Act
        var priority = new WeeklyPriority
        {
            WeeklyPriorityId = Guid.NewGuid(),
            WeeklyReviewId = Guid.NewGuid(),
            Title = "Test"
        };

        // Assert
        Assert.That(priority.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void PriorityLevel_AllValues_CanBeAssigned()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            var low = new WeeklyPriority { Level = PriorityLevel.Low };
            Assert.That(low.Level, Is.EqualTo(PriorityLevel.Low));

            var medium = new WeeklyPriority { Level = PriorityLevel.Medium };
            Assert.That(medium.Level, Is.EqualTo(PriorityLevel.Medium));

            var high = new WeeklyPriority { Level = PriorityLevel.High };
            Assert.That(high.Level, Is.EqualTo(PriorityLevel.High));

            var critical = new WeeklyPriority { Level = PriorityLevel.Critical };
            Assert.That(critical.Level, Is.EqualTo(PriorityLevel.Critical));
        });
    }

    [Test]
    public void Priority_DifferentCategories_CanBeAssigned()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            var work = new WeeklyPriority { Category = "Work" };
            Assert.That(work.Category, Is.EqualTo("Work"));

            var personal = new WeeklyPriority { Category = "Personal" };
            Assert.That(personal.Category, Is.EqualTo("Personal"));

            var health = new WeeklyPriority { Category = "Health" };
            Assert.That(health.Category, Is.EqualTo("Health"));

            var learning = new WeeklyPriority { Category = "Learning" };
            Assert.That(learning.Category, Is.EqualTo("Learning"));
        });
    }

    [Test]
    public void Priority_WithTargetDate_CanBeSet()
    {
        // Arrange
        var targetDate = new DateTime(2024, 2, 15);

        // Act
        var priority = new WeeklyPriority
        {
            WeeklyPriorityId = Guid.NewGuid(),
            WeeklyReviewId = Guid.NewGuid(),
            Title = "Important task",
            TargetDate = targetDate
        };

        // Assert
        Assert.That(priority.TargetDate, Is.EqualTo(targetDate));
    }

    [Test]
    public void Priority_CriticalLevel_IsHighestPriority()
    {
        // Arrange & Act
        var priority = new WeeklyPriority
        {
            WeeklyPriorityId = Guid.NewGuid(),
            WeeklyReviewId = Guid.NewGuid(),
            Title = "Urgent fix",
            Level = PriorityLevel.Critical
        };

        // Assert
        Assert.That(priority.Level, Is.EqualTo(PriorityLevel.Critical));
        Assert.That((int)priority.Level, Is.GreaterThan((int)PriorityLevel.High));
    }
}
