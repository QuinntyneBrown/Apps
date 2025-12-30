namespace DateNightIdeaGenerator.Core.Tests;

public class DateIdeaTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesDateIdea()
    {
        // Arrange
        var dateIdeaId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var title = "Romantic Dinner";
        var description = "Candlelit dinner at home";
        var category = Category.Romantic;
        var budgetRange = BudgetRange.Medium;
        var location = "Home";
        var durationMinutes = 120;

        // Act
        var dateIdea = new DateIdea
        {
            DateIdeaId = dateIdeaId,
            UserId = userId,
            Title = title,
            Description = description,
            Category = category,
            BudgetRange = budgetRange,
            Location = location,
            DurationMinutes = durationMinutes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dateIdea.DateIdeaId, Is.EqualTo(dateIdeaId));
            Assert.That(dateIdea.UserId, Is.EqualTo(userId));
            Assert.That(dateIdea.Title, Is.EqualTo(title));
            Assert.That(dateIdea.Description, Is.EqualTo(description));
            Assert.That(dateIdea.Category, Is.EqualTo(category));
            Assert.That(dateIdea.BudgetRange, Is.EqualTo(budgetRange));
            Assert.That(dateIdea.Location, Is.EqualTo(location));
            Assert.That(dateIdea.DurationMinutes, Is.EqualTo(durationMinutes));
            Assert.That(dateIdea.IsFavorite, Is.False);
            Assert.That(dateIdea.HasBeenTried, Is.False);
        });
    }

    [Test]
    public void DefaultValues_AreSetCorrectly()
    {
        // Arrange & Act
        var dateIdea = new DateIdea();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dateIdea.Title, Is.EqualTo(string.Empty));
            Assert.That(dateIdea.Description, Is.EqualTo(string.Empty));
            Assert.That(dateIdea.IsFavorite, Is.False);
            Assert.That(dateIdea.HasBeenTried, Is.False);
            Assert.That(dateIdea.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(dateIdea.Experiences, Is.Not.Null.And.Empty);
            Assert.That(dateIdea.Ratings, Is.Not.Null.And.Empty);
        });
    }

    [Test]
    public void MarkAsFavorite_UpdatesPropertiesCorrectly()
    {
        // Arrange
        var dateIdea = new DateIdea
        {
            DateIdeaId = Guid.NewGuid(),
            IsFavorite = false
        };
        var beforeUpdate = DateTime.UtcNow.AddSeconds(-1);

        // Act
        dateIdea.MarkAsFavorite();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dateIdea.IsFavorite, Is.True);
            Assert.That(dateIdea.UpdatedAt, Is.Not.Null);
            Assert.That(dateIdea.UpdatedAt!.Value, Is.GreaterThan(beforeUpdate));
        });
    }

    [Test]
    public void MarkAsTried_UpdatesPropertiesCorrectly()
    {
        // Arrange
        var dateIdea = new DateIdea
        {
            DateIdeaId = Guid.NewGuid(),
            HasBeenTried = false
        };
        var beforeUpdate = DateTime.UtcNow.AddSeconds(-1);

        // Act
        dateIdea.MarkAsTried();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dateIdea.HasBeenTried, Is.True);
            Assert.That(dateIdea.UpdatedAt, Is.Not.Null);
            Assert.That(dateIdea.UpdatedAt!.Value, Is.GreaterThan(beforeUpdate));
        });
    }

    [Test]
    public void GetAverageRating_NoRatings_ReturnsNull()
    {
        // Arrange
        var dateIdea = new DateIdea
        {
            Ratings = new List<Rating>()
        };

        // Act
        var avgRating = dateIdea.GetAverageRating();

        // Assert
        Assert.That(avgRating, Is.Null);
    }

    [Test]
    public void GetAverageRating_NullRatings_ReturnsNull()
    {
        // Arrange
        var dateIdea = new DateIdea
        {
            Ratings = null!
        };

        // Act
        var avgRating = dateIdea.GetAverageRating();

        // Assert
        Assert.That(avgRating, Is.Null);
    }

    [Test]
    public void GetAverageRating_SingleRating_ReturnsThatRating()
    {
        // Arrange
        var dateIdea = new DateIdea
        {
            Ratings = new List<Rating>
            {
                new Rating { Score = 4 }
            }
        };

        // Act
        var avgRating = dateIdea.GetAverageRating();

        // Assert
        Assert.That(avgRating, Is.EqualTo(4.0));
    }

    [Test]
    public void GetAverageRating_MultipleRatings_ReturnsCorrectAverage()
    {
        // Arrange
        var dateIdea = new DateIdea
        {
            Ratings = new List<Rating>
            {
                new Rating { Score = 5 },
                new Rating { Score = 4 },
                new Rating { Score = 3 }
            }
        };

        // Act
        var avgRating = dateIdea.GetAverageRating();

        // Assert
        Assert.That(avgRating, Is.EqualTo(4.0));
    }

    [Test]
    public void GetAverageRating_WithDecimalAverage_ReturnsCorrectValue()
    {
        // Arrange
        var dateIdea = new DateIdea
        {
            Ratings = new List<Rating>
            {
                new Rating { Score = 5 },
                new Rating { Score = 4 },
                new Rating { Score = 4 },
                new Rating { Score = 3 }
            }
        };

        // Act
        var avgRating = dateIdea.GetAverageRating();

        // Assert
        Assert.That(avgRating, Is.EqualTo(4.0));
    }

    [Test]
    public void Season_CanBeSet()
    {
        // Arrange
        var season = "Summer";
        var dateIdea = new DateIdea();

        // Act
        dateIdea.Season = season;

        // Assert
        Assert.That(dateIdea.Season, Is.EqualTo(season));
    }

    [Test]
    public void Location_CanBeSet()
    {
        // Arrange
        var location = "Central Park";
        var dateIdea = new DateIdea();

        // Act
        dateIdea.Location = location;

        // Assert
        Assert.That(dateIdea.Location, Is.EqualTo(location));
    }

    [Test]
    public void DurationMinutes_CanBeSet()
    {
        // Arrange & Act
        var dateIdea = new DateIdea
        {
            DurationMinutes = 90
        };

        // Assert
        Assert.That(dateIdea.DurationMinutes, Is.EqualTo(90));
    }

    [Test]
    public void Experiences_CanBePopulated()
    {
        // Arrange
        var dateIdea = new DateIdea();
        var exp1 = new Experience { ExperienceId = Guid.NewGuid() };
        var exp2 = new Experience { ExperienceId = Guid.NewGuid() };

        // Act
        dateIdea.Experiences.Add(exp1);
        dateIdea.Experiences.Add(exp2);

        // Assert
        Assert.That(dateIdea.Experiences, Has.Count.EqualTo(2));
    }

    [Test]
    public void Ratings_CanBePopulated()
    {
        // Arrange
        var dateIdea = new DateIdea();
        var rating1 = new Rating { RatingId = Guid.NewGuid(), Score = 5 };
        var rating2 = new Rating { RatingId = Guid.NewGuid(), Score = 4 };

        // Act
        dateIdea.Ratings.Add(rating1);
        dateIdea.Ratings.Add(rating2);

        // Assert
        Assert.That(dateIdea.Ratings, Has.Count.EqualTo(2));
    }
}
