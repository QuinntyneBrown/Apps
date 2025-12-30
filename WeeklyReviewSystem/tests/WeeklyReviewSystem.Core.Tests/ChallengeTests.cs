namespace WeeklyReviewSystem.Core.Tests;

public class ChallengeTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesChallenge()
    {
        // Arrange
        var challengeId = Guid.NewGuid();
        var reviewId = Guid.NewGuid();
        var title = "Technical difficulty";
        var description = "Database performance issues";
        var resolution = "Optimized queries and added indexes";
        var lessonsLearned = "Regular performance monitoring is crucial";
        var isResolved = true;

        // Act
        var challenge = new Challenge
        {
            ChallengeId = challengeId,
            WeeklyReviewId = reviewId,
            Title = title,
            Description = description,
            Resolution = resolution,
            LessonsLearned = lessonsLearned,
            IsResolved = isResolved
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(challenge.ChallengeId, Is.EqualTo(challengeId));
            Assert.That(challenge.WeeklyReviewId, Is.EqualTo(reviewId));
            Assert.That(challenge.Title, Is.EqualTo(title));
            Assert.That(challenge.Description, Is.EqualTo(description));
            Assert.That(challenge.Resolution, Is.EqualTo(resolution));
            Assert.That(challenge.LessonsLearned, Is.EqualTo(lessonsLearned));
            Assert.That(challenge.IsResolved, Is.EqualTo(isResolved));
        });
    }

    [Test]
    public void Title_DefaultValue_IsEmptyString()
    {
        // Arrange & Act
        var challenge = new Challenge();

        // Assert
        Assert.That(challenge.Title, Is.EqualTo(string.Empty));
    }

    [Test]
    public void IsResolved_DefaultValue_IsFalse()
    {
        // Arrange & Act
        var challenge = new Challenge
        {
            ChallengeId = Guid.NewGuid(),
            WeeklyReviewId = Guid.NewGuid(),
            Title = "Test challenge"
        };

        // Assert
        Assert.That(challenge.IsResolved, Is.False);
    }

    [Test]
    public void Resolve_ValidResolution_UpdatesStatusAndResolution()
    {
        // Arrange
        var challenge = new Challenge
        {
            ChallengeId = Guid.NewGuid(),
            WeeklyReviewId = Guid.NewGuid(),
            Title = "Team conflict",
            IsResolved = false
        };
        var resolution = "Had a team meeting and clarified expectations";

        // Act
        challenge.Resolve(resolution);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(challenge.IsResolved, Is.True);
            Assert.That(challenge.Resolution, Is.EqualTo(resolution));
        });
    }

    [Test]
    public void Resolve_EmptyResolution_StillMarksAsResolved()
    {
        // Arrange
        var challenge = new Challenge
        {
            ChallengeId = Guid.NewGuid(),
            WeeklyReviewId = Guid.NewGuid(),
            Title = "Test challenge",
            IsResolved = false
        };
        var resolution = "";

        // Act
        challenge.Resolve(resolution);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(challenge.IsResolved, Is.True);
            Assert.That(challenge.Resolution, Is.EqualTo(resolution));
        });
    }

    [Test]
    public void Description_NullValue_IsAllowed()
    {
        // Arrange & Act
        var challenge = new Challenge
        {
            ChallengeId = Guid.NewGuid(),
            WeeklyReviewId = Guid.NewGuid(),
            Title = "Test",
            Description = null
        };

        // Assert
        Assert.That(challenge.Description, Is.Null);
    }

    [Test]
    public void LessonsLearned_NullValue_IsAllowed()
    {
        // Arrange & Act
        var challenge = new Challenge
        {
            ChallengeId = Guid.NewGuid(),
            WeeklyReviewId = Guid.NewGuid(),
            Title = "Test",
            LessonsLearned = null
        };

        // Assert
        Assert.That(challenge.LessonsLearned, Is.Null);
    }

    [Test]
    public void Resolution_NullValue_IsAllowed()
    {
        // Arrange & Act
        var challenge = new Challenge
        {
            ChallengeId = Guid.NewGuid(),
            WeeklyReviewId = Guid.NewGuid(),
            Title = "Test",
            Resolution = null
        };

        // Assert
        Assert.That(challenge.Resolution, Is.Null);
    }

    [Test]
    public void CreatedAt_DefaultValue_IsSetToUtcNow()
    {
        // Arrange & Act
        var challenge = new Challenge
        {
            ChallengeId = Guid.NewGuid(),
            WeeklyReviewId = Guid.NewGuid(),
            Title = "Test"
        };

        // Assert
        Assert.That(challenge.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void Challenge_Unresolved_CanHaveDescription()
    {
        // Arrange & Act
        var challenge = new Challenge
        {
            ChallengeId = Guid.NewGuid(),
            WeeklyReviewId = Guid.NewGuid(),
            Title = "Open issue",
            Description = "Still investigating the root cause",
            IsResolved = false
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(challenge.IsResolved, Is.False);
            Assert.That(challenge.Description, Is.Not.Null);
            Assert.That(challenge.Resolution, Is.Null);
        });
    }

    [Test]
    public void Challenge_Resolved_CanHaveLessonsLearned()
    {
        // Arrange
        var challenge = new Challenge
        {
            ChallengeId = Guid.NewGuid(),
            WeeklyReviewId = Guid.NewGuid(),
            Title = "Performance issue"
        };

        // Act
        challenge.Resolve("Implemented caching");
        challenge.LessonsLearned = "Always consider caching for frequently accessed data";

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(challenge.IsResolved, Is.True);
            Assert.That(challenge.Resolution, Is.Not.Null);
            Assert.That(challenge.LessonsLearned, Is.Not.Null);
        });
    }
}
