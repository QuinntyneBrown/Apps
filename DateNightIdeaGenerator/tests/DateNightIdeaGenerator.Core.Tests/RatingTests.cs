namespace DateNightIdeaGenerator.Core.Tests;

public class RatingTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesRating()
    {
        // Arrange
        var ratingId = Guid.NewGuid();
        var dateIdeaId = Guid.NewGuid();
        var experienceId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var score = 5;
        var review = "Absolutely wonderful!";

        // Act
        var rating = new Rating
        {
            RatingId = ratingId,
            DateIdeaId = dateIdeaId,
            ExperienceId = experienceId,
            UserId = userId,
            Score = score,
            Review = review
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(rating.RatingId, Is.EqualTo(ratingId));
            Assert.That(rating.DateIdeaId, Is.EqualTo(dateIdeaId));
            Assert.That(rating.ExperienceId, Is.EqualTo(experienceId));
            Assert.That(rating.UserId, Is.EqualTo(userId));
            Assert.That(rating.Score, Is.EqualTo(score));
            Assert.That(rating.Review, Is.EqualTo(review));
        });
    }

    [Test]
    public void DefaultValues_AreSetCorrectly()
    {
        // Arrange & Act
        var rating = new Rating();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(rating.Score, Is.EqualTo(0));
            Assert.That(rating.Review, Is.Null);
            Assert.That(rating.DateIdeaId, Is.Null);
            Assert.That(rating.ExperienceId, Is.Null);
            Assert.That(rating.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(rating.UpdatedAt, Is.Null);
        });
    }

    [Test]
    public void IsValidScore_WithScore1_ReturnsTrue()
    {
        // Arrange
        var rating = new Rating { Score = 1 };

        // Act
        var isValid = rating.IsValidScore();

        // Assert
        Assert.That(isValid, Is.True);
    }

    [Test]
    public void IsValidScore_WithScore5_ReturnsTrue()
    {
        // Arrange
        var rating = new Rating { Score = 5 };

        // Act
        var isValid = rating.IsValidScore();

        // Assert
        Assert.That(isValid, Is.True);
    }

    [Test]
    public void IsValidScore_WithScore3_ReturnsTrue()
    {
        // Arrange
        var rating = new Rating { Score = 3 };

        // Act
        var isValid = rating.IsValidScore();

        // Assert
        Assert.That(isValid, Is.True);
    }

    [Test]
    public void IsValidScore_WithScore0_ReturnsFalse()
    {
        // Arrange
        var rating = new Rating { Score = 0 };

        // Act
        var isValid = rating.IsValidScore();

        // Assert
        Assert.That(isValid, Is.False);
    }

    [Test]
    public void IsValidScore_WithScore6_ReturnsFalse()
    {
        // Arrange
        var rating = new Rating { Score = 6 };

        // Act
        var isValid = rating.IsValidScore();

        // Assert
        Assert.That(isValid, Is.False);
    }

    [Test]
    public void IsValidScore_WithNegativeScore_ReturnsFalse()
    {
        // Arrange
        var rating = new Rating { Score = -1 };

        // Act
        var isValid = rating.IsValidScore();

        // Assert
        Assert.That(isValid, Is.False);
    }

    [Test]
    public void UpdateScore_WithValidScore_UpdatesScore()
    {
        // Arrange
        var rating = new Rating
        {
            RatingId = Guid.NewGuid(),
            Score = 3
        };
        var beforeUpdate = DateTime.UtcNow.AddSeconds(-1);

        // Act
        rating.UpdateScore(5);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(rating.Score, Is.EqualTo(5));
            Assert.That(rating.UpdatedAt, Is.Not.Null);
            Assert.That(rating.UpdatedAt!.Value, Is.GreaterThan(beforeUpdate));
        });
    }

    [Test]
    public void UpdateScore_WithInvalidScore_DoesNotUpdate()
    {
        // Arrange
        var rating = new Rating
        {
            RatingId = Guid.NewGuid(),
            Score = 3
        };

        // Act
        rating.UpdateScore(6);

        // Assert
        Assert.That(rating.Score, Is.EqualTo(3));
    }

    [Test]
    public void UpdateScore_WithZeroScore_DoesNotUpdate()
    {
        // Arrange
        var rating = new Rating
        {
            RatingId = Guid.NewGuid(),
            Score = 4
        };

        // Act
        rating.UpdateScore(0);

        // Assert
        Assert.That(rating.Score, Is.EqualTo(4));
    }

    [Test]
    public void UpdateScore_WithNegativeScore_DoesNotUpdate()
    {
        // Arrange
        var rating = new Rating
        {
            RatingId = Guid.NewGuid(),
            Score = 4
        };

        // Act
        rating.UpdateScore(-1);

        // Assert
        Assert.That(rating.Score, Is.EqualTo(4));
    }

    [Test]
    public void UpdateScore_WithValidScore1_Updates()
    {
        // Arrange
        var rating = new Rating { Score = 5 };

        // Act
        rating.UpdateScore(1);

        // Assert
        Assert.That(rating.Score, Is.EqualTo(1));
    }

    [Test]
    public void UpdateScore_InvalidScore_DoesNotUpdateTimestamp()
    {
        // Arrange
        var rating = new Rating
        {
            RatingId = Guid.NewGuid(),
            Score = 3
        };

        // Act
        rating.UpdateScore(10);

        // Assert
        Assert.That(rating.UpdatedAt, Is.Null);
    }

    [Test]
    public void Review_CanBeSet()
    {
        // Arrange
        var review = "Great experience, would recommend!";
        var rating = new Rating();

        // Act
        rating.Review = review;

        // Assert
        Assert.That(rating.Review, Is.EqualTo(review));
    }

    [Test]
    public void DateIdea_NavigationProperty_CanBeSet()
    {
        // Arrange
        var dateIdea = new DateIdea { DateIdeaId = Guid.NewGuid() };
        var rating = new Rating
        {
            DateIdeaId = dateIdea.DateIdeaId
        };

        // Act
        rating.DateIdea = dateIdea;

        // Assert
        Assert.That(rating.DateIdea, Is.EqualTo(dateIdea));
    }

    [Test]
    public void Experience_NavigationProperty_CanBeSet()
    {
        // Arrange
        var experience = new Experience { ExperienceId = Guid.NewGuid() };
        var rating = new Rating
        {
            ExperienceId = experience.ExperienceId
        };

        // Act
        rating.Experience = experience;

        // Assert
        Assert.That(rating.Experience, Is.EqualTo(experience));
    }

    [Test]
    public void DateIdeaId_CanBeNull()
    {
        // Arrange & Act
        var rating = new Rating
        {
            DateIdeaId = null
        };

        // Assert
        Assert.That(rating.DateIdeaId, Is.Null);
    }

    [Test]
    public void ExperienceId_CanBeNull()
    {
        // Arrange & Act
        var rating = new Rating
        {
            ExperienceId = null
        };

        // Assert
        Assert.That(rating.ExperienceId, Is.Null);
    }
}
