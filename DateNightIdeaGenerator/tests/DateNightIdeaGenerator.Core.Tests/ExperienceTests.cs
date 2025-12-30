namespace DateNightIdeaGenerator.Core.Tests;

public class ExperienceTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesExperience()
    {
        // Arrange
        var experienceId = Guid.NewGuid();
        var dateIdeaId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var experienceDate = DateTime.UtcNow;
        var notes = "Had a wonderful time!";
        var actualCost = 75.50m;
        var photos = "photo1.jpg,photo2.jpg";

        // Act
        var experience = new Experience
        {
            ExperienceId = experienceId,
            DateIdeaId = dateIdeaId,
            UserId = userId,
            ExperienceDate = experienceDate,
            Notes = notes,
            ActualCost = actualCost,
            Photos = photos
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(experience.ExperienceId, Is.EqualTo(experienceId));
            Assert.That(experience.DateIdeaId, Is.EqualTo(dateIdeaId));
            Assert.That(experience.UserId, Is.EqualTo(userId));
            Assert.That(experience.ExperienceDate, Is.EqualTo(experienceDate));
            Assert.That(experience.Notes, Is.EqualTo(notes));
            Assert.That(experience.ActualCost, Is.EqualTo(actualCost));
            Assert.That(experience.Photos, Is.EqualTo(photos));
        });
    }

    [Test]
    public void DefaultValues_AreSetCorrectly()
    {
        // Arrange & Act
        var experience = new Experience();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(experience.Notes, Is.EqualTo(string.Empty));
            Assert.That(experience.WasSuccessful, Is.True);
            Assert.That(experience.WouldRepeat, Is.True);
            Assert.That(experience.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(experience.Ratings, Is.Not.Null.And.Empty);
        });
    }

    [Test]
    public void MarkAsSuccessful_SetsPropertyToTrue()
    {
        // Arrange
        var experience = new Experience
        {
            ExperienceId = Guid.NewGuid(),
            WasSuccessful = false
        };

        // Act
        experience.MarkAsSuccessful();

        // Assert
        Assert.That(experience.WasSuccessful, Is.True);
    }

    [Test]
    public void MarkAsSuccessful_WhenAlreadyTrue_RemainsTrue()
    {
        // Arrange
        var experience = new Experience
        {
            ExperienceId = Guid.NewGuid(),
            WasSuccessful = true
        };

        // Act
        experience.MarkAsSuccessful();

        // Assert
        Assert.That(experience.WasSuccessful, Is.True);
    }

    [Test]
    public void MarkAsWouldRepeat_SetsPropertyToTrue()
    {
        // Arrange
        var experience = new Experience
        {
            ExperienceId = Guid.NewGuid(),
            WouldRepeat = false
        };

        // Act
        experience.MarkAsWouldRepeat();

        // Assert
        Assert.That(experience.WouldRepeat, Is.True);
    }

    [Test]
    public void MarkAsWouldRepeat_WhenAlreadyTrue_RemainsTrue()
    {
        // Arrange
        var experience = new Experience
        {
            ExperienceId = Guid.NewGuid(),
            WouldRepeat = true
        };

        // Act
        experience.MarkAsWouldRepeat();

        // Assert
        Assert.That(experience.WouldRepeat, Is.True);
    }

    [Test]
    public void WasSuccessful_CanBeSetToFalse()
    {
        // Arrange & Act
        var experience = new Experience
        {
            WasSuccessful = false
        };

        // Assert
        Assert.That(experience.WasSuccessful, Is.False);
    }

    [Test]
    public void WouldRepeat_CanBeSetToFalse()
    {
        // Arrange & Act
        var experience = new Experience
        {
            WouldRepeat = false
        };

        // Assert
        Assert.That(experience.WouldRepeat, Is.False);
    }

    [Test]
    public void ActualCost_CanBeSet()
    {
        // Arrange & Act
        var experience = new Experience
        {
            ActualCost = 125.75m
        };

        // Assert
        Assert.That(experience.ActualCost, Is.EqualTo(125.75m));
    }

    [Test]
    public void ActualCost_CanBeNull()
    {
        // Arrange & Act
        var experience = new Experience
        {
            ActualCost = null
        };

        // Assert
        Assert.That(experience.ActualCost, Is.Null);
    }

    [Test]
    public void Photos_CanBeSet()
    {
        // Arrange
        var photos = "url1.jpg,url2.jpg,url3.jpg";
        var experience = new Experience();

        // Act
        experience.Photos = photos;

        // Assert
        Assert.That(experience.Photos, Is.EqualTo(photos));
    }

    [Test]
    public void DateIdea_NavigationProperty_CanBeSet()
    {
        // Arrange
        var dateIdea = new DateIdea { DateIdeaId = Guid.NewGuid() };
        var experience = new Experience
        {
            DateIdeaId = dateIdea.DateIdeaId
        };

        // Act
        experience.DateIdea = dateIdea;

        // Assert
        Assert.That(experience.DateIdea, Is.EqualTo(dateIdea));
    }

    [Test]
    public void Ratings_CanBePopulated()
    {
        // Arrange
        var experience = new Experience();
        var rating1 = new Rating { RatingId = Guid.NewGuid(), Score = 5 };
        var rating2 = new Rating { RatingId = Guid.NewGuid(), Score = 4 };

        // Act
        experience.Ratings.Add(rating1);
        experience.Ratings.Add(rating2);

        // Assert
        Assert.That(experience.Ratings, Has.Count.EqualTo(2));
    }

    [Test]
    public void ExperienceDate_CanBeSet()
    {
        // Arrange
        var date = new DateTime(2024, 7, 4, 19, 0, 0, DateTimeKind.Utc);
        var experience = new Experience();

        // Act
        experience.ExperienceDate = date;

        // Assert
        Assert.That(experience.ExperienceDate, Is.EqualTo(date));
    }
}
