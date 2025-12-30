namespace WeeklyReviewSystem.Core.Tests;

public class WeeklyReviewTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesWeeklyReview()
    {
        // Arrange
        var reviewId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var weekStartDate = new DateTime(2024, 1, 15);
        var weekEndDate = new DateTime(2024, 1, 21);
        var overallRating = 8;
        var reflections = "Great week overall";
        var lessonsLearned = "Time management is key";
        var gratitude = "Thankful for team support";
        var improvementAreas = "Need better work-life balance";

        // Act
        var review = new WeeklyReview
        {
            WeeklyReviewId = reviewId,
            UserId = userId,
            WeekStartDate = weekStartDate,
            WeekEndDate = weekEndDate,
            OverallRating = overallRating,
            Reflections = reflections,
            LessonsLearned = lessonsLearned,
            Gratitude = gratitude,
            ImprovementAreas = improvementAreas
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(review.WeeklyReviewId, Is.EqualTo(reviewId));
            Assert.That(review.UserId, Is.EqualTo(userId));
            Assert.That(review.WeekStartDate, Is.EqualTo(weekStartDate));
            Assert.That(review.WeekEndDate, Is.EqualTo(weekEndDate));
            Assert.That(review.OverallRating, Is.EqualTo(overallRating));
            Assert.That(review.Reflections, Is.EqualTo(reflections));
            Assert.That(review.LessonsLearned, Is.EqualTo(lessonsLearned));
            Assert.That(review.Gratitude, Is.EqualTo(gratitude));
            Assert.That(review.ImprovementAreas, Is.EqualTo(improvementAreas));
            Assert.That(review.IsCompleted, Is.False);
            Assert.That(review.Accomplishments, Is.Not.Null);
            Assert.That(review.Challenges, Is.Not.Null);
            Assert.That(review.Priorities, Is.Not.Null);
        });
    }

    [Test]
    public void CompleteReview_SetsIsCompletedToTrue()
    {
        // Arrange
        var review = new WeeklyReview
        {
            WeeklyReviewId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WeekStartDate = new DateTime(2024, 1, 15),
            WeekEndDate = new DateTime(2024, 1, 21),
            IsCompleted = false
        };

        // Act
        review.CompleteReview();

        // Assert
        Assert.That(review.IsCompleted, Is.True);
    }

    [Test]
    public void GetWeekNumber_ValidDate_ReturnsCorrectWeekNumber()
    {
        // Arrange
        var review = new WeeklyReview
        {
            WeeklyReviewId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WeekStartDate = new DateTime(2024, 1, 15),
            WeekEndDate = new DateTime(2024, 1, 21)
        };

        // Act
        var weekNumber = review.GetWeekNumber();

        // Assert
        var expectedWeekNumber = System.Globalization.ISOWeek.GetWeekOfYear(new DateTime(2024, 1, 15));
        Assert.That(weekNumber, Is.EqualTo(expectedWeekNumber));
    }

    [Test]
    public void GetWeekNumber_FirstWeekOfYear_ReturnsOne()
    {
        // Arrange
        var review = new WeeklyReview
        {
            WeeklyReviewId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WeekStartDate = new DateTime(2024, 1, 1),
            WeekEndDate = new DateTime(2024, 1, 7)
        };

        // Act
        var weekNumber = review.GetWeekNumber();

        // Assert
        Assert.That(weekNumber, Is.GreaterThanOrEqualTo(1));
    }

    [Test]
    public void OverallRating_ValidRange_CanBeSet()
    {
        // Arrange & Act
        var review = new WeeklyReview
        {
            WeeklyReviewId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            OverallRating = 7
        };

        // Assert
        Assert.That(review.OverallRating, Is.EqualTo(7));
    }

    [Test]
    public void OverallRating_NullValue_IsAllowed()
    {
        // Arrange & Act
        var review = new WeeklyReview
        {
            WeeklyReviewId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            OverallRating = null
        };

        // Assert
        Assert.That(review.OverallRating, Is.Null);
    }

    [Test]
    public void Accomplishments_Collection_CanBePopulated()
    {
        // Arrange
        var review = new WeeklyReview
        {
            WeeklyReviewId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WeekStartDate = new DateTime(2024, 1, 15),
            WeekEndDate = new DateTime(2024, 1, 21)
        };

        var accomplishment = new Accomplishment
        {
            AccomplishmentId = Guid.NewGuid(),
            WeeklyReviewId = review.WeeklyReviewId,
            Title = "Completed project"
        };

        // Act
        review.Accomplishments.Add(accomplishment);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(review.Accomplishments, Has.Count.EqualTo(1));
            Assert.That(review.Accomplishments.First().Title, Is.EqualTo("Completed project"));
        });
    }

    [Test]
    public void Challenges_Collection_CanBePopulated()
    {
        // Arrange
        var review = new WeeklyReview
        {
            WeeklyReviewId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WeekStartDate = new DateTime(2024, 1, 15),
            WeekEndDate = new DateTime(2024, 1, 21)
        };

        var challenge = new Challenge
        {
            ChallengeId = Guid.NewGuid(),
            WeeklyReviewId = review.WeeklyReviewId,
            Title = "Technical issue"
        };

        // Act
        review.Challenges.Add(challenge);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(review.Challenges, Has.Count.EqualTo(1));
            Assert.That(review.Challenges.First().Title, Is.EqualTo("Technical issue"));
        });
    }

    [Test]
    public void Priorities_Collection_CanBePopulated()
    {
        // Arrange
        var review = new WeeklyReview
        {
            WeeklyReviewId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WeekStartDate = new DateTime(2024, 1, 15),
            WeekEndDate = new DateTime(2024, 1, 21)
        };

        var priority = new WeeklyPriority
        {
            WeeklyPriorityId = Guid.NewGuid(),
            WeeklyReviewId = review.WeeklyReviewId,
            Title = "Complete documentation",
            Level = PriorityLevel.High
        };

        // Act
        review.Priorities.Add(priority);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(review.Priorities, Has.Count.EqualTo(1));
            Assert.That(review.Priorities.First().Title, Is.EqualTo("Complete documentation"));
        });
    }

    [Test]
    public void CreatedAt_DefaultValue_IsSetToUtcNow()
    {
        // Arrange & Act
        var review = new WeeklyReview
        {
            WeeklyReviewId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        // Assert
        Assert.That(review.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }
}
